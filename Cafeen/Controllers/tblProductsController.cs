using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cafeen.Models;
using PagedList;

namespace Cafeen.Controllers
{
    public class tblProductsController : Controller
    {
        private ProductContext db = new ProductContext();

        // GET: tblProducts
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";
            ViewBag.QtySortParm = sortOrder == "Qty" ? "qty_desc" : "Qty";
            ViewBag.CategorySortParm = sortOrder == "Category" ? "category_desc" : "Category";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var tblProducts = from s in db.tblProducts.Include(t => t.tblCategory)
                              select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                tblProducts = tblProducts.Where(s => s.Name.Contains(searchString)
                || s.tblCategory.CategoryName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    tblProducts = tblProducts.OrderByDescending(s => s.Name);
                    break;
                case "Category":
                    tblProducts = tblProducts.OrderBy(s => s.tblCategory.CategoryName);
                    break;
                case "category_desc":
                    tblProducts = tblProducts.OrderByDescending(s => s.tblCategory.CategoryName);
                    break;
                case "Price":
                    tblProducts = tblProducts.OrderBy(s => s.tblCategory.Price);
                    break;
                case "price_desc":
                    tblProducts = tblProducts.OrderByDescending(s => s.tblCategory.Price);
                    break;
                case "Qty":
                    tblProducts = tblProducts.OrderBy(s => s.Qty);
                    break;
                case "qty_desc":
                    tblProducts = tblProducts.OrderByDescending(s => s.Qty);
                    break;
                default:
                    tblProducts = tblProducts.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(tblProducts.ToPagedList(pageNumber, pageSize));
        }

        // GET: tblProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProduct tblProduct = db.tblProducts.Find(id);
            if (tblProduct == null)
            {
                return HttpNotFound();
            }
            return View(tblProduct);
        }

        // GET: tblProducts/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.tblCategories, "Id", "CategoryName");
            return View();
        }

        // POST: tblProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Qty,CategoryId")] tblProduct tblProduct)
        {
            if (ModelState.IsValid)
            {
                db.tblProducts.Add(tblProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.tblCategories, "Id", "CategoryName", tblProduct.CategoryId);
            return View(tblProduct);
        }

        // GET: tblProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProduct tblProduct = db.tblProducts.Find(id);
            if (tblProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.tblCategories, "Id", "CategoryName", tblProduct.CategoryId);
            return View(tblProduct);
        }

        // POST: tblProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Qty,CategoryId")] tblProduct tblProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.tblCategories, "Id", "CategoryName", tblProduct.CategoryId);
            return View(tblProduct);
        }

        // GET: tblProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProduct tblProduct = db.tblProducts.Find(id);
            if (tblProduct == null)
            {
                return HttpNotFound();
            }
            return View(tblProduct);
        }

        // POST: tblProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblProduct tblProduct = db.tblProducts.Find(id);
            db.tblProducts.Remove(tblProduct);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
