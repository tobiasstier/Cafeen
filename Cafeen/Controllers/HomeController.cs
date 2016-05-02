using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cafeen.Models;

namespace Cafeen.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult History()
        {

            DatabaseContext db = new DatabaseContext();
            List<History> history = db.Histories.ToList();

            return View(history);
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}