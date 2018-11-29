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
    public class ShopController : Controller
    {
        private MilkVitaEntities db = new MilkVitaEntities();

        // GET: /Shop/
        public ActionResult Index()
        {
            return View(db.tbl_ShopList.ToList());
        }

        // GET: /Shop/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_ShopList tbl_shoplist = db.tbl_ShopList.Find(id);
            if (tbl_shoplist == null)
            {
                return HttpNotFound();
            }
            return View(tbl_shoplist);
        }

        // GET: /Shop/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Shop/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,name,address")] tbl_ShopList tbl_shoplist)
        {
            if (ModelState.IsValid)
            {
                db.tbl_ShopList.Add(tbl_shoplist);
                ViewBag.massage = "Shop Created Succesfully";
                db.SaveChanges();
          
                ModelState.Clear();
                return View();
            }

            return View(tbl_shoplist);
        }

        // GET: /Shop/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_ShopList tbl_shoplist = db.tbl_ShopList.Find(id);
            if (tbl_shoplist == null)
            {
                return HttpNotFound();
            }
            return View(tbl_shoplist);
        }

        // POST: /Shop/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,name,address")] tbl_ShopList tbl_shoplist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_shoplist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_shoplist);
        }

        // GET: /Shop/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_ShopList tbl_shoplist = db.tbl_ShopList.Find(id);
            if (tbl_shoplist == null)
            {
                return HttpNotFound();
            }
            return View(tbl_shoplist);
        }

        [HttpPost]
        public JsonResult DeleteConfirmed(int id)
        {
            string success = string.Empty;
            // return Json(success, JsonRequestBehavior.AllowGet);
            tbl_ShopList tbl_shoplist = db.tbl_ShopList.Find(id);
            db.tbl_ShopList.Remove(tbl_shoplist);
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
        //    string path = Path.Combine(Server.MapPath("~/Report"), "ShopReport.rdlc");
        //    if (System.IO.File.Exists(path))
        //    {
        //        lr.ReportPath = path;
        //    }
        //    else
        //    {
        //        return View("Index");
        //    }
        //    List<tbl_ShopList> cm = new List<tbl_ShopList>();
        //    using (MilkVitaEntities dc = new MilkVitaEntities())
        //    {
        //        cm = dc.tbl_ShopList.ToList();
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
