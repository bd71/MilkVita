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
    public class IssueController : Controller
    {
        private MilkVitaEntities db = new MilkVitaEntities();

        // GET: /Issue/
        public ActionResult Index()
        {
            var tbl_issue = db.tbl_Issue.Include(t => t.tbl_Brand).Include(t => t.tbl_Color).Include(t => t.tbl_Demand).Include(t => t.tbl_Demand1).Include(t => t.tbl_Department).Include(t => t.tbl_ItemMaster).Include(t => t.tbl_Price).Include(t => t.tbl_Receive).Include(t => t.tbl_Receive1).Include(t => t.tbl_Unit);
            return View(tbl_issue.ToList());
        }

        // GET: /Issue/Create
        public ActionResult Create()
        {
            ViewBag.brandId = new SelectList(db.tbl_Brand, "id", "name");
            ViewBag.colorId = new SelectList(db.tbl_Color, "id", "name");
            ViewBag.demandNo = new SelectList(db.tbl_Demand, "id", "demandNo");
            ViewBag.demandQty = new SelectList(db.tbl_Demand, "id", "demandQty");
            ViewBag.departmentId = new SelectList(db.tbl_Department, "id", "name");
            ViewBag.itemId = new SelectList(db.tbl_ItemMaster, "id", "name");
            ViewBag.priceId = new SelectList(db.tbl_Price, "id", "price");
            ViewBag.GRN = new SelectList(db.tbl_Receive, "id", "GRN");
            ViewBag.receiveQty = new SelectList(db.tbl_Receive, "id", "receiveQty");
            ViewBag.unitId = new SelectList(db.tbl_Unit, "id", "name");
            return View();
        }

        // POST: /Issue/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,GRN,demandNo,itemId,brandId,colorId,demandQty,receiveQty,issueQty,unitId,priceId,departmentId,dateTime,calanNo")] tbl_Issue tbl_issue)
        {
            if (ModelState.IsValid)
            {
                tbl_issue.dateTime = DateTime.Now;
                db.tbl_Issue.Add(tbl_issue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.brandId = new SelectList(db.tbl_Brand, "id", "name", tbl_issue.brandId);
            ViewBag.colorId = new SelectList(db.tbl_Color, "id", "name", tbl_issue.colorId);
            ViewBag.demandNo = new SelectList(db.tbl_Demand, "id", "demandNo", tbl_issue.demandNo);
            ViewBag.demandQty = new SelectList(db.tbl_Demand, "id", "demandQty", tbl_issue.demandQty);
            ViewBag.departmentId = new SelectList(db.tbl_Department, "id", "name", tbl_issue.departmentId);
            ViewBag.itemId = new SelectList(db.tbl_ItemMaster, "id", "name", tbl_issue.itemId);
            ViewBag.priceId = new SelectList(db.tbl_Price, "id", "price", tbl_issue.priceId);
            ViewBag.GRN = new SelectList(db.tbl_Receive, "id", "GRN", tbl_issue.GRN);
            ViewBag.receiveQty = new SelectList(db.tbl_Receive, "id", "receiveQty", tbl_issue.receiveQty);
            ViewBag.unitId = new SelectList(db.tbl_Unit, "id", "name", tbl_issue.unitId);
            return View(tbl_issue);
        }

        // GET: /Issue/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Issue tbl_issue = db.tbl_Issue.Find(id);
            if (tbl_issue == null)
            {
                return HttpNotFound();
            }
            ViewBag.brandId = new SelectList(db.tbl_Brand, "id", "name", tbl_issue.brandId);
            ViewBag.colorId = new SelectList(db.tbl_Color, "id", "name", tbl_issue.colorId);
            ViewBag.demandNo = new SelectList(db.tbl_Demand, "id", "demandNo", tbl_issue.demandNo);
            ViewBag.demandQty = new SelectList(db.tbl_Demand, "id", "demandQty", tbl_issue.demandQty);
            ViewBag.departmentId = new SelectList(db.tbl_Department, "id", "name", tbl_issue.departmentId);
            ViewBag.itemId = new SelectList(db.tbl_ItemMaster, "id", "name", tbl_issue.itemId);
            ViewBag.priceId = new SelectList(db.tbl_Price, "id", "price", tbl_issue.priceId);
            ViewBag.GRN = new SelectList(db.tbl_Receive, "id", "GRN", tbl_issue.GRN);
            ViewBag.receiveQty = new SelectList(db.tbl_Receive, "id", "receiveQty", tbl_issue.receiveQty);
            ViewBag.unitId = new SelectList(db.tbl_Unit, "id", "name", tbl_issue.unitId);
            return View(tbl_issue);
        }

        // POST: /Issue/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,GRN,demandNo,itemId,brandId,colorId,demandQty,receiveQty,issueQty,unitId,priceId,departmentId,dateTime,calanNo")] tbl_Issue tbl_issue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_issue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.brandId = new SelectList(db.tbl_Brand, "id", "name", tbl_issue.brandId);
            ViewBag.colorId = new SelectList(db.tbl_Color, "id", "name", tbl_issue.colorId);
            ViewBag.demandNo = new SelectList(db.tbl_Demand, "id", "demandNo", tbl_issue.demandNo);
            ViewBag.demandQty = new SelectList(db.tbl_Demand, "id", "demandNo", tbl_issue.demandQty);
            ViewBag.departmentId = new SelectList(db.tbl_Department, "id", "name", tbl_issue.departmentId);
            ViewBag.itemId = new SelectList(db.tbl_ItemMaster, "id", "name", tbl_issue.itemId);
            ViewBag.priceId = new SelectList(db.tbl_Price, "id", "id", tbl_issue.priceId);
            ViewBag.GRN = new SelectList(db.tbl_Receive, "id", "receiveNo", tbl_issue.GRN);
            ViewBag.receiveQty = new SelectList(db.tbl_Receive, "id", "receiveNo", tbl_issue.receiveQty);
            ViewBag.unitId = new SelectList(db.tbl_Unit, "id", "name", tbl_issue.unitId);
            return View(tbl_issue);
        }

        // GET: /Issue/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Issue tbl_issue = db.tbl_Issue.Find(id);
            if (tbl_issue == null)
            {
                return HttpNotFound();
            }
            return View(tbl_issue);
        }

        [HttpPost]
        public JsonResult DeleteConfirmed(int id)
        {
            string success = string.Empty;
            // return Json(success, JsonRequestBehavior.AllowGet);
            tbl_Issue tbl_issue = db.tbl_Issue.Find(id);
            db.tbl_Issue.Remove(tbl_issue);
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
