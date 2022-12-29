using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlyWithMe.Class
{
    public class ListPaymentFirbase
    {
        public List<PaymentFirebase> List { get; set; }
        ListPaymentFirbase() { 
            List = new List<PaymentFirebase>();
        }
    }
}