using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FlyWithMe.Models
{
    public class PaymentModel
    {
        [Required]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[a-zA-Z]+ [a-zA-Z]+$", ErrorMessage = "Name must contain both a family name and a main name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "Credit Card must be a 16-Digit Number")]
        public string CardNumber { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/(202[3-9]|2030)$", ErrorMessage = "Expiration date must be in the format MM/YYYY")]
        public string ExpirationDate { get; set; }
        [Required]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "CCV must be a 3-Digit Number")]
        [DataType(DataType.Text)]
        public int SecurityCode { get; set; }
    }
}