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
    public class DepartmentController : Controller
    {
        private MilkVitaEntities db = new MilkVitaEntities();

        // GET: /Department/
        public ActionResult Index()
        {
            return View(db.tbl_Department.ToList());
        }

        // GET: /Department/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Department tbl_department = db.tbl_Department.Find(id);
            if (tbl_department == null)
            {
                return HttpNotFound();
            }
            return View(tbl_department);
        }

        // GET: /Department/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Department/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,name")] tbl_Department tbl_department)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Department.Add(tbl_department);
                ViewBag.message = "Department Added Succesfully";
                db.SaveChanges();

                ModelState.Clear();
                return View();
                //return RedirectToAction("Index");
            }

            return View(tbl_department);
        }

        // GET: /Department/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Department tbl_department = db.tbl_Department.Find(id);
            if (tbl_department == null)
            {
                return HttpNotFound();
            }
            return View(tbl_department);
        }

        // POST: /Department/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,name")] tbl_Department tbl_department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_department);
        }

        // GET: /Department/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Department tbl_department = db.tbl_Department.Find(id);
            if (tbl_department == null)
            {
                return HttpNotFound();
            }
            return View(tbl_department);
        }

        [HttpPost]
        public JsonResult DeleteConfirmed(int id)
        {
            string success = string.Empty;
            // return Json(success, JsonRequestBehavior.AllowGet);
            tbl_Department tbl_department = db.tbl_Department.Find(id);
            db.tbl_Department.Remove(tbl_department);
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
