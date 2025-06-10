namespace ContractService.Kafka
{
    public interface IKafkaProducer
    {
        Task SendMessageAsync(string topic, string key, object value);
    }
}