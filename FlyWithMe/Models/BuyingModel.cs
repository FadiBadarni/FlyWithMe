using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace FlyWithMe.Models
{
    public class BuyingModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.CreditCard)]
        [Display(Name = "CreditCard")]
        public string creditCard { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "CVV")]
        public int cVV { get; set; }

      
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "MM")]
            public string mM { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "YY")]
        public string YY { get; set; }
        
         [Required]
        [DataType(DataType.Text)]
        [Display(Name = "ID")]
        public string id { get; set; }


        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string FristName { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }




    }
}