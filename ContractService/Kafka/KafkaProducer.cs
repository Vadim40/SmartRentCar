

using Confluent.Kafka;
using System.Text.Json;

namespace ContractService.Kafka
{


    public class KafkaProducer : IKafkaProducer
    {
        private readonly IProducer<string, string> _producer;

        public KafkaProducer(IConfiguration configuration)
        {
            var config = new ProducerConfig
            {
                //TODO замена
                BootstrapServers = configuration["Kafka:BootstrapServers"]
            };

            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        public async Task SendMessageAsync(string topic, string key, object value)
        {
            var json = JsonSerializer.Serialize(value);
            var message = new Message<string, string> { Key = key, Value = json };
            await _producer.ProduceAsync(topic, message);
        }
    }

}