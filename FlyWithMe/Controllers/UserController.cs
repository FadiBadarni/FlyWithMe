using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Windows.Input;
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

        private static readonly byte[] Key = Encoding.UTF8.GetBytes("mysecretkey12345"); // Key should be a byte array of the desired key size (16, 24, or 32 bytes for 128, 192, or 256-bit key size)
        private static readonly byte[] Iv = Encoding.UTF8.GetBytes("1234567812345678"); // Initialization vector should be a byte array of 16 bytes
        public static string Decrypt(string encrypted)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = Iv;
                using (var decryptor = aes.CreateDecryptor())
                {
                    var inputBytes = Convert.FromBase64String(encrypted);
                    var outputBytes = decryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);
                    return Encoding.UTF8.GetString(outputBytes);
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult> ViewOrders(loginModel loginModel) { 

            var firebaseClient = new FirebaseClient(FirbaseLink);
            int order = (loginModel.OrderID/1000);
            int fOrder=await firebaseClient.Child("Payment").Child(loginModel.ID + "").Child("PaymentID").OnceSingleAsync<int>();

            if (fOrder != order)
                return Redirect("/User/Login");
         

            var listPaymentFirebase = await firebaseClient.Child("Payment")
                .Child(loginModel.ID + "")
                .Child("Paid").OnceAsListAsync<PaymentFirebase>();
            List<PaymentFirebase> list = new List<PaymentFirebase>();
            foreach(var payment in listPaymentFirebase)
            {
                list.Add(payment.Object);
            }


            foreach (PaymentFirebase p in list)
            {
                if(p.PaymentInfo != null)
                if (p.PaymentInfo.CardNumber != null && p.PaymentInfo.CardNumber.Length > 16)
                {
                    p.PaymentInfo.CardNumber = Decrypt(p.PaymentInfo.CardNumber);
                }
            }


            ViewBag.listPaymentFirebase = list;
            ViewBag.OrderID=order*1000;

            return View();
        }
    }
}