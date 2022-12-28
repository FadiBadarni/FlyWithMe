﻿using System;
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
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.CreditCard)]
        public string CardNumber { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string ExpirationDate { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public int SecurityCode { get; set; }
    }
}