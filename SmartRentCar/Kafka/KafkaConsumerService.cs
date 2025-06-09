using Confluent.Kafka;

namespace SmartRentCar.Kafka
{

    public class KafkaConsumerService : BackgroundService
    {
        private readonly IConfiguration _config;

        public KafkaConsumerService(IConfiguration config)
        {
            _config = config;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumerConfig = new ConsumerConfig
            {
                // TODO заменить на реальное
                BootstrapServers = _config["Kafka:BootstrapServers"],
                GroupId = "my-consumer-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
            consumer.Subscribe("your-topic");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var cr = consumer.Consume(stoppingToken);
                    Console.WriteLine($"Received: {cr.Message.Value}");
                }
                catch (OperationCanceledException) { break; }
            }

            consumer.Close();
        }
    }

}
