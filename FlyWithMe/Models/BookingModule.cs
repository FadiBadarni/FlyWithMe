using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace FlyWithMe.Models
{
    public class BookingModule
    {
        [Key]
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "ID Number")]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string firstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string lastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Year Of Birth")]
        public string yearOfBirth { get; set; }
        
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string phoneNumber { get; set; }







    }
}