using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Firebase.Database;
using FlyWithMe.Models;

namespace FlyWithMe.Controllers
{
    public class BookingController : Controller
    {
  
        public ActionResult PassengersInfo(int? id, int? count)
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

    }
}