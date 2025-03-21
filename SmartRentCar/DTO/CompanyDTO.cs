using System.ComponentModel.DataAnnotations;

namespace SmartRentCar.DTO
{
    public class CompanyDTO
    {
        
        public int CompanyId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
