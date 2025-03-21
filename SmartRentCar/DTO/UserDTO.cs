using System.ComponentModel.DataAnnotations;

namespace SmartRentCar.DTO
{
    public class UserDTO
    {
       
        public int UserId { get; set; }

       
        public string NameF { get; set; }


        public string NameI { get; set; }


        public string Email { get; set; }

        public string PhoneNumber { get; set; }


        public DateTime CreatedAt { get; set; }

 
        public string License { get; set; }

    }
}
