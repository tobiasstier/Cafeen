using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cafeen.Models;

namespace Cafeen.Controllers
{
    public class HistoryController : Controller
    {
        // GET: History
        public ActionResult Index()
        {

            DatabaseContext db = new DatabaseContext();


            List<History> history = db.Histories.ToList();



            return View(history);
        }
    }
}