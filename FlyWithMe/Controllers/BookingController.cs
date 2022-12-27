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


            DepartureYearMonth = search.yearMonth(1);

            var outboundFlight = await firebaseClient.Child("Planes")
                .Child(search.Origin)
                .Child(search.Destination)
                .Child(DepartureYearMonth)
                .Child(search.IdGo.ToString())
                .OnceSingleAsync<Planes>();
            ViewBag.outboundFlight = outboundFlight;



            if (search.IdBack != -1)
            {
                ReturnYearMonth=search.yearMonth(2);
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
            ViewBag.search = search;
            Session["count"] = search.Passengers;
            return View();
        }






        [HttpPost]
        public ActionResult Payment(List<Passenger> p, string goPlane, string backPlane, string searching)
        {
            Planes planes1 = new Planes();
            Planes planes2 = null;
            Searching search = new Searching();
            search.bulidSearchingFromSring(searching);
            var firebaseClient = new FirebaseClient(FirbaseLink);


            planes1.bulidPlaneFromSring(goPlane);
            planes1.BookedSeats += p.Count;
            var Updateplane = firebaseClient
                    .Child("Planes")
                    .Child(search.Origin)
                    .Child(search.Destination)
                    .Child(search.yearMonth(1))
                    .Child(planes1.ID + "")
                    .Child("BookedSeats")
                    .PutAsync(planes1.BookedSeats);
           
            if (backPlane != "Empty")
            {
                planes2 = new Planes();
                planes2.bulidPlaneFromSring(backPlane);
                planes2.BookedSeats += p.Count;
                ViewBag.planes2 = planes2;

                Updateplane = firebaseClient
                 .Child("Planes")
                 .Child(search.Destination)
                 .Child(search.Origin)
                 .Child(search.yearMonth(2))
                 .Child(planes2.ID + "")
                    .Child("BookedSeats")
                 .PutAsync(planes2.BookedSeats);
            }
            List<int> seatNumber = new List<int>();
            for (int i = 0; i < p.Count; i++)
            {
                seatNumber.Add(planes1.BookedSeats - i);
            }


          
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
                   .Child(p[0].ID + "")
                   .PutAsync(paymentFirebase);

            ViewBag.planes1 = planes1;
            ViewBag.seatNumber = seatNumber;
            ViewBag.p = p;
            ViewBag.search = search;

            return View();
        }


        [HttpPost]
        public ActionResult ProcessPayment(Models.Payment payment, string id)
        {
            var firebaseClient = new FirebaseClient(FirbaseLink);
            var PaymentFirebase = firebaseClient
                  .Child("Payment")
                  .Child(id).Child("payment")
                  .PutAsync(payment);
            
            return View();
        }

    }
}