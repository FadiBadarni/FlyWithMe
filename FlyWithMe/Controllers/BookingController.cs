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
using PayPal.Api;

namespace FlyWithMe.Controllers
{
    
    public class BookingController : Controller
    {

        private static string FirbaseLink = "https://myflight-db2b1-default-rtdb.firebaseio.com";
        public async Task<ActionResult> PassengersInfo([Bind(Include = "IdGo,IdBack,Origin,Destination,Departure,Return,Class,Passengers")] Searching search)
        {
            var firebaseClient = new FirebaseClient(FirbaseLink);
            string DepartureYearMonth,ReturnYearMonth; 




            if (search.Departure.Month < 10)
            {
                DepartureYearMonth = search.Departure.Year.ToString() + "-0" + search.Departure.Month.ToString();
            }
            else
            {
                DepartureYearMonth = search.Departure.Year.ToString() + "-" + search.Departure.Month.ToString();

            }
            var outboundFlight = await firebaseClient.Child("Planes")
                .Child(search.Origin)
                .Child(search.Destination)
                .Child(DepartureYearMonth)
                .Child(search.IdGo.ToString())
                .OnceSingleAsync<Planes>();
            ViewBag.outboundFlight = outboundFlight;



            if (search.IdBack != -1)
            {
                if (search.Return.Month < 10)
                {
                    ReturnYearMonth = search.Return.Year.ToString() + "-0" + search.Return.Month.ToString();
                }
                else
                {
                    ReturnYearMonth = search.Return.Year.ToString() + "-" + search.Return.Month.ToString();
                }
                var inboundFlight = await firebaseClient.Child("Planes")
                    .Child(search.Destination)
                    .Child(search.Origin)
                    .Child(ReturnYearMonth)
                    .Child(search.IdBack.ToString())
                    .OnceSingleAsync<Planes>();
                ViewBag.inboundFlight = inboundFlight;
            }
            else
            {
                ViewBag.inboundFlight = null;
            }

         


            ViewBag.outboundFlight = outboundFlight;
            
            Session["count"] = search.Passengers;
            return View();
        }

        public ActionResult Payment()
        {
            return View();
        }


    }
}