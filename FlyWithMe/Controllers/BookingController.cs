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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace FlyWithMe.Controllers
{

    public class BookingController : Controller
    {

        private static string FirbaseLink = "https://myflight-db2b1-default-rtdb.firebaseio.com";
        public async Task<ActionResult> PassengersInfo([Bind(Include = "IdGo,IdBack,Origin,Destination,Departure,Return,Class,Passengers")] Searching search)
        {
            var firebaseClient = new FirebaseClient(FirbaseLink);
            string DepartureYearMonth, ReturnYearMonth;




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
            ViewBag.search= search;
            Session["count"] = search.Passengers;
            return View();
        }

       

     


        [HttpPost]
        public ActionResult Payment(List<Passenger> p, string goPlane, string backPlane,  string searching)
        {

            Planes planes1= new Planes();
            planes1.bulidPlaneFromSring(goPlane);
            Planes planes2 = new Planes();
            planes2.bulidPlaneFromSring(backPlane);


            Searching search= new Searching();
            search.bulidSearchingFromSring(searching);
            var firebaseClient = new FirebaseClient(FirbaseLink);
            PaymentFirebase paymentFirebase = new PaymentFirebase
            {
                id = p[0].ID,
                InboundFlight = planes1,
                OutboundFlight = planes2,
                PaymentInfo = null,
                passengers = p
            };
             var PaymentFirebase = firebaseClient
                    .Child("Payment")
                    .Child(p[0].ID+"")
                    .PutAsync(paymentFirebase);

            ViewBag.planes1 = planes1;
            ViewBag.planes2 = planes2;
            ViewBag.p = p;
            ViewBag.search = search;
            return View();
        }
        //public ActionResult Payment([Bind(Include = "IdGo,IdBack,Origin,Destination,Departure,Return,Class,Passengers")] Searching search)
        //{
        //    return View();
        //}


    }
}