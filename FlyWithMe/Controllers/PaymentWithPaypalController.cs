using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
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
using PayPal.Api;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Payment = PayPal.Api.Payment;

namespace FlyWithMe.Controllers
{
    public class PaymentWithPaypalController : Controller
    {
        private static string FirbaseLink = "https://myflight-db2b1-default-rtdb.firebaseio.com";

        // GET: PaymentWithPaypal
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult PaymentWithPaypal(Payment p, string plane1, string plane2, string searching, string Id)
        {


            List<Planes> pPlanes = new List<Planes>();

            Planes goPlane = new Planes();
            goPlane.bulidPlaneFromSring(plane1);
            pPlanes.Add(goPlane);

            if (plane2 != "-1")
            {
                Planes backPlane = new Planes();
                backPlane.bulidPlaneFromSring(plane2);
                pPlanes.Add(backPlane);
            }


            Searching searching1 = new Searching();
            searching1.bulidSearchingFromSring(searching);

            //getting the apiContext
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal
                //Payer Id will be returned when payment proceeds or click to pay

                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/PaymentWithPaypal/PaymentWithPayPal?";
                    var guid = Convert.ToString((new Random()).Next(100000));
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid, pPlanes, searching1, Id);
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);

                }
                else
                {
                    // This function exectues after receving all parameters for the payment
                    var guid = Request.Params["guid"];

                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    //If executed payment failed then we will show payment failure message to user
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }

            }
            catch (Exception ex)
            {
                //return View("FailureView");
              
                string idcase = Request.Params["Id"];
                return Redirect("/PaymentWithPaypal/SuccessView?id=" + idcase);
                //    return View("SuccessView");

            }
            //on successful payment, show success page to user.

            string id = Request.Params["Id"];
            return Redirect("/PaymentWithPaypal/SuccessView?id=" + id);
            //  return View("SuccessView");
        }


        private PayPal.Api.Payment payment;
        private PayPal.Api.Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        private PayPal.Api.Payment CreatePayment(APIContext apiContext, string redirectUrl, List<Planes> pPlanes, Searching searching, string Id)
        {
            //create itemlist and add item objects to it
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            //Adding Item Details like name, currency, price etc
            int sum = 0;
            foreach (var item in pPlanes)
            {
                itemList.items.Add(new Item()
                {
                    name = "Plane Ticket",
                    currency = "ILS",
                    price = item.Price/ searching.Passengers + "",
                    quantity = searching.Passengers + "",
                    sku = "sku"
                });
                sum += (item.Price);
            }


            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl + "&Id=" + Id
            };
            // Adding Tax, shipping and Subtotal details
            var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = sum + ""
            };
            //Final amount with details
            var amount = new Amount()
            {
                currency = "ILS",
                total = (sum).ToString(), // Total must be equal to sum of tax, shipping and subtotal.
                details = details
            };

            var transactionList = new List<Transaction>();
            // Adding description about the transaction
            transactionList.Add(new Transaction()
            {
                description = "Transaction Description",
                invoice_number = "your generated invoice number", //Generate an Invoice No
                amount = amount,
                item_list = itemList
            });
            this.payment = new PayPal.Api.Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext
            return this.payment.Create(apiContext);
        }

        private static readonly byte[] Key = Encoding.UTF8.GetBytes("mysecretkey12345"); // Key should be a byte array of the desired key size (16, 24, or 32 bytes for 128, 192, or 256-bit key size)
        private static readonly byte[] Iv = Encoding.UTF8.GetBytes("1234567812345678"); // Initialization vector should be a byte array of 16 bytes
        public static string Encrypt(string text)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = Iv;
                using (var encryptor = aes.CreateEncryptor())
                {
                    var inputBytes = Encoding.UTF8.GetBytes(text);
                    var outputBytes = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);
                    return Convert.ToBase64String(outputBytes);
                }
            }
        }

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













        public async Task<ActionResult> SuccessView(PaymentModel p, string id)
        {



            var firebaseClient = new FirebaseClient(FirbaseLink);

            int CounterID = await firebaseClient
                  .Child("Payment")
                  .Child("CounterID")
                  .OnceSingleAsync<int>();

            CounterID++;
          
            await firebaseClient.Child("Payment")
              .Child("CounterID").PutAsync(CounterID);


            if (id == null)
                id = Request.Params["id"];


            
           
            PaymentFirebase paymentFirebase = await firebaseClient
                  .Child("Payment")
                  .Child(id).Child("Unpaid")
                  .OnceSingleAsync<PaymentFirebase>();

            var listPaymentFirbase = await firebaseClient
                .Child("Payment")
                .Child(id).Child("Paid").OnceAsListAsync<PaymentFirebase>();

            List<PaymentFirebase> list = new List<PaymentFirebase>();

            foreach (var item in listPaymentFirbase)
            {
                list.Add(item.Object);
            }
           
               
                


          //  listPaymentFirbase.List.Add(paymentFirebase);

            if (p.CardNumber != null)
            {
                paymentFirebase.PaymentInfo = p;
                p.CardNumber = Encrypt(p.CardNumber);
            }

            list.Add(paymentFirebase);


            await firebaseClient.Child("Payment")
                 .Child(id).Child("Paid").PutAsync(list);


            if(p.CardNumber!=null && p.CardNumber.Length > 16)
            {
                p.CardNumber=Decrypt(p.CardNumber);
            }
            Random rnd = new Random();


            ViewBag.PaymentFirebase = paymentFirebase;
            ViewBag.OrderId = CounterID+ listPaymentFirbase.Count;
            ViewBag.random = rnd.Next(100000000, 1000000000);

            return View();
        }

        public ActionResult FailureView()
        {
            return View();
        }
    }
}