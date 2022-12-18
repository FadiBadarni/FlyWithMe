using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlyWithMe.Class
{
    public class Planes
    {
        public int BookedSeats { get; set; }
        public int Capacity { get; set; }
        public string Company { get; set; }
        public string DepartureDate { get; set; }
        public string From { get; set; }
        public int ID { get; set; }
        public string Landing { get; set; }
        public double Price  { get; set; }
        public string ReturnDate { get; set; }

        public string TakeOff { get; set; }
    
        public string To { get; set; }
        public Planes(int bookedSeats, int capacity, string company, string departureDate, string from, int id, string landing, double price, string returnDate, string takeOff, string to)
        {
            this.BookedSeats = bookedSeats;
            this.Capacity = capacity;
            this.Company = company;
            this.DepartureDate = departureDate;
            this.From = from;
            this.ID = id;
            this.Landing = landing;
            this.Price = price;
            this.ReturnDate = returnDate;
            TakeOff = takeOff;
            this.To = to;
        }
    }
}