using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlyWithMe.Models;

namespace FlyWithMe.Class
{
    public class PaymentFirebase
    {
        public List<Passenger> passengers { get; set; }
        public int id { get; set; }
        public Planes InboundFlight { get; set; }

        public Planes OutboundFlight { get; set; }

        public Payment PaymentInfo { get; set; }

    }
}