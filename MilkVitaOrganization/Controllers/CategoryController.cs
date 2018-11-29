using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MilkVitaOrganization.Models;

namespace MilkVitaOrganization.Controllers
{
    public class CategoryController : Controller
    {
        private MilkVitaEntities db = new MilkVitaEntities();

        // GET: /Category/
        public ActionResult Index()
        {
            var tbl_category = db.tbl_Category.Include(t => t.tbl_Category2);
            return View(tbl_category.ToList());
        }

        // GET: /Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Category tbl_category = db.tbl_Category.Find(id);
            if (tbl_category == null)
            {
                return HttpNotFound();
            }
            return View(tbl_category);
        }

        // GET: /Category/Create
        public ActionResult Create()
        {
            ViewBag.categoryId = new SelectList(db.tbl_Category, "id", "name");
            return View();
        }

        // POST: /Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,code,name,categoryId")] tbl_Category tbl_category)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Category.Add(tbl_category);
                ViewBag.message = "Category Inserted Succesfully";
                db.SaveChanges();

                //ModelState.Clear();
                //return View();
                return RedirectToAction("Index");
            }

            ViewBag.categoryId = new SelectList(db.tbl_Category, "id", "name", tbl_category.categoryId);
            return View(tbl_category);
        }

        // GET: /Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Category tbl_category = db.tbl_Category.Find(id);
            if (tbl_category == null)
            {
                return HttpNotFound();
            }
            ViewBag.categoryId = new SelectList(db.tbl_Category, "id", "name", tbl_category.categoryId);
            return View(tbl_category);
        }

        // POST: /Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,code,name,categoryId")] tbl_Category tbl_category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categoryId = new SelectList(db.tbl_Category, "id", "name", tbl_category.categoryId);
            return View(tbl_category);
        }

        // GET: /Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Category tbl_category = db.tbl_Category.Find(id);
            if (tbl_category == null)
            {
                return HttpNotFound();
            }
            return View(tbl_category);
        }

        [HttpPost]
        public JsonResult DeleteConfirmed(int id)
        {
            string success = string.Empty;
            // return Json(success, JsonRequestBehavior.AllowGet);
            tbl_Category tbl_category = db.tbl_Category.Find(id);
            db.tbl_Category.Remove(tbl_category);
            int result = db.SaveChanges();

            if (result > 0)
            {
                success = "Data delete successfully";
            }
            //  return Json(success, JsonRequestBehavior.AllowGet);

            return Json(new { result = success });

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
