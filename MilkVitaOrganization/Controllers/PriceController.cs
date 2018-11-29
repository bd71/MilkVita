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
    public class PriceController : Controller
    {
        private MilkVitaEntities db = new MilkVitaEntities();

        // GET: /Price/
        public ActionResult Index()
        {
            var tbl_price = db.tbl_Price.Include(t => t.tbl_ItemMaster).Include(t => t.tbl_Unit);
            return View(tbl_price.ToList());
        }

        // GET: /Price/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Price tbl_price = db.tbl_Price.Find(id);
            if (tbl_price == null)
            {
                return HttpNotFound();
            }
            return View(tbl_price);
        }

        // GET: /Price/Create
        public ActionResult Create()
        {
            ViewBag.itemMasterId = new SelectList(db.tbl_ItemMaster, "id", "name");
            ViewBag.unitId = new SelectList(db.tbl_Unit, "id", "shortCode");
            return View();
        }

        // POST: /Price/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,unitId,itemMasterId,price")] tbl_Price tbl_price)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Price.Add(tbl_price);
                ViewBag.message = "Price Inserted Succesfully";
                db.SaveChanges();


                //return View(tbl_price);
                return RedirectToAction("Index");
            }

            ViewBag.itemMasterId = new SelectList(db.tbl_ItemMaster, "id", "name", tbl_price.itemMasterId);
            ViewBag.unitId = new SelectList(db.tbl_Unit, "id", "shortCode", tbl_price.unitId);
            return View(tbl_price);
        }

        // GET: /Price/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Price tbl_price = db.tbl_Price.Find(id);
            if (tbl_price == null)
            {
                return HttpNotFound();
            }
            ViewBag.itemMasterId = new SelectList(db.tbl_ItemMaster, "id", "name", tbl_price.itemMasterId);
            ViewBag.unitId = new SelectList(db.tbl_Unit, "id", "shortCode", tbl_price.unitId);
            return View(tbl_price);
        }

        // POST: /Price/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,unitId,itemMasterId,price")] tbl_Price tbl_price)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_price).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.itemMasterId = new SelectList(db.tbl_ItemMaster, "id", "name", tbl_price.itemMasterId);
            ViewBag.unitId = new SelectList(db.tbl_Unit, "id", "shortCode", tbl_price.unitId);
            return View(tbl_price);
        }

        // GET: /Price/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Price tbl_price = db.tbl_Price.Find(id);
            if (tbl_price == null)
            {
                return HttpNotFound();
            }
            return View(tbl_price);
        }

        [HttpPost]
        public JsonResult DeleteConfirmed(int id)
        {
            string success = string.Empty;
            // return Json(success, JsonRequestBehavior.AllowGet);
            tbl_Price tbl_price = db.tbl_Price.Find(id);
            db.tbl_Price.Remove(tbl_price);
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
