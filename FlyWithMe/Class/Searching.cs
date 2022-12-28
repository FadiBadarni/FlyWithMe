using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;
using FlyWithMe.Models;
using PayPal.Api;

namespace FlyWithMe.Class
{
    public class Searching
    {

        public string Origin { get; set; }

        public string Destination { get; set; }

        public DateTime Departure { get; set; }


        public DateTime Return { get; set; }

        public string Class { get; set; }

        public int Passengers { get; set; }
        public int IdGo { get; set; }
        public int IdBack { get; set; }

        public string infoTostring()
        {
            return Origin + "," + Destination + "," + Departure +
                "," + Return + "," + Class + "," + Passengers + "," + IdGo + "," + IdBack;
        }

        public void bulidSearchingFromSring(string str)
        {
            if (str == null || str.Length == 0) return;
            int i = 0;
            string s = "";
            while (str[i] != ',')
                s += str[i++];
            this.Origin = s;

            s = "";
            i++;
            while (str[i] != ',')
                s += str[i++];
            this.Destination = s;

            s = "";
            i++;
            while (str[i] != ',')
                s += str[i++];
            int j = 0;


            this.Departure = stringtodate(s);


            s = "";
            i++;
            while (str[i] != ',')
                s += str[i++];
            this.Return = stringtodate(s);


            s = "";
            i++;
            while (str[i] != ',')
                s += str[i++];
            this.Class = s;

            s = "";
            i++;
            while (str[i] != ',')
                s += str[i++];
            this.Passengers = int.Parse(s, NumberStyles.AllowCurrencySymbol);

            s = "";
            i++;
            while (str[i] != ',')
                s += str[i++];
            this.IdGo = int.Parse(s, NumberStyles.AllowCurrencySymbol); ;


            s = "";
            i++;
            while (i < str.Length)
                s += str[i++];
            this.IdBack = int.Parse(s, NumberStyles.AllowLeadingSign);
        }

        private DateTime stringtodate(string s)
        {
            int j = 0, m, d, y;
            string help = "";
            while (s[j] != '/')
                help += s[j++];
            d = int.Parse(help, NumberStyles.AllowCurrencySymbol);
            help = "";
            j++;
            while (s[j] != '/')
                help += s[j++];
            m = int.Parse(help, NumberStyles.AllowCurrencySymbol);
            help = "";
            j++;
            while (s[j] != ' ')
                help += s[j++];
            y = int.Parse(help, NumberStyles.AllowCurrencySymbol);
            return new DateTime(y, m, d);
        }

        public  string yearMonth(int x)
        {
            if (x == 1)
            {
                if (Departure.Month < 10)
                    return Departure.Year.ToString() + "-0" + Departure.Month.ToString();

                else
                    return Departure.Year.ToString() + "-" + Departure.Month.ToString();

            }
            else
            {
                if (Departure.Month < 10)
                    return Return.Year.ToString() + "-0" + Return.Month.ToString();

                else
                    return Return.Year.ToString() + "-" + Return.Month.ToString();
            }


        }
            
    }
}