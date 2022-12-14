using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace FlyWithMe.Models
{
    public class InsertDataModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string FristName { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string email { get; set; }


        [Required]
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Passport")]
        public Image Passport { get; set; }



        //[Required]
        //[FileExtensions]







    }
}