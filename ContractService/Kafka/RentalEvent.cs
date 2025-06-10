namespace ContractService.Kafka
{
    public class RentalEvent
    {
        public int RentalId { get; set; }
        public string EventType { get; set; } 
        public DateTime Timestamp { get; set; }
    }


}