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
    public class ItemMasterController : Controller
    {
        private MilkVitaEntities db = new MilkVitaEntities();

        // GET: /ItemMaster/
        public ActionResult Index()
        {
            var tbl_itemmaster = db.tbl_ItemMaster.Include(t => t.tbl_Brand).Include(t => t.tbl_Category).Include(t => t.tbl_Color);
            return View(tbl_itemmaster.ToList());
        }

        // GET: /ItemMaster/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_ItemMaster tbl_itemmaster = db.tbl_ItemMaster.Find(id);
            if (tbl_itemmaster == null)
            {
                return HttpNotFound();
            }
            return View(tbl_itemmaster);
        }

        // GET: /ItemMaster/Create
        public ActionResult Create()
        {
            ViewBag.brandId = new SelectList(db.tbl_Brand, "id", "name");
            ViewBag.categoryId = new SelectList(db.tbl_Category, "id", "name");
            ViewBag.colorId = new SelectList(db.tbl_Color, "id", "name");
            return View();
        }

        // POST: /ItemMaster/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,code,name,categoryId,brandId,colorId,barCode")] tbl_ItemMaster tbl_itemmaster)
        {
            if (ModelState.IsValid)
            {
                db.tbl_ItemMaster.Add(tbl_itemmaster);
                ViewBag.message = "Item Inserted Succesfully";
                db.SaveChanges();


                //return View(tbl_itemmaster);
                return RedirectToAction("Index");
            }

            ViewBag.brandId = new SelectList(db.tbl_Brand, "id", "name", tbl_itemmaster.brandId);
            ViewBag.categoryId = new SelectList(db.tbl_Category, "id", "name", tbl_itemmaster.categoryId);
            ViewBag.colorId = new SelectList(db.tbl_Color, "id", "name", tbl_itemmaster.colorId);
            return View(tbl_itemmaster);
        }

        // GET: /ItemMaster/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_ItemMaster tbl_itemmaster = db.tbl_ItemMaster.Find(id);
            if (tbl_itemmaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.brandId = new SelectList(db.tbl_Brand, "id", "name", tbl_itemmaster.brandId);
            ViewBag.categoryId = new SelectList(db.tbl_Category, "id", "name", tbl_itemmaster.categoryId);
            ViewBag.colorId = new SelectList(db.tbl_Color, "id", "name", tbl_itemmaster.colorId);
            return View(tbl_itemmaster);
        }

        // POST: /ItemMaster/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,code,name,categoryId,brandId,colorId,barCode")] tbl_ItemMaster tbl_itemmaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_itemmaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.brandId = new SelectList(db.tbl_Brand, "id", "name", tbl_itemmaster.brandId);
            ViewBag.categoryId = new SelectList(db.tbl_Category, "id", "name", tbl_itemmaster.categoryId);
            ViewBag.colorId = new SelectList(db.tbl_Color, "id", "name", tbl_itemmaster.colorId);
            return View(tbl_itemmaster);
        }

        // GET: /ItemMaster/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_ItemMaster tbl_itemmaster = db.tbl_ItemMaster.Find(id);
            if (tbl_itemmaster == null)
            {
                return HttpNotFound();
            }
            return View(tbl_itemmaster);
        }

        [HttpPost]
        public JsonResult DeleteConfirmed(int id)
        {
            string success = string.Empty;
            // return Json(success, JsonRequestBehavior.AllowGet);
            tbl_ItemMaster tbl_itemmaster = db.tbl_ItemMaster.Find(id);
            db.tbl_ItemMaster.Remove(tbl_itemmaster);
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
        //    string path = Path.Combine(Server.MapPath("~/Report"), "ItemReport.rdlc");
        //    if (System.IO.File.Exists(path))
        //    {
        //        lr.ReportPath = path;
        //    }
        //    else
        //    {
        //        return View("Index");
        //    }
        //    List<tbl_ItemMaster> cm = new List<tbl_ItemMaster>();
        //    using (MilkVitaEntities dc = new MilkVitaEntities())
        //    {
        //        cm = dc.tbl_ItemMaster.ToList();
        //    }
        //    ReportDataSource rd = new ReportDataSource("DataSet6", cm);
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
