
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ContractService.Models
{
    public class Rental
    {
        [Key]
        public int RentalId { get; set; }

        [ForeignKey("RentalStatus")]
        public int RentalStatusId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Car")]
        public int CarId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal TotalCost { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Deposit { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public RentalStatus RentalStatus { get; set; }
        public Car Car {get; set;}
    }
}
