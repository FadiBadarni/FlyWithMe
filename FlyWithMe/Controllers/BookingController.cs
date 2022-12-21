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
        public ActionResult PassengersInfo([Bind(Include = "IdGo,IdBack,Origin,Destination,Departure,Return,Class,Passengers")] Searching search)

        {
            Session["count"] = search.Passengers;
            return View();
        }

        public ActionResult Payment()
        {
            return View();
        }


    }
}