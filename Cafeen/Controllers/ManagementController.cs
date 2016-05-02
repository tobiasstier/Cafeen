using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cafeen.Controllers
{
    public class ManagementController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Product()
        {
            return View();
        }

        public ActionResult Category()
        {
            return View();
        }
    }
}