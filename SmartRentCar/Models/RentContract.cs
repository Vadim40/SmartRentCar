using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartRentCar.Models
{
    public class RentContract
    {
        [Key]
        public int RentContractId { get; set; }

        [ForeignKey("ContractStatus")]
        public int ContractStatusId { get; set; }

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

        public ContractStatus ContractStatus { get; set; }
        public User User { get; set; }
        public Car Car { get; set; }
    }
}
