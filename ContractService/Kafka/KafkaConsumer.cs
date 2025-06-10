using Confluent.Kafka;
using ContractService.Repositories;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ContractService.Kafka
{
    public class KafkaConsumer 
    {
        private readonly IConsumer<string, string> _consumer;
        private readonly IRentalRepository _rentalRepository;
        private readonly CancellationTokenSource _cts;
        private Task? _consumeTask;

        public KafkaConsumer(IRentalRepository rentalRepository, string bootstrapServers, string groupId)
        {
            _rentalRepository = rentalRepository;

            var config = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = groupId,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false
            };

            _consumer = new ConsumerBuilder<string, string>(config).Build();
            _cts = new CancellationTokenSource();
        }

        public void StartConsuming(params string[] topics)
        {
            _consumer.Subscribe(topics);

            _consumeTask = Task.Run(async () =>
            {
                try
                {
                    while (!_cts.Token.IsCancellationRequested)
                    {

                        var result = _consumer.Consume(_cts.Token);

                        if (result != null)
                        {
                            await HandleMessage(result.Message.Value);
                            _consumer.Commit(result);
                        }

                    }
                }
                finally
                {
                    _consumer.Close();
                }
            }, _cts.Token);
        }

        private async Task HandleMessage(string message)
        {

            var data = JsonSerializer.Deserialize<RentalEvent>(message);

            int rentalId = data.RentalId;
            string eventType = data.EventType;
            switch (eventType)
            {
                case "RentalStarted":
                    await _rentalRepository.ConfirmStartByRenter(rentalId);
                    break;
                case "RentalEnd":
                    await _rentalRepository.ConfirmEndByRenter(rentalId);
                    break;
                case "RentalEarlyEnd":
                    await _rentalRepository.ConfirmEarlyEndByRenter(rentalId);
                    break;
                case "DepositRaise":
                    await _rentalRepository.BeginDispute(rentalId);
                    break;
                default:
                    break;
            }
        }



    }
}
