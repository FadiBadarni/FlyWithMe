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

            // Replace these placeholder values with your own Firebase project details
            string apiKey = "AIzaSyAvKjlB0TZ_lgvFGMFKEcuhDHbgi0Dpf4M";
            string databaseURL = "https://myflight-db2b1-default-rtdb.firebaseio.com";

            // Initialize the Firebase client
            var firebaseClient = new FirebaseClient(databaseURL, new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(apiKey)
            });

            //const int numPlanes = 2000;
            //// Generate a list of 50 random city names
            //var cityNames = GenerateRandomCityNames();

            //// Loop through the city names and add planes to the database
            //for (var i = 0; i < numPlanes; i++)
            //{
            //    // Generate a random departure city
            //    var departureCity = GenerateRandomCityName();

            //    // Generate a random destination city
            //    var destinationCity = GenerateRandomCityName();

            //    // Skip the same city
            //    if (departureCity == destinationCity)
            //    {
            //        continue;
            //    }

            //    // Generate a random year and month
            //    var randomYear = rnd.Next(2023, 2024);
            //    var randomMonth = rnd.Next(2, 13);
            //    var yearMonth = $"{randomYear}-{randomMonth:D2}";

            //    var takeOffDate = GenerateRandomDate(randomYear, randomMonth);

            //    // Generate a random plane ID
            //    var planeID = GenerateRandomPlaneID();

            //    // Generate random booking information
            //    var bookedSeats = rnd.Next(0, 101);
            //    var capacity = 100;
            //    var company = GenerateRandomCompanyName();


            //    // Generate a random landing time
            //    var landing = GenerateRandomLandingTime();


            //    var price = rnd.Next(100, 1000);

            //    var takeoff = GenerateRandomTakeoffTime(landing);

            //    // Add the plane to the database
            //    var plane = new Planes
            //    {
            //        BookedSeats = bookedSeats,
            //        Capacity = capacity,
            //        Company = company,
            //        ID = planeID,
            //        Landing = landing,
            //        Price = price,
            //        TakeOff = takeoff,
            //        DepartureDate = takeOffDate
            //    };
            //    var planeKey = firebaseClient
            //        .Child("Planes")
            //        .Child(departureCity)
            //        .Child(destinationCity)
            //        .Child(yearMonth)
            //        .Child(planeID.ToString())
            //        .PutAsync(plane);

            //}




            var model = new SearchModel();
            var cities = Enum.GetValues(typeof(Cities)).Cast<Cities>();
            var selectList = new SelectList(cities.Select(x => new
            {
                Value = x,
                Text = x.GetType()
                        .GetMember(x.ToString())
                        .First()
                        .GetCustomAttribute<CityNameAttribute>()
                        .Name
            }), "Value", "Text");
            ViewBag.Cities = selectList;
            return View(model);
        }



        private static Random rnd = new Random();
        private static string GenerateRandomCityName()
        {
            var cityNames = new[]
            {
                "NewYork", "LosAngeles", "Chicago", "Houston", "Phoenix",
                "Philadelphia", "SanAntonio", "SanDiego"
            };
            var index = rnd.Next(cityNames.Length);
            return cityNames[index];
        }
        // Generate a list of 50 random city names
        private static List<string> GenerateRandomCityNames()
        {
            var cityNames = new List<string>();
            for (int i = 0; i < 50; i++)
            {
                cityNames.Add(GenerateRandomCityName());
            }
            return cityNames;
        }


        // Generate a random plane ID
        private static int GenerateRandomPlaneID()
        {
            return rnd.Next(10000, 100000);
        }


        // Generate a random company name
        private static string GenerateRandomCompanyName()
        {
            var companyNames = new[]
            {
                   "American Airlines",
                    "Delta Air Lines",
                    "Southwest Airlines",
                    "United Airlines",
                    "Alaska Airlines",
                    "JetBlue Airways",
                    "Spirit Airlines",
                    "Hawaiian Airlines",
                    "Frontier Airlines"
            };
            var index = rnd.Next(companyNames.Length);
            return companyNames[index];
        }


        // Generate a random takeoff time
        private static string GenerateRandomTakeoffTime(string landing)
        {
            // Split the landing time string by the colon separator
            var landingComponents = landing.Split(':');

            // Get the hour and minute components of the landing time
            var landingHour = int.Parse(landingComponents[0]);
            var landingMinute = int.Parse(landingComponents[1]);

            // Generate a random hour between 0 and the landing hour (inclusive)
            var hour = rnd.Next(0, landingHour + 1);

            // Generate a random minute between 0 and 59 (inclusive)
            var minute = rnd.Next(0, 60);

            // If the generated hour is equal to the landing hour,
            // make sure the generated minute is earlier than the landing minute
            if (hour == landingHour && minute >= landingMinute)
            {
                minute = rnd.Next(0, landingMinute);
            }

            // Convert the hour and minute to strings
            var hourString = hour.ToString("D2");
            var minuteString = minute.ToString("D2");

            return $"{hourString}:{minuteString}";
        }

        // Generate a random landing time
        private static string GenerateRandomLandingTime()
        {
            var hour = rnd.Next(0, 24).ToString("D2");
            var minute = rnd.Next(0, 60).ToString("D2");
            return $"{hour}:{minute}";
        }


        static string GenerateRandomDate(int year, int month)
        {
            // Create a new Random object
            Random random = new Random();

            // Get the number of days in the specified month
            int daysInMonth = DateTime.DaysInMonth(year, month);

            // Generate a random day in the specified month
            int day = random.Next(1, daysInMonth + 1);

            // Return the random date as a string in the desired format
            return day.ToString("D2") + "/" + month.ToString("D2") + "/" + year.ToString("D4");
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
            string pushId = "NKALbgxH7-tSrvO3_Kc";
            int id = 86377;
            ViewData["PassengersCount"] = search.Passengers;

            var firebaseClient = new FirebaseClient(FirbaseLink);
            string yearMonth;

            
            if (search.Departure.Month < 10)
            {
                yearMonth = search.Departure.Year.ToString() + "-0" + search.Departure.Month.ToString();
            }
            else
            {
                yearMonth = search.Departure.Year.ToString() + "-" + search.Departure.Month.ToString();

            }

            var dbPlanes = await firebaseClient.Child("Planes")
                .Child(search.Origin)
                .Child(search.Destination)
                .Child(yearMonth)
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
                .Child(yearMonth)
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
            string yearMonth;
            yearMonth = search.yearMonth(2);
            
            var dbPlanes = await firebaseClient.Child("Planes")
                .Child(search.Destination)
                .Child(search.Origin)
                .Child(yearMonth)
                .OnceAsync<Planes>();

            foreach (var plane in dbPlanes)
            {
                if (plane.Object.Capacity - plane.Object.BookedSeats >= search.Passengers)
                    BackPlaneslist.Add(plane.Object);
            }
            string previousyearMonth=search.yearMonth(1);
           
            string id = search.IdGo.ToString();
            var onePlanes = await firebaseClient.Child("Planes")
           .Child(search.Origin)
           .Child(search.Destination)
           .Child(previousyearMonth)
           .Child(search.IdGo.ToString())
           .OnceSingleAsync<Planes>();

            onePlanes.TotalPrice(search.Passengers);
            ViewBag.BackPlaneslist = BackPlaneslist;
            ViewBag.SearchResults = search;
            ViewBag.onePlanes = onePlanes;

            return View();
        }
    }

}