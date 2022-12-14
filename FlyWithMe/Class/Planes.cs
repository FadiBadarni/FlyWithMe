using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlyWithMe.Class
{
    public class Planes
    {
        public int bookedSeats { get; set; }
        public int capacity { get; set; }
        public string company { get; set; }
        public string departureDate { get; set; }
        public string from { get; set; }
        public int id { get; set; }
        public string landing { get; set; }
        public double price  { get; set; }
        public string returnDate { get; set; }

        public string TakeOff { get; set; }
    
        public string to { get; set; }
        public Planes(int bookedSeats, int capacity, string company, string departureDate, string from, int id, string landing, double price, string returnDate, string takeOff, string to)
        {
            this.bookedSeats = bookedSeats;
            this.capacity = capacity;
            this.company = company;
            this.departureDate = departureDate;
            this.from = from;
            this.id = id;
            this.landing = landing;
            this.price = price;
            this.returnDate = returnDate;
            TakeOff = takeOff;
            this.to = to;
        }
    }
}