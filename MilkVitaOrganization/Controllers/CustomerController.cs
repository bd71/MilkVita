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
    public class CustomerController : Controller
    {
        private MilkVitaEntities db = new MilkVitaEntities();

        // GET: /Customer/
        public ActionResult Index()
        {
            return View(db.tbl_Customer.ToList());
        }

        // GET: /Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Customer tbl_customer = db.tbl_Customer.Find(id);
            if (tbl_customer == null)
            {
                return HttpNotFound();
            }
            return View(tbl_customer);
        }

        // GET: /Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,code,name,mobile,email,address")] tbl_Customer tbl_customer)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Customer.Add(tbl_customer);
                ViewBag.message = "Customer Inserted Succesfully";
                db.SaveChanges();

                ModelState.Clear();
                return View();
            }

            return View(tbl_customer);
        }

        // GET: /Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Customer tbl_customer = db.tbl_Customer.Find(id);
            if (tbl_customer == null)
            {
                return HttpNotFound();
            }
            return View(tbl_customer);
        }

        // POST: /Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,code,name,mobile,email,address")] tbl_Customer tbl_customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_customer);
        }

        // GET: /Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Customer tbl_customer = db.tbl_Customer.Find(id);
            if (tbl_customer == null)
            {
                return HttpNotFound();
            }
            return View(tbl_customer);
        }

        [HttpPost]
        public JsonResult DeleteConfirmed(int id)
        {
            string success = string.Empty;
            // return Json(success, JsonRequestBehavior.AllowGet);
            tbl_Customer tbl_customer = db.tbl_Customer.Find(id);
            db.tbl_Customer.Remove(tbl_customer);
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
