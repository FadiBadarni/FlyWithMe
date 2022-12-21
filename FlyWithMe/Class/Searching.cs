using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FlyWithMe.Models;

namespace FlyWithMe.Class
{
    public class Searching
    {
        //private int v;

        //public Searching(SearchModel search)
        //{
        //    this.search = search;
        //    this.v = 0;
        //}
    
        public string Origin { get; set; }
       
        public string Destination { get; set; }
      
        public DateTime Departure { get; set; }
       
        public DateTime Return { get; set; }
        
        public string Class { get; set; }
        
        public int Passengers { get; set; }
        public int IdGo { get; set; }
        public int IdBack { get; set; }
        // SearchModel search { get; set; }
    }
}