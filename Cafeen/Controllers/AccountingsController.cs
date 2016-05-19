using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cafeen.Models;

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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
                db2.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Receipt(int id)
        {

            var tblAccountings = from s in db.Accountings
                              .Where (t => t.Id == id)
                              select s;

            return View(tblAccountings);
        }

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
            else
            {
                accounting.LockStatus = true;
                accounting.EndProduct = ProductToStringParser();
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Returns all the products in tblProduct table as a string on the
        //form: id,name,cat,qty,price;id,name,cat,qty,price; ...
        public string ProductToStringParser ()
        {
            var tblProducts = from s in db2.tblProducts.Include(t => t.tblCategory)
                              select s;
            string productString = "";
            foreach (var item in tblProducts)
            {
                productString = productString +
                    string.Join(",", item.Id.ToString(), item.Name, item.tblCategory.CategoryName, item.Qty.ToString(), item.tblCategory.Price.ToString()) + ";";
            }
            return productString;
        }
    }
}
