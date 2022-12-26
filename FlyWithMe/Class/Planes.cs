using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
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
        [Key]
        public int ID { get; set; }
        public string Landing { get; set; }
        public int Price { get; set; }
        public string TakeOff { get; set; }

        public Object TotalPrice(int x)
        {
            return this.Price = Price *= x;
        }

        public override string ToString()
        {
            return "Landing:" + Landing + "     TakeOff:" + TakeOff + "\n Total Price:" + Price;
        }

        // public string To { get; set; }
        //public Planes(int bookedSeats, int capacity, string company, int id, string landing, double price, string takeOff)
        //{
        //    this.BookedSeats = bookedSeats;
        //    this.Capacity = capacity;
        //    this.Company = company;
        //   // this.DepartureDate = departureDate;
        //    //this.From = from;
        //    this.ID = id;
        //    this.Landing = landing;
        //    this.Price = price;
        //   // this.ReturnDate = returnDate;
        //    TakeOff = takeOff;
        //   // this.To = to;
        //}
    }
}