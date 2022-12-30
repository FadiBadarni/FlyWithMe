using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FlyWithMe.Models
{
    public class MyFlight
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "ID")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "ID must be a 9-Digit Integer")]
        public string ID { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Order ID")]
        [RegularExpression(@"^\d{7}$", ErrorMessage = "The order wiht a 7-digit integer")]
        public int OrderID { get; set; }


    }

}