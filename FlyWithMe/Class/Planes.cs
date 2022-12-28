using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
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
        //[Key]
        public int ID { get; set; }
        public string Landing { get; set; }
        public int Price { get; set; }
        public int getPrice() { return Price; }
        public string TakeOff { get; set; }

        public Object TotalPrice(int x)
        {
            return this.Price = Price *= x;
        }

        public override string ToString()
        {
            return "Landing at:" + Landing + "     TakeOff:" + TakeOff + "\n Total Price:" + Price;
        }


        public Planes() { }
    
        public string infoTostring()
        {
            return BookedSeats + "," + Capacity + "," + Company + "," + DepartureDate + "," + Landing + "," + Price + "," + TakeOff+","+ID;
        }

        public void bulidPlaneFromSring(string str)
        {
            
            if(str== null || str.Length == 0) return;
            int i = 0;
            string s = "";
            while (str[i] != ',')
                s+= str[i++];
            this.BookedSeats = int.Parse(s, NumberStyles.AllowCurrencySymbol);

            s = "";
            i++;
            while (str[i] != ',')
                s += str[i++]; 
            this.Capacity = int.Parse(s, NumberStyles.AllowCurrencySymbol);

            s = "";
            i++;
            while (str[i] != ',')
                s += str[i++];
            this.Company = s;


            s = "";
            i++;
            while (str[i] != ',')
                s += str[i++];
            this.DepartureDate = s;


            s = "";
            i++;
            while (str[i] != ',')
                s += str[i++];
            this.Landing = s;

            s = "";
            i++;
            while (str[i] != ',')
                s += str[i++];
            this.Price = int.Parse(s, NumberStyles.AllowCurrencySymbol);

            s = "";
            i++;
            while (str[i] != ',')
                s += str[i++];
            this.TakeOff =s;


            s = "";
            i++;
            while (i<str.Length)
                    s += str[i++];
                   
                
                
            this.ID = int.Parse(s, NumberStyles.AllowCurrencySymbol);
        }
    
    }
}