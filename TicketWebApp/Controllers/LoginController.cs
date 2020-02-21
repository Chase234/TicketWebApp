using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketWebApp.Models;

namespace TicketWebApp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult IndexLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(TicketWebApp.Models.UserLogin userModel)
        {
            using (WebTicketsEntities db = new WebTicketsEntities())
            {
                var userDetails = db.UserLogins.Where(x => x.UserName == userModel.UserName && x.Password == userModel.Password).FirstOrDefault();
                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Wrong Username or Password";
                        return View("IndexLogin", userModel);
                }
                else
                {
                    Session["UserID"] = userDetails.UserID;
                    Session["Customer"] = userDetails.Customer;

                    if (userDetails.Customer == 1)
                    {
                        return RedirectToAction("Index", "Home");

                    }

                };
            }
               
                return View();

        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("IndexLogin", "Login");
        }

    }
}