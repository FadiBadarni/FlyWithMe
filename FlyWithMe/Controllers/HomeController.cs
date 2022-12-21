using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Firebase.Database;
using Firebase.Database.Query;
using FlyWithMe.Class;
using FlyWithMe.Models;

namespace FlyWithMe.Controllers
{
    public class HomeController : Controller
    {
        private static string FirbaseLink = "https://myflight-db2b1-default-rtdb.firebaseio.com";
       

        public ActionResult Index()
        {
            //var firebaseClient = new FirebaseClient(FirbaseLink);

            //List<string> days = new List<string> { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
            //// List<string> Cites = new List<string> { "Vienna", "Brussels", "Bahrain", "Sofia", "Montreal", "Beijing", "Larnaca", "Paris", "Berlin", "Rome", "Barcelona", "Geneva", "Bangko" };
            //List<string> AirPort = new List<string> { "El Al" };
            //List<string> FromTo = new List<string> { "Bangkok", "TelAviv" };
            //int x = 9;
            //Random rd = new Random();
            


            

            //for (int i = 180; i < 200; i++)
            //{
            //    int bookedSeats = rd.Next(401);
            //    int capacity = rd.Next(bookedSeats, 401);
            //    double price = rd.Next(100, 1201);
            //    string company = AirPort[rd.Next(AirPort.Count)];
            //    int h1 = rd.Next(25);
            //    int m1 = rd.Next(0, 61);

            //    string sm1 = ":" + m1;
            //    if (m1 < 10)
            //        if (m1 == 0) sm1 = h1 + ":00";
            //        else sm1 = h1 + ":0" + m1;
            //    string landing = h1 + sm1;

            //    string takeOff = (h1 + x) + sm1;
            //    int indexDays = rd.Next(days.Count);
            //    int indexFromTo = rd.Next(FromTo.Count);
            //    Planes p = new Planes()
            //    {
            //        BookedSeats = bookedSeats,
            //        Capacity = capacity,
            //        Price = price,
            //        ID = i,
            //        Company = company,
            //        Landing = landing,
            //        TakeOff = takeOff
            //    };
            //    await firebaseClient.Child("Planes").Child(FromTo[indexFromTo]).
            //        Child(FromTo[(indexFromTo + 1) % 2]).Child(days[indexDays]).Child(i + "").PutAsync(p);
            //}
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult OutboundFlights()
        {
           return Redirect("/Home");

        }


            [HttpPost]
        public async Task<ActionResult> OutboundFlights(SearchModel search)
        {
            if (search == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                //return Redirect("/Home");
            }

            ViewData["Origin"] = Request["Origin"];
            ViewData["Destination"] = Request["Destination"];

            ViewData["Departure"] = Request["Departure"];
            ViewData["Return"] = Request["Return"];

            ViewData["PassengersCount"] = search.Passengers;

            var firebaseClient = new FirebaseClient(FirbaseLink);

           // Searching searching = new Searching(search);
            
           // var result = firebaseClient.Child("searching").PostAsync(searching).Result;

           
            var dbPlanes = await firebaseClient.Child("Planes")
                .Child(search.Origin)
                .Child(search.Destination)
                .Child(search.Departure.DayOfWeek.ToString())
                .OnceAsync<Planes>();
            List<Planes> GoingPlaneslist = new List<Planes>();
            List<Planes> BackPlaneslist = new List<Planes>();
            foreach (var plane in dbPlanes)
            { 
               
                if (plane.Object.Capacity - plane.Object.BookedSeats >= search.Passengers)
                GoingPlaneslist.Add(plane.Object);

            }
            dbPlanes = await firebaseClient.Child("Planes")
               .Child(search.Destination)
               .Child(search.Origin)
               .Child(search.Departure.DayOfWeek.ToString())
               .OnceAsync<Planes>();

            foreach (var plane in dbPlanes)
            {
                if (plane.Object.Capacity - plane.Object.BookedSeats >= search.Passengers)
                    BackPlaneslist.Add(plane.Object);
            }

            ViewBag.GoingPlaneslist = GoingPlaneslist;
           // ViewBag.Key = result.Key;
            ViewBag.SearchResults = search;

            return View();
        }


        public async Task<ActionResult> InboundFlights([Bind(Include = "IdGo,IdBack,Origin,Destination,Departure,Return,Class,Passengers")] Searching search)
        {
           
            ViewData["Origin"] = Request["Origin"];
            ViewData["Destination"] = Request["Destination"];

            ViewData["Departure"] = Request["Departure"];
            ViewData["Return"] = Request["Return"];

            var firebaseClient = new FirebaseClient(FirbaseLink);
            // var search= await firebaseClient.Child("Searching").Child(key).OnceSingleAsync<SearchModel>();
            List<Planes> BackPlaneslist = new List<Planes>();
            var dbPlanes = await firebaseClient.Child("Planes")
              .Child(search.Destination)
              .Child(search.Origin)
              .Child(search.Departure.DayOfWeek.ToString())
              .OnceAsync<Planes>();

            foreach (var plane in dbPlanes)
            {
                if (plane.Object.Capacity - plane.Object.BookedSeats >= search.Passengers)
                    BackPlaneslist.Add(plane.Object);
            }

            var onePlanes = await firebaseClient.Child("Planes")
           .Child(search.Origin)
           .Child(search.Destination)
           .Child(search.Departure.DayOfWeek.ToString()).Child(search.IdGo.ToString())
           .OnceSingleAsync<Planes>();

            onePlanes.TotalPrice(search.Passengers);
            ViewBag.BackPlaneslist = BackPlaneslist;
            ViewBag.SearchResults = search;
            ViewBag.onePlanes = onePlanes;
            return View();
        }


        //public async Task<ActionResult> BookingInfo([Bind(Include = "IdGo,IdBack,Origin,Destination,Departure,Return,Class,Passengers")] Searching search)
        //{
        //    double price = 0;
        //    List<Planes> planes = new List<Planes>();
        //    var firebaseClient = new FirebaseClient(FirbaseLink);
        //    var goPlanes = await firebaseClient.Child("Planes").Child(search.Origin)
        //    .Child(search.Destination)
        //    .Child(search.Departure.DayOfWeek.ToString()).Child(search.IdGo.ToString())
        //    .OnceSingleAsync<Planes>();
        //    goPlanes.TotalPrice(search.Passengers);
        //    price += goPlanes.Price;
        //    planes.Add(goPlanes);
        //    if (search.IdBack != -1)
        //    {
        //        var backPlanes = await firebaseClient.Child("Planes").Child(search.Destination)
        //     .Child(search.Origin)
        //     .Child(search.Departure.DayOfWeek.ToString()).Child(search.IdBack.ToString())
        //     .OnceSingleAsync<Planes>();
        //        backPlanes.TotalPrice(search.Passengers);
        //        price += backPlanes.Price;
        //        planes.Add(backPlanes);
        //    }

            
        //    ViewBag.SearchResults = search;
        //    ViewBag.Planes = planes;
        //    ViewBag.Price = price;

        //    return View();
        //}
    }

}