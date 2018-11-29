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
    public class SupplierController : Controller
    {
        private MilkVitaEntities db = new MilkVitaEntities();

        // GET: /Supplier/
        public ActionResult Index()
        {
            return View(db.tbl_Supplier.ToList());
        }

        // GET: /Supplier/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Supplier tbl_supplier = db.tbl_Supplier.Find(id);
            if (tbl_supplier == null)
            {
                return HttpNotFound();
            }
            return View(tbl_supplier);
        }

        // GET: /Supplier/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Supplier/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,code,name,mobile,email,address")] tbl_Supplier tbl_supplier)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Supplier.Add(tbl_supplier);
                ViewBag.message = "Supplier Added Succesfully";
                db.SaveChanges();

                ModelState.Clear();
                return View();
                //return RedirectToAction("Index");
            }

            return View(tbl_supplier);
        }

        // GET: /Supplier/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Supplier tbl_supplier = db.tbl_Supplier.Find(id);
            if (tbl_supplier == null)
            {
                return HttpNotFound();
            }
            return View(tbl_supplier);
        }

        // POST: /Supplier/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,code,name,mobile,email,address")] tbl_Supplier tbl_supplier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_supplier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_supplier);
        }

        // GET: /Supplier/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Supplier tbl_supplier = db.tbl_Supplier.Find(id);
            if (tbl_supplier == null)
            {
                return HttpNotFound();
            }
            return View(tbl_supplier);
        }

        [HttpPost]
        public JsonResult DeleteConfirmed(int id)
        {
            string success = string.Empty;
            // return Json(success, JsonRequestBehavior.AllowGet);
            tbl_Supplier tbl_supplier = db.tbl_Supplier.Find(id);
            db.tbl_Supplier.Remove(tbl_supplier);
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
