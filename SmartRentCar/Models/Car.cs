using SmartRentCar.Models.CarInfo;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartRentCar.Models
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }
        [ForeignKey("Company")]
        public int? CompanyId { get; set; }

        [ForeignKey("CarBrand")]
        public int BrandId { get; set; }

        [ForeignKey("CarClass")]
        public int ClassId { get; set; }

        [ForeignKey("CarFuelType")]
        public int FuelTypeId { get; set; }

        [ForeignKey("CarTransmission")]
        public int TransmissionTypeId { get; set; }

        public decimal CostPerDay { get; set; }
        public decimal DepositAmount { get; set; }

        [Required]
        [MaxLength(100)]
        public string CarName { get; set; }

        public CarBrand CarBrand { get; set; }
        public CarClass CarClass { get; set; }
        public CarFuelType CarFuelType { get; set; }
        public CarTransmission CarTransmission { get; set; }
        public Company Company { get; set; }
    }
}
