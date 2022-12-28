using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FlyWithMe.Models
{
    public class Passenger
    {
        [Required]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[a-zA-Z]+ [a-zA-Z]+$", ErrorMessage = "Name must contain both a family name and a main name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please Enter A Valid E-mail Address")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^05\d{8}$", ErrorMessage = "Phone must start with 05 and be a 10-digit integer")]
        public int Phone { get; set; }
        [Key]
        [Required]
        [DataType(DataType.Text)]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "ID must be a 9-Digit Integer")]
        public int ID { get; set; }

        public override string ToString()
        {
            return ID + "\n" + Name + "\n" + Email + "\n" + Phone + "\n";
        }
    }
}