using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        private List<Planes> planeslist = new List<Planes>();

        public ActionResult Index()
        {
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

        
        public ActionResult Search()
        {
            return View();
        }
       

        [HttpPost]
        public async Task<ActionResult> Finding(SearchModel search)
        {
            if (search == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewData["Origin"] = Request["Origin"];
            ViewData["Destination"] = Request["Destination"];

            ViewData["Departure"] = Request["Departure"];
            ViewData["Return"] = Request["Return"];

            var firebaseClient = new FirebaseClient(FirbaseLink);
            var dbPlanes = await firebaseClient.Child("Planes").OnceAsync<Planes>();
            // await firebaseClient.Child("Planes").PostAsync(new Planes(10,300, "El-Al",DateTime.Now.D);

            foreach (var plane in dbPlanes)
            {

                if (plane.Object.from == search.Origin)
                    if (plane.Object.to == search.Destination)
                        //if (plane.Object.departureDate.Equals(search.Departure))
                        //    if (plane.Object.returnDate.Equals(search.Return))
                                if (plane.Object.capacity - plane.Object.bookedSeats >= search.Passengers)
                            planeslist.Add(plane.Object);

            }

            ViewBag.Planes=planeslist;
            ViewBag.SearchResults = search;

            return View();
        }


        public async Task<ActionResult> Buying(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var firebaseClient = new FirebaseClient(FirbaseLink);
            var dbPlanes = await firebaseClient.Child("Planes").OnceAsync<Planes>();
            Planes selectPlane;
            foreach (var plane in dbPlanes)
                if(plane.Object.id== id)
                {
                    selectPlane=plane.Object;
                    break;
                }

            return View();
        }

    }

}