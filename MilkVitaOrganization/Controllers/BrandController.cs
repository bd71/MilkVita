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
    public class BrandController : Controller
    {
        private MilkVitaEntities db = new MilkVitaEntities();

        // GET: /Brand/
        public ActionResult Index()
        {
            return View(db.tbl_Brand.ToList());
        }

        // GET: /Brand/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Brand tbl_brand = db.tbl_Brand.Find(id);
            if (tbl_brand == null)
            {
                return HttpNotFound();
            }
            return View(tbl_brand);
        }

        // GET: /Brand/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Brand/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,name")] tbl_Brand tbl_brand)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Brand.Add(tbl_brand);
                ViewBag.message = "Brand Inserted Succesfully";
                db.SaveChanges();
         
                ModelState.Clear();
                return View();
            }

            return View(tbl_brand);
        }

        // GET: /Brand/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Brand tbl_brand = db.tbl_Brand.Find(id);
            if (tbl_brand == null)
            {
                return HttpNotFound();
            }
            return View(tbl_brand);
        }

        // POST: /Brand/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,name")] tbl_Brand tbl_brand)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_brand).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_brand);
        }

        // GET: /Brand/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Brand tbl_brand = db.tbl_Brand.Find(id);
            if (tbl_brand == null)
            {
                return HttpNotFound();
            }
            return View(tbl_brand);
        }

        [HttpPost]
        public JsonResult DeleteConfirmed(int id)
        {
            string success = string.Empty;
            // return Json(success, JsonRequestBehavior.AllowGet);

            tbl_Brand tbl_brand = db.tbl_Brand.Find(id);
            db.tbl_Brand.Remove(tbl_brand);
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
