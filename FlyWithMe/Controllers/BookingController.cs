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
        // GET: Booking
        public ActionResult Index()
        {
            return View();
        }

      
        public ActionResult InsertData(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }

        public ActionResult Buying()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Buying(InsertDataModel insertDataModel)
        {

            return View();
        }

    }
}