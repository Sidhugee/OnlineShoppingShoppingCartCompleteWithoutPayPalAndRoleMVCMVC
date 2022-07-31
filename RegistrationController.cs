using OnlineShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineShopping.Controllers
{
    [AllowAnonymous]
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Models.Membership model)
        {
            using(var Entities = new Entities())
            {
              bool IsValid =  Entities.Users.Any(x=>x.Username == model.Username && x.Password == model.Password);
                if (IsValid)
                {
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    return RedirectToAction("Index", "Product");
                }
                ModelState.AddModelError("","Invalid Username And Password");
                return View();
            }
        }
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(User model)
        {
            using (var Entities = new Entities())
            {
                Entities.Users.Add(model);
                Entities.SaveChanges();
            }
            return RedirectToAction("Login");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}