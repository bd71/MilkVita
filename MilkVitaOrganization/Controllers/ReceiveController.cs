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
    public class ReceiveController : Controller
    {
        private MilkVitaEntities db = new MilkVitaEntities();

        // GET: /Receive/
        public ActionResult Index()
        {
            var tbl_receive = db.tbl_Receive.Include(t => t.tbl_Brand).Include(t => t.tbl_Color).Include(t => t.tbl_Demand).Include(t => t.tbl_ItemMaster).Include(t => t.tbl_Unit);
            return View(tbl_receive.ToList());
        }

        // GET: /Receive/Create
        public ActionResult Create()
        {
            //ViewBag.brandId = new SelectList(db.tbl_Brand, "id", "name");
            //ViewBag.colorId = new SelectList(db.tbl_Color, "id", "name");            
            //ViewBag.demandId = new SelectList(objtbl_Demand, "id", "demandNo");
            //ViewBag.ItemId = new SelectList(db.tbl_ItemMaster, "id", "name");
            //ViewBag.unitId = new SelectList(db.tbl_Unit, "id", "name");


            ViewBag.ItemId = new SelectList(db.tbl_ItemMaster, "id", "name");
            ViewBag.brandId = new SelectList(db.tbl_Brand, "id", "name");
            ViewBag.colorId = new SelectList(db.tbl_Color, "id", "name");

            var objtbl_Demand = db.tbl_Demand.GroupBy(x => x.demandNo).Select(x => x.FirstOrDefault());
            ViewBag.demandId = new SelectList(objtbl_Demand, "id", "demandNo");
            ViewBag.unitId = new SelectList(db.tbl_Unit, "id", "name");


            return View();
        }

        // POST: /Receive/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,receiveNo,GRN,serialNo,ItemId,brandId,colorId,dateTime,demandId,receiveQty,unitId")] tbl_Receive tbl_receive)
        {
            tbl_receive.receiveNo = "55";
            tbl_receive.GRN = 55;
            tbl_receive.serialNo = 55;
            tbl_receive.dateTime = DateTime.Now;


            var abc = tbl_receive;

            if (ModelState.IsValid)
            {
                db.tbl_Receive.Add(tbl_receive);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.brandId = new SelectList(db.tbl_Brand, "id", "name", tbl_receive.brandId);
            //ViewBag.colorId = new SelectList(db.tbl_Color, "id", "name", tbl_receive.colorId);
            //ViewBag.demandId = new SelectList(db.tbl_Demand, "id", "demandNo", tbl_receive.demandId);
            //ViewBag.ItemId = new SelectList(db.tbl_ItemMaster, "id", "name", tbl_receive.ItemId);
            //ViewBag.unitId = new SelectList(db.tbl_Unit, "id", "name", tbl_receive.unitId);
            //return View(tbl_receive);

            db.tbl_Receive.Add(tbl_receive);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: /Receive/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Receive tbl_receive = db.tbl_Receive.Find(id);
            if (tbl_receive == null)
            {
                return HttpNotFound();
            }
            ViewBag.brandId = new SelectList(db.tbl_Brand, "id", "name", tbl_receive.brandId);
            ViewBag.colorId = new SelectList(db.tbl_Color, "id", "name", tbl_receive.colorId);
            ViewBag.demandId = new SelectList(db.tbl_Demand, "id", "demandNo", tbl_receive.demandId);
            ViewBag.ItemId = new SelectList(db.tbl_ItemMaster, "id", "name", tbl_receive.ItemId);
            ViewBag.unitId = new SelectList(db.tbl_Unit, "id", "name", tbl_receive.unitId);
            return View(tbl_receive);
        }

        // POST: /Receive/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,receiveNo,GRN,serialNo,ItemId,brandId,colorId,dateTime,demandId,receiveQty,unitId")] tbl_Receive tbl_receive)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_receive).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.brandId = new SelectList(db.tbl_Brand, "id", "name", tbl_receive.brandId);
            ViewBag.colorId = new SelectList(db.tbl_Color, "id", "name", tbl_receive.colorId);
            ViewBag.demandId = new SelectList(db.tbl_Demand, "id", "demandNo", tbl_receive.demandId);
            ViewBag.ItemId = new SelectList(db.tbl_ItemMaster, "id", "name", tbl_receive.ItemId);
            ViewBag.unitId = new SelectList(db.tbl_Unit, "id", "name", tbl_receive.unitId);
            return View(tbl_receive);
        }

        // GET: /Receive/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Receive tbl_receive = db.tbl_Receive.Find(id);
            if (tbl_receive == null)
            {
                return HttpNotFound();
            }
            return View(tbl_receive);
        }

        [HttpPost]
        public JsonResult DeleteConfirmed(int id)
        {
            string success = string.Empty;
            // return Json(success, JsonRequestBehavior.AllowGet);
            tbl_Receive tbl_receive = db.tbl_Receive.Find(id);
            db.tbl_Receive.Remove(tbl_receive);
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
