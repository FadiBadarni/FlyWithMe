using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FlyWithMe.Models
{
    public class loginModel
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

      //  [ValidatePersonName]
        [EmailAddress]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
     

    }

    //[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    //public class ValidatePersonName : ValidationAttribute
    //{
    //    protected override ValidationResult IsValid(object value, ValidationContext validationContext) { 
            
            
    //        string PhoneNumber = (string)validationContext.ObjectType.GetProperty("Phone Number").GetValue(validationContext.ObjectInstance, null);
        
    //        string Email = (string)validationContext.ObjectType.GetProperty("Email Address").GetValue(validationContext.ObjectInstance, null);

           

    //        //check at least one has a value
    //        if (string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(PhoneNumber))
    //            return new ValidationResult("At least one is required!!");

    //        return ValidationResult.Success;
    //    }
    //}
}