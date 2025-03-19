using System;
using System.ComponentModel.DataAnnotations;

namespace SmartRentCar.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string NameF { get; set; }

        [Required]
        [MaxLength(50)]
        public string NameI { get; set; }

        [Required]
        [MinLength(8)]
        public string Pass { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        [MaxLength(20)]
        public string License { get; set; }

        public bool IsVerified { get; set; }
    }
}
