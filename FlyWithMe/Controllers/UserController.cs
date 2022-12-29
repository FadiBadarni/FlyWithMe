using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Firebase.Database;
using Firebase.Database.Query;
using FlyWithMe.Class;
using FlyWithMe.Models;
using Microsoft.Ajax.Utilities;
using PayPal.Api;

namespace FlyWithMe.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        private static string FirbaseLink = "https://myflight-db2b1-default-rtdb.firebaseio.com";

        public ActionResult Index()
        {
            return View();
        }
        public  ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ViewOrders(loginModel loginModel) { 

            var firebaseClient = new FirebaseClient(FirbaseLink);
            string order = (loginModel.OrderID/100).ToString();
            string email = await firebaseClient.Child("Payment").Child(loginModel.ID + "").Child("Paid")
                .Child(loginModel.OrderID + "").Child("passengers").Child("0").Child("Email")
                  .OnceSingleAsync<string>();
            //if(loginModel.Email!=email)
            //    return  Redirect("/User/Login"); 

            var listPaymentFirebase = await firebaseClient.Child("Payment")
                .Child(loginModel.ID + "")
                .Child("Paid").OnceAsListAsync<PaymentFirebase>();
            List<PaymentFirebase> list = new List<PaymentFirebase>();
            foreach(var payment in listPaymentFirebase)
            {
                list.Add(payment.Object);
            }

            ViewBag.listPaymentFirebase = list;
          

            return View();
        }
    }
}