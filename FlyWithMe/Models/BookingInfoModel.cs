using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlyWithMe.Models
{
    public class BookingInfoModel
    {
        public int Id { get; set; }
        public bool Bag { get; set; }
        public bool Meal { get; set; }
        public bool VIP { get; set; }

        
    }
}