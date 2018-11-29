using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MilkVitaOrganization.Models;
//using Microsoft.Reporting.WebForms;
using System.IO;

namespace MilkVitaOrganization.Controllers
{
    public class SuperVisorController : Controller
    {
        private MilkVitaEntities db = new MilkVitaEntities();

        // GET: /SuperVisor/
        public ActionResult Index(int tbl_ZoneList = 0, int tbl_ZoneOfficerList = 0, int tbl_VanDriverList=0)
        {
            var tbl_zonesupervisor = db.tbl_ZoneSuperVisor.Include(t => t.tbl_VanDriverList).Include(t => t.tbl_ZoneList).Include(t => t.tbl_ZoneOfficerList);

            if (tbl_ZoneList > 0)
                tbl_zonesupervisor = tbl_zonesupervisor.Where(t => t.zoneId == tbl_ZoneList);

            ViewBag.tbl_ZoneSuperVisor = tbl_zonesupervisor;

            if (tbl_ZoneOfficerList > 0)
                tbl_zonesupervisor = tbl_zonesupervisor.Where(t => t.zoneOfficerId == tbl_ZoneOfficerList);

            ViewBag.tbl_ZoneSuperVisor = tbl_zonesupervisor;

            if (tbl_VanDriverList > 0)
                tbl_zonesupervisor = tbl_zonesupervisor.Where(t => t.vanDriverId == tbl_VanDriverList);

            ViewBag.tbl_ZoneSuperVisor = tbl_zonesupervisor;

            return View(tbl_zonesupervisor.ToList());
        }

        // GET: /SuperVisor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_ZoneSuperVisor tbl_zonesupervisor = db.tbl_ZoneSuperVisor.Find(id);
            if (tbl_zonesupervisor == null)
            {
                return HttpNotFound();
            }
            return View(tbl_zonesupervisor);
        }

        // GET: /SuperVisor/Create
        public ActionResult Create()
        {
            ViewBag.vanDriverId = new SelectList(db.tbl_VanDriverList, "id", "name");
            ViewBag.zoneId = new SelectList(db.tbl_ZoneList, "id", "name");
            ViewBag.zoneOfficerId = new SelectList(db.tbl_ZoneOfficerList, "id", "name");
            return View();
        }

        // POST: /SuperVisor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,name,empIdNo,mobile,zoneId,zoneOfficerId,vanDriverId")] tbl_ZoneSuperVisor tbl_zonesupervisor)
        {
            if (ModelState.IsValid)
            {
                db.tbl_ZoneSuperVisor.Add(tbl_zonesupervisor);
                ViewBag.message = "Super Visor Created Succesfully";
                db.SaveChanges();


                //return View(tbl_zonesupervisor);
                return RedirectToAction("Index");
            }

            ViewBag.vanDriverId = new SelectList(db.tbl_VanDriverList, "id", "name", tbl_zonesupervisor.vanDriverId);
            ViewBag.zoneId = new SelectList(db.tbl_ZoneList, "id", "name", tbl_zonesupervisor.zoneId);
            ViewBag.zoneOfficerId = new SelectList(db.tbl_ZoneOfficerList, "id", "name", tbl_zonesupervisor.zoneOfficerId);
            return View(tbl_zonesupervisor);
        }

        //public ActionResult Report(string id)
        //{
        //    LocalReport lr = new LocalReport();
        //    string path = Path.Combine(Server.MapPath("~/Report"), "SuperVisorReport.rdlc");
        //    if (System.IO.File.Exists(path))
        //    {
        //        lr.ReportPath = path;
        //    }
        //    else
        //    {
        //        return View("Index");
        //    }
        //    List<tbl_ZoneSuperVisor> cm = new List<tbl_ZoneSuperVisor>();
        //    using (MilkVitaEntities dc = new MilkVitaEntities())
        //    {
        //        cm = dc.tbl_ZoneSuperVisor.ToList();
        //    }
        //    ReportDataSource rd = new ReportDataSource("DataSet1", cm);
        //    lr.DataSources.Add(rd);
        //    string reportType = id;
        //    string mimeType;
        //    string encoding;
        //    string fileNameExtension;



        //    string deviceInfo =

        //    "<DeviceInfo>" +
        //    "  <OutputFormat>" + id + "</OutputFormat>" +
        //    "  <PageWidth>8.5in</PageWidth>" +
        //    "  <PageHeight>11in</PageHeight>" +
        //    "  <MarginTop>0.5in</MarginTop>" +
        //    "  <MarginLeft>1in</MarginLeft>" +
        //    "  <MarginRight>1in</MarginRight>" +
        //    "  <MarginBottom>0.5in</MarginBottom>" +
        //    "</DeviceInfo>";

        //    Warning[] warnings;
        //    string[] streams;
        //    byte[] renderedBytes;

        //    renderedBytes = lr.Render(
        //        reportType,
        //        deviceInfo,
        //        out mimeType,
        //        out encoding,
        //        out fileNameExtension,
        //        out streams,
        //        out warnings);
        //    return File(renderedBytes, mimeType);
        //}

        // GET: /SuperVisor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_ZoneSuperVisor tbl_zonesupervisor = db.tbl_ZoneSuperVisor.Find(id);
            if (tbl_zonesupervisor == null)
            {
                return HttpNotFound();
            }
            ViewBag.vanDriverId = new SelectList(db.tbl_VanDriverList, "id", "name", tbl_zonesupervisor.vanDriverId);
            ViewBag.zoneId = new SelectList(db.tbl_ZoneList, "id", "name", tbl_zonesupervisor.zoneId);
            ViewBag.zoneOfficerId = new SelectList(db.tbl_ZoneOfficerList, "id", "name", tbl_zonesupervisor.zoneOfficerId);
            return View(tbl_zonesupervisor);
        }

        // POST: /SuperVisor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,name,empIdNo,mobile,zoneId,zoneOfficerId,vanDriverId")] tbl_ZoneSuperVisor tbl_zonesupervisor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_zonesupervisor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.vanDriverId = new SelectList(db.tbl_VanDriverList, "id", "name", tbl_zonesupervisor.vanDriverId);
            ViewBag.zoneId = new SelectList(db.tbl_ZoneList, "id", "name", tbl_zonesupervisor.zoneId);
            ViewBag.zoneOfficerId = new SelectList(db.tbl_ZoneOfficerList, "id", "name", tbl_zonesupervisor.zoneOfficerId);
            return View(tbl_zonesupervisor);
        }

        // GET: /SuperVisor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_ZoneSuperVisor tbl_zonesupervisor = db.tbl_ZoneSuperVisor.Find(id);
            if (tbl_zonesupervisor == null)
            {
                return HttpNotFound();
            }
            return View(tbl_zonesupervisor);
        }

        [HttpPost]
        public JsonResult DeleteConfirmed(int id)
        {
            string success = string.Empty;
            // return Json(success, JsonRequestBehavior.AllowGet);
            tbl_ZoneSuperVisor tbl_zonesupervisor = db.tbl_ZoneSuperVisor.Find(id);
            db.tbl_ZoneSuperVisor.Remove(tbl_zonesupervisor);
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
