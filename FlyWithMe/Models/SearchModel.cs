using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlyWithMe.Models
{
    public class SearchModel
    {
        public string from { get; set; }
        public string To { get; set; }
        
        public DateTime departure { get; set; }
        public DateTime preturn { get; set; }
        public string p_class { get; set; }

        public int travelers { get; set; }
    }
   
}