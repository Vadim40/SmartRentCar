using Confluent.Kafka;

namespace SmartRentCar.Kafka
{
    public class KafkaProducerService
    {
        private readonly IProducer<Null, string> _producer;

        public KafkaProducerService(IConfiguration config)
        {
            var producerConfig = new ProducerConfig
            {
                // TODO заменить на реальный
                BootstrapServers = config["Kafka:BootstrapServers"] 
            };

            _producer = new ProducerBuilder<Null, string>(producerConfig).Build();
        }

        public async Task SendMessageAsync(string topic, string message)
        {
            await _producer.ProduceAsync(topic, new Message<Null, string> { Value = message });
        }
    }
}
