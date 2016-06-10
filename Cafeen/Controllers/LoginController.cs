using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cafeen.Controllers
{
    public class LoginController : Controller
    {
        [HttpPost]
        public ActionResult Index()
        {
            string username = "SU";
            string password = "Cafeen";

            
            if (Request.Form["usernameInput"] == username && Request.Form["passwordInput"] == password)
            {
                Session["valid"] = "valid";
                return RedirectToAction("Index","Home");
            }
            else
            {
                Session["valid"] = "Not valid";
                return View();
            }
        }
    }
}