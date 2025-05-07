namespace ContractService.DTOs
{
    public class RentalDTO 
    {
        public int RentalId { get; set; }

        public int RentalStatusId { get; set; }
        public string RentalStatusName { get; set; }
        public string CarName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }


        public decimal TotalCost { get; set; }

        public decimal Deposit { get; set; }

        public DateTime CreatedAt { get; set; }

        public string ContractAddress { get; set; }


    }
}