using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Firebase.Database;
using Firebase.Database.Query;
using FlyWithMe.Models;

namespace FlyWithMe.Controllers
{
    public class UsersController : Controller
    {
       
        private static string FirbaseLink = "https://myflight-db2b1-default-rtdb.firebaseio.com";
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (model.Password != model.PasswordConfirm) return View();
            var firebaseClient = new FirebaseClient(FirbaseLink);
            var dbUsers = await firebaseClient.Child("Users").OnceAsync<RegisterModel>();
            foreach (var user in dbUsers)
            {

                if(user.Object.Email==model.Email) { return View(); }
            }
            var result = await firebaseClient.Child("Users").PostAsync(model);

           return Redirect("/Home"); 
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginModel model)
        {
           
            var firebaseClient = new FirebaseClient(FirbaseLink);
            var dbUsers = await firebaseClient.Child("Users").OnceAsync<RegisterModel>();
            foreach (var user in dbUsers)
            {

                if (user.Object.Email == model.Email &&
                    user.Object.Password == model.Password) { return Redirect("/Home"); }
              
            }
            return View();
        }

        



    }
}