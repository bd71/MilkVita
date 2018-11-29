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
    public class VanDriverController : Controller
    {
        private MilkVitaEntities db = new MilkVitaEntities();

        // GET: /VanDriver/
        public ActionResult Index()
        {
            var tbl_vandriverlist = db.tbl_VanDriverList.Include(t => t.tbl_ZoneList);
            return View(tbl_vandriverlist.ToList());
        }

        // GET: /VanDriver/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_VanDriverList tbl_vandriverlist = db.tbl_VanDriverList.Find(id);
            if (tbl_vandriverlist == null)
            {
                return HttpNotFound();
            }
            return View(tbl_vandriverlist);
        }

        // GET: /VanDriver/Create
        public ActionResult Create()
        {
            ViewBag.zoneId = new SelectList(db.tbl_ZoneList, "id", "name");
            return View();
        }

        // POST: /VanDriver/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,serialNo,name,zoneId")] tbl_VanDriverList tbl_vandriverlist)
        {
            if (ModelState.IsValid)
            {
                db.tbl_VanDriverList.Add(tbl_vandriverlist);
                ViewBag.message = "Van Driver Added Succesfully";
                db.SaveChanges();


                //return View(tbl_vandriverlist);
                return RedirectToAction("Index");
            }

            ViewBag.zoneId = new SelectList(db.tbl_ZoneList, "id", "name", tbl_vandriverlist.zoneId);
            return View(tbl_vandriverlist);
        }

        // GET: /VanDriver/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_VanDriverList tbl_vandriverlist = db.tbl_VanDriverList.Find(id);
            if (tbl_vandriverlist == null)
            {
                return HttpNotFound();
            }
            ViewBag.zoneId = new SelectList(db.tbl_ZoneList, "id", "name", tbl_vandriverlist.zoneId);
            return View(tbl_vandriverlist);
        }

        // POST: /VanDriver/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,serialNo,name,zoneId")] tbl_VanDriverList tbl_vandriverlist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_vandriverlist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.zoneId = new SelectList(db.tbl_ZoneList, "id", "name", tbl_vandriverlist.zoneId);
            return View(tbl_vandriverlist);
        }

        // GET: /VanDriver/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_VanDriverList tbl_vandriverlist = db.tbl_VanDriverList.Find(id);
            if (tbl_vandriverlist == null)
            {
                return HttpNotFound();
            }
            return View(tbl_vandriverlist);
        }

        [HttpPost]
        public JsonResult DeleteConfirmed(int id)
        {
            string success = string.Empty;
            // return Json(success, JsonRequestBehavior.AllowGet);
            tbl_VanDriverList tbl_vandriverlist = db.tbl_VanDriverList.Find(id);
            db.tbl_VanDriverList.Remove(tbl_vandriverlist);
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
        //    string path = Path.Combine(Server.MapPath("~/Report"), "VanDriverReport.rdlc");
        //    if (System.IO.File.Exists(path))
        //    {
        //        lr.ReportPath = path;
        //    }
        //    else
        //    {
        //        return View("Index");
        //    }
        //    List<tbl_VanDriverList> cm = new List<tbl_VanDriverList>();
        //    using (MilkVitaEntities dc = new MilkVitaEntities())
        //    {
        //        cm = dc.tbl_VanDriverList.ToList();
        //    }
        //    ReportDataSource rd = new ReportDataSource("DataSet4", cm);
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
