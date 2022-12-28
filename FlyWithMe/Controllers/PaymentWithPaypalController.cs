using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
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

   
        public ActionResult PaymentWithPaypal(Payment p, string plane1, string plane2, string searching,string Id)
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
           
            
            Searching searching1= new Searching();
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
                    
                    //this section will be executed first because PayerID doesn't exist
                    //it is returned by the create function call of the payment class
                    // Creating a payment
                    // baseURL is the url on which paypal sendsback the data.
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/PaymentWithPaypal/PaymentWithPayPal?";
                //here we are generating guid for storing the paymentID received in session
                //which will be used in the payment execution
                var guid = Convert.ToString((new Random()).Next(100000));
                //CreatePayment function gives us the payment approval url
                //on which payer is redirected for paypal account payment
                var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid, pPlanes, searching1,Id);
                //get links returned from paypal in response to Create function call
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
        private PayPal.Api.Payment CreatePayment(APIContext apiContext, string redirectUrl,List<Planes> pPlanes,Searching searching,string Id)
        {
            //create itemlist and add item objects to it
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            //Adding Item Details like name, currency, price etc
            int sum = 0;
            foreach(var item in pPlanes)
            {
                 itemList.items.Add(new Item()
                {
                    name = "Go Plane",
                    currency = "USD",
                    price = item.Price+"",
                    quantity = searching.Passengers+"",
                    sku = "sku"
                });
                sum += item.Price* searching.Passengers;
            }
         
           
            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl + "&Id="+Id
            };
            // Adding Tax, shipping and Subtotal details
            var details = new Details()
            {
                tax = "1",
                shipping = "1",
                subtotal = sum + ""
            };
            //Final amount with details
            var amount = new Amount()
            {
                currency = "USD",
                total = (sum+ 2).ToString(), // Total must be equal to sum of tax, shipping and subtotal.
                details = details
            };

            var transactionList = new List<Transaction>();
            // Adding description about the transaction
            transactionList.Add(new Transaction()
            {
                description = "Transaction description",
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



        public  string sha256(string randomString)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
        

        public async Task<ActionResult> SuccessView(PaymentModel p,string id)
        {
            
            if(id == null) 
                id = Request.Params["id"];

           

            var firebaseClient = new FirebaseClient(FirbaseLink);
            PaymentFirebase  paymentFirebase = await firebaseClient
                  .Child("Payment")
                  .Child(id)
                  .OnceSingleAsync<PaymentFirebase>();

            //if (p != null)
            //{
            //    paymentFirebase.PaymentInfo = p;
            //    await firebaseClient.Child("Payment")
            //      .Child(id).PutAsync(paymentFirebase);
            //   // p.CardNumber = sha256(p.CardNumber);
            //}
            Random rnd = new Random();
          

            ViewBag.PaymentFirebase = paymentFirebase;
            ViewBag.random = rnd.Next(100000000, 1000000000);
            
            return View();
        }

        public ActionResult FailureView()
        {
            return View();
        }
    }
}