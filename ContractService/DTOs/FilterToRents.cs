namespace ContractService.DTOs 
{
    public class FilterToRents{
        public int []? RentalStatuses {get; set;}
        public DateTime? StartDate {get; set;}
        public DateTime? EndDate {get; set;}
        public string? CarName {get; set;}
    }
}