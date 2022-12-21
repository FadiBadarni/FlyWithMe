using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Firebase.Database;
using Firebase.Database.Query;
using FlyWithMe.Class;
using FlyWithMe.Models;

namespace FlyWithMe.Controllers
{
    
    public class BookingController : Controller
    {

        private static string FirbaseLink = "https://myflight-db2b1-default-rtdb.firebaseio.com";
        public ActionResult InsertData(int? id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session["count"] = count;
            return View();
        }

        public ActionResult Payment()
        {
            return View();
        }

        
        public ActionResult BookingInfo()
        {
            return View();
        }

       
        public ActionResult BookingInfo([Bind(Include = "IdGo,IdBack,Origin,Destination,Departure,Return,Class,Passengers")] Searching search)
        {
            double price = 0;
            //List<Planes> planes = new List<Planes>();
            //var firebaseClient = new FirebaseClient(FirbaseLink);
            //var goPlanes = await firebaseClient.Child("Planes").Child(search.Origin)
            //.Child(search.Destination)
            //.Child(search.Departure.DayOfWeek.ToString()).Child(search.IdGo.ToString())
            //.OnceSingleAsync<Planes>();
            //goPlanes.TotalPrice(search.Passengers);
            //price += goPlanes.Price;
            //planes.Add(goPlanes);
            //if (search.IdBack != -1)
            //{
            //    var backPlanes = await firebaseClient.Child("Planes").Child(search.Destination)
            // .Child(search.Origin)
            // .Child(search.Departure.DayOfWeek.ToString()).Child(search.IdBack.ToString())
            // .OnceSingleAsync<Planes>();
            //    backPlanes.TotalPrice(search.Passengers);
            //    price += backPlanes.Price;
            //    planes.Add(backPlanes);
            //}


            //ViewBag.SearchResults = search;
            //ViewBag.Planes = planes;
            //ViewBag.Price = price;

            return View();
        }
    }
}