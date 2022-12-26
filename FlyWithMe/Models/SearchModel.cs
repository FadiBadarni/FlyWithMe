using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace FlyWithMe.Models
{
    public class SearchModel
    {
        [Required]
        public string Origin { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        public DateTime Departure { get; set; }
        [Required]
        public DateTime Return { get; set; }
        [Required]
        public string Class { get; set; }
        [Required]
        public int Passengers { get; set; }
        public override string ToString()
        {
            return Origin + " ⸺✈︎ " + Destination;
        }

        public static SearchModel Copy(SearchModel model)
        {
            SearchModel newSearch = new SearchModel()
            {
                Origin = model.Origin,
                Destination = model.Destination,
                Departure = model.Departure,
                Return = model.Return,   
                Class = model.Class,
                Passengers = model.Passengers
            };
            return newSearch;
        }


        //public static string infoInString()
        //{
        //    return ""
        //}
    }

}