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
    public class ColorController : Controller
    {
        private MilkVitaEntities db = new MilkVitaEntities();

        // GET: /Color/
        public ActionResult Index()
        {
            return View(db.tbl_Color.ToList());
        }

        // GET: /Color/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Color tbl_color = db.tbl_Color.Find(id);
            if (tbl_color == null)
            {
                return HttpNotFound();
            }
            return View(tbl_color);
        }

        // GET: /Color/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Color/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,name")] tbl_Color tbl_color)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Color.Add(tbl_color);
                db.SaveChanges();
                ViewBag.message = "Color Inserted Succesfully"; 
                return View(tbl_color);
            }
            
            return View(tbl_color);
        }

        // GET: /Color/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Color tbl_color = db.tbl_Color.Find(id);
            if (tbl_color == null)
            {
                return HttpNotFound();
            }
            return View(tbl_color);
        }

        // POST: /Color/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,name")] tbl_Color tbl_color)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_color).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_color);
        }

        // GET: /Color/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Color tbl_color = db.tbl_Color.Find(id);
            if (tbl_color == null)
            {
                return HttpNotFound();
            }
            return View(tbl_color);
        }

        [HttpPost]
        public JsonResult DeleteConfirmed(int id)
        {
            string success = string.Empty;
            // return Json(success, JsonRequestBehavior.AllowGet);
            tbl_Color tbl_color = db.tbl_Color.Find(id);
            db.tbl_Color.Remove(tbl_color);
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
