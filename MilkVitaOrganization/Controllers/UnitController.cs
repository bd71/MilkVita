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
    public class UnitController : Controller
    {
        private MilkVitaEntities db = new MilkVitaEntities();

        // GET: /Unit/
        public ActionResult Index()
        {
            return View(db.tbl_Unit.ToList());
        }

        // GET: /Unit/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Unit tbl_unit = db.tbl_Unit.Find(id);
            if (tbl_unit == null)
            {
                return HttpNotFound();
            }
            return View(tbl_unit);
        }

        // GET: /Unit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Unit/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,name,shortCode,convertParameter")] tbl_Unit tbl_unit)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Unit.Add(tbl_unit);
                ViewBag.message = "Unit Inserted Succesfully";
                db.SaveChanges();

                ModelState.Clear();
                return View();
                //return RedirectToAction("Index");
            }

            return View(tbl_unit);
        }

        // GET: /Unit/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Unit tbl_unit = db.tbl_Unit.Find(id);
            if (tbl_unit == null)
            {
                return HttpNotFound();
            }
            return View(tbl_unit);
        }

        // POST: /Unit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,name,shortCode,convertParameter")] tbl_Unit tbl_unit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_unit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_unit);
        }

        // GET: /Unit/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Unit tbl_unit = db.tbl_Unit.Find(id);
            if (tbl_unit == null)
            {
                return HttpNotFound();
            }
            return View(tbl_unit);
        }

        [HttpPost]
        public JsonResult DeleteConfirmed(int id)
        {
            string success = string.Empty;
            // return Json(success, JsonRequestBehavior.AllowGet);
            tbl_Unit tbl_unit = db.tbl_Unit.Find(id);
            db.tbl_Unit.Remove(tbl_unit);
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
