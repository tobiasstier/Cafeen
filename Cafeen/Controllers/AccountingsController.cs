using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cafeen.Models;
using Cafeen.ViewModels;
using System.Globalization;

namespace Cafeen.Controllers
{
    public class AccountingsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        private ProductContext db2 = new ProductContext();

        // GET: Accountings
        public ActionResult Index()
        {
            return View(db.Accountings.ToList());
        }

        // GET: Accountings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accounting accounting = db.Accountings.Find(id);
            if (accounting == null)
            {
                return HttpNotFound();
            }
            return View(accounting);
        }

        // GET: Accountings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Accountings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StartCash,EndCash,CardCash,Timestamp")] Accounting accounting)
        {
            if (ModelState.IsValid)
            {
                db.Accountings.Add(accounting);
                accounting.Timestamp = DateTime.Now;
                accounting.StartProduct = ProductToStringParser();
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accounting);
        }

        // GET: Accountings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accounting accounting = db.Accountings.Find(id);
            if (accounting == null)
            {
                return HttpNotFound();
            }
            return View(accounting);
        }

        // POST: Accountings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StartCash,EndCash,CardCash,Timestamp")] Accounting accounting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accounting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accounting);
        }

        // GET: Accountings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accounting accounting = db.Accountings.Find(id);
            if (accounting == null)
            {
                return HttpNotFound();
            }
            return View(accounting);
        }

        // POST: Accountings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Accounting accounting = db.Accountings.Find(id);
            db.Accountings.Remove(accounting);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Closes databases after they've been used.
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
                db2.Dispose();
            }
            base.Dispose(disposing);
        }
        
        //Used by the Receipt View to display the data from Accountings table.
        public ActionResult Receipt(int id)
        {
            var tblAccountings = from s in db.Accountings
                              .Where(t => t.Id == id)
                                 select s;

            //Creates a new object as defined in ViewModel folder. 
            //It is used to display the data in the Receipt View.
            var receipt = new AccountingsReceipt
            {
                Id = (from a in tblAccountings select a.Id).First(),
                StartCash = (from a in tblAccountings select a.StartCash).First(),
                EndCash = (from a in tblAccountings select a.EndCash).First(),
                CardCash = (from a in tblAccountings select a.CardCash).First(),
                Timestamp = (from a in tblAccountings select a.Timestamp).First(),
                LockStatus = (from a in tblAccountings select a.LockStatus).First(),
                StartProduct = ProductStringToList((from a in tblAccountings select a.StartProduct).First()),
                EndProduct = ProductStringToList((from a in tblAccountings select a.EndProduct).First())
            };
            return View(receipt);
        }

        //When the button in the Index View is pressed, the LockStatus is either set to true or false
        //Depending on its' current value. When the value is set to true, EndProduct in Accounting
        //table is filled with the items currently in tblProducts (inventory).

        public ActionResult Lock(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var accounting = db.Accountings.Find(id);
            if (accounting == null)
            {
                return HttpNotFound();
            }
            if (accounting.LockStatus)
            {
                accounting.LockStatus = false;
            }

            return View(accounting);
        }

        //Returns all the products in tblProduct table as a string on the
        //form: id,name,cat,qty,price;id,name,cat,qty,price; ...
//        public string ProductToStringParser(int id)
//        {
//            var tblProducts = from s in db2.tblProducts.Include(t => t.tblCategory)
//                              select s;
//            string productString = "";
//            foreach (var item in tblProducts)
//            {
//                productString = productString +
//                    string.Join(",", item.Id.ToString(), item.Name, item.tblCategory.CategoryName, item.Qty.ToString(), item.tblCategory.Price.ToString()) + ";"
//            }
//           }

//            else
//            {
//                accounting.LockStatus = true;
//                accounting.EndProduct = ProductToStringParser();
//    }
//            db.SaveChanges();
//            return RedirectToAction("Index");
//}

//Returns all the products in tblProduct table as a string on the
//form: id:name:cat:qty:price;id:name:cat:qty:price; ... ;
        public string ProductToStringParser()
        {
            var tblProducts = from s in db2.tblProducts.Include(t => t.tblCategory)
                              select s;
            var productString = string.Empty;

            foreach (var item in tblProducts)
            {
                productString = productString +
                    string.Join(":", item.Id.ToString(), item.Name, item.tblCategory.CategoryName, item.Qty.ToString(), item.tblCategory.Price.ToString()) + ";";
            }
            return productString;
        }

        //Takes a string on the form: form: id:name:cat:qty:price;id:name:cat:qty:price; ... ;
        //And converts it to a list of Product Objects (See Product in  ViewModels folder).
        public static List<Product> ProductStringToList(string productString)
        {
            List<Product> productList = new List<Product>();
            productString = productString.Remove(productString.Length - 1);
            var ps = productString.Split(';');
            foreach (var item in ps)
            {
                var ps1 = item.Split(':');
                productList.Add(new Product { Id = Int32.Parse(ps1[0]), Name = ps1[1], Category = ps1[2], Qty = Int32.Parse(ps1[3]), Price = Decimal.Parse(ps1[4]) });
            }
            return productList;
        }
    }
}
