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
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public int Phone { get; set; }
        [Key]
        [Required]
        [DataType(DataType.Text)]
        public int ID { get; set; }

        public override string ToString()
        {
            return ID +"\n"+Name +"\n"+Email+"\n"+Phone+"\n";
        }
    }
}