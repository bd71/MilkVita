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
    public class ZoneOfficerController : Controller
    {
        private MilkVitaEntities db = new MilkVitaEntities();

        // GET: /ZoneOfficer/
        public ActionResult Index(int tbl_ZoneList=0, int tbl_VanDriverList=0)
        {
            var tbl_zoneofficerlist = db.tbl_ZoneSuperVisor.Include(t => t.tbl_VanDriverList).Include(t => t.tbl_ZoneList);

            if (tbl_ZoneList > 0)
                tbl_zoneofficerlist = tbl_zoneofficerlist.Where(t => t.zoneId == tbl_ZoneList);

            ViewBag.tbl_ZoneOfficerList = tbl_zoneofficerlist;

            if (tbl_VanDriverList > 0)
                tbl_zoneofficerlist = tbl_zoneofficerlist.Where(t => t.zoneId == tbl_VanDriverList);

            ViewBag.tbl_ZoneOfficerList = tbl_zoneofficerlist;

            return View(db.tbl_ZoneOfficerList.ToList());
        }

        // GET: /ZoneOfficer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_ZoneOfficerList tbl_zoneofficerlist = db.tbl_ZoneOfficerList.Find(id);
            if (tbl_zoneofficerlist == null)
            {
                return HttpNotFound();
            }
            return View(tbl_zoneofficerlist);
        }

        // GET: /ZoneOfficer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /ZoneOfficer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,serialNo,name,officersId")] tbl_ZoneOfficerList tbl_zoneofficerlist)
        {
            if (ModelState.IsValid)
            {
                db.tbl_ZoneOfficerList.Add(tbl_zoneofficerlist);
                ViewBag.message = "Zone Officer Added Succesfully";
                db.SaveChanges();

                ModelState.Clear();
                return View();
                //return RedirectToAction("Index");
            }

            return View(tbl_zoneofficerlist);
        }

        // GET: /ZoneOfficer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_ZoneOfficerList tbl_zoneofficerlist = db.tbl_ZoneOfficerList.Find(id);
            if (tbl_zoneofficerlist == null)
            {
                return HttpNotFound();
            }
            return View(tbl_zoneofficerlist);
        }

        // POST: /ZoneOfficer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,serialNo,name,officersId")] tbl_ZoneOfficerList tbl_zoneofficerlist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_zoneofficerlist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_zoneofficerlist);
        }

        // GET: /ZoneOfficer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_ZoneOfficerList tbl_zoneofficerlist = db.tbl_ZoneOfficerList.Find(id);
            if (tbl_zoneofficerlist == null)
            {
                return HttpNotFound();
            }
            return View(tbl_zoneofficerlist);
        }

        [HttpPost]
        public JsonResult DeleteConfirmed(int id)
        {
            string success = string.Empty;
            // return Json(success, JsonRequestBehavior.AllowGet);
            tbl_ZoneOfficerList tbl_zoneofficerlist = db.tbl_ZoneOfficerList.Find(id);
            db.tbl_ZoneOfficerList.Remove(tbl_zoneofficerlist);
            int result = db.SaveChanges();

            if (result > 0)
            {
                success = "Data delete successfully";
            }
            //  return Json(success, JsonRequestBehavior.AllowGet);

            return Json(new { result = success });

        }

        //public ActionResult Report(string id)
        //{
        //    LocalReport lr = new LocalReport();
        //    string path = Path.Combine(Server.MapPath("~/Report"), "ZoneOfficerReport.rdlc");
        //    if (System.IO.File.Exists(path))
        //    {
        //        lr.ReportPath = path;
        //    }
        //    else
        //    {
        //        return View("Index");
        //    }
        //    List<tbl_ZoneOfficerList> cm = new List<tbl_ZoneOfficerList>();
        //    using (MilkVitaEntities dc = new MilkVitaEntities())
        //    {
        //        cm = dc.tbl_ZoneOfficerList.ToList();
        //    }
        //    ReportDataSource rd = new ReportDataSource("DataSet2", cm);
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
