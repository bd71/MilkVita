using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MilkVitaOrganization.Models;
using System.Web.Script.Serialization;
using System.Data.Entity.Migrations;
using System.Text;
//using Microsoft.Reporting.WebForms;
using System.IO;

namespace MilkVitaOrganization.Controllers
{
    public class DemandController : BaseController
    {

        private static readonly MilkVitaEntities _dbContext = new MilkVitaEntities();

        public ActionResult Index(int? id)
        {
            ConfigureActionViewDataForSubmitButton(id);

            var demands = FillControlDataSouceByDemandNo();
            
            ViewData[DemandsViewModel.DemandsKey] = demands;

            _loadViewBags();
            
            return View(FillControlsFromDatabase(id));
        }

        private DemandViewModel FillControlsFromDatabase(int? id)
        {
            var demandViewModel = new DemandViewModel
            {
                DemandNo = Session.GetDataFromSession<int>(DemandViewModel.DemandKey)
            };

            var tblDemand = _dbContext.tbl_Demand.Find(id);
            if (tblDemand == null)
                return demandViewModel;


            demandViewModel.DemandNo = Session.GetDataFromSession<int>(DemandViewModel.DemandKey);
            demandViewModel.DemandId = tblDemand.id;
            demandViewModel.UnitId = tblDemand.unitId;
            demandViewModel.SupplierId = tblDemand.supplierId;
            demandViewModel.ItemId = tblDemand.itemId;
            demandViewModel.DemandQty = tblDemand.demandQty;

            return demandViewModel;
        }

        private void ConfigureActionViewDataForSubmitButton(int? id)
        {
            if (id > 0)
            {
                ViewData["Operation"] = "Update";
            }
            else
            {
                SetDemandNoGlobally();
                ViewData["Operation"] = "Save";
            }
        }

        private DemandsViewModel FillControlDataSouceByDemandNo()
        {
            var demandsViewModel = new DemandsViewModel
            {
                DemandViewModel =
                {
                    DemandNo = Session.GetDataFromSession<int>(DemandViewModel.DemandKey)
                }
            };


            demandsViewModel.DemandViewModels = _dbContext.tbl_Demand
                .Where(q => q.demandNo.Equals(demandsViewModel.DemandViewModel.DemandNo)).Select(p => new DemandViewModel
                {
                    UnitId = p.unitId,
                    Unit = p.tbl_Unit.name,
                    DemandQty = p.demandQty,
                    SupplierId = p.supplierId,
                    Supplier = p.tbl_Supplier.name,
                    ItemId = p.itemId,
                    Item = p.tbl_ItemMaster.name,
                    DemandId = p.id,
                }).ToList();

            return demandsViewModel;
        }

        private void _loadViewBags()
        {

            ViewBag.Suppliers = new SelectList(GetSuppliers(), "Id", "Value");
            ViewBag.MasterItems = new SelectList(GetMasterItems(), "Id", "Value");
            ViewBag.ItemUnits = new SelectList(GetItemUnits(), "Id", "Value");
            ViewBag.DemandNos = new SelectList(GetDemandNos(), "Id", "Value");

        }

        private static IEnumerable<SelectItem> GetSuppliers()
        {

            var suppliers = _dbContext.tbl_Supplier.ToList();

            return suppliers
                .Select(p => new SelectItem { Id = p.id.ToString(), Value = p.name });
        }

        private static IEnumerable<SelectItem> GetMasterItems()
        {

            var temp = _dbContext.tbl_ItemMaster.ToList();

            return temp
                .Select(p => new SelectItem { Id = p.id.ToString(), Value = p.name });
        }

        private static IEnumerable<SelectItem> GetItemUnits()
        {

            var temp = _dbContext.tbl_Unit.ToList();

            return temp
                .Select(p => new SelectItem { Id = p.id.ToString(), Value = p.name });
        }

        private static IEnumerable<SelectItem> GetDemandNos()
        {

            var temp = _dbContext.tbl_Demand.Select(p=>p.demandNo).Distinct().ToList();

            return temp
                .Select(p => new SelectItem { Id = p.ToString(), Value = p.ToString() });
        }



        private void SetDemandNoGlobally()
        {
            if (Session.IsSessionHasValue(DemandViewModel.DemandKey))
                return;
            
            
            var result = _dbContext.tbl_Demand.Select(p => p.demandNo).ToList();
            var maxDemandNo = result.Count > 0 ? result.Max(p => p) + 1 : 1;
            
            Session.SetDataToSession(DemandViewModel.DemandKey, maxDemandNo);
        }


        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Save")]
        public ActionResult Save(DemandViewModel demandViewModel)

        {
            try
            {
                var tempTblDemand = new tbl_Demand
                {
                    itemId = demandViewModel.ItemId,
                    supplierId = demandViewModel.SupplierId,
                    unitId = demandViewModel.UnitId,
                    demandQty = demandViewModel.DemandQty,
                    demandNo = Session.GetDataFromSession<int>(DemandViewModel.DemandKey)
                };

                if (!ModelState.IsValid)
                    return RedirectToAction("Index");


                _dbContext.tbl_Demand.Add(tempTblDemand);
                _dbContext.SaveChanges();

                Success($"<b>{demandViewModel.Item} </b> Item was successfully added to the database.", true);
            }
            catch (Exception ex)
            {
                ErrorMessage();
            }

            return RedirectToAction("Index");
        }

        private void ErrorMessage()
        {
            Danger("Something went wrong. Please check your input.");
        }


        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Update")]
        public ActionResult Edit(DemandViewModel demandViewModel)

        {
            try
            {
                if (!ModelState.IsValid)
                    return RedirectToAction("Index");

                var tblDemand = _dbContext.tbl_Demand.Find(demandViewModel.DemandId);

                if (tblDemand != null)
                {
                    tblDemand.itemId = demandViewModel.ItemId;
                    tblDemand.supplierId = demandViewModel.SupplierId;
                    tblDemand.unitId = demandViewModel.UnitId;
                    tblDemand.demandQty = demandViewModel.DemandQty;
                    tblDemand.demandNo = Session.GetDataFromSession<int>(DemandViewModel.DemandKey);

                    _dbContext.Entry(tblDemand).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                    
                    Success($"<b>{tblDemand.tbl_ItemMaster.name} </b> Item was successfully update to the database.", true);
                }
                
            }
            catch (Exception ex)
            {
                ErrorMessage();
            }
            
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "New")]
        public ActionResult New(DemandViewModel demandViewModel)
        {
            Session.RemoveAllFromSession();
            _loadViewBags();
            Warning("New demand created",true);
            return RedirectToAction("Index");

        }
        
        public ActionResult Delete(int id)
        {
            try
            {
                var tblDemand = _dbContext.tbl_Demand.Find(id);

                if (tblDemand == null)
                {
                    ErrorMessage();
                    return RedirectToAction("Index", new {id});
                }

                _dbContext.tbl_Demand.Remove(tblDemand);
                var result = _dbContext.SaveChanges();

                if (result > 0) Information("Data deleted successfully!");
            }
            catch (Exception ex)
            {
                ErrorMessage();
            }

            return RedirectToAction("Index", new {id = 0});
        }
        
        
        [HttpGet]
        public ActionResult GetDemand(int demandId)
        {
            
            Session.SetDataToSession(DemandViewModel.DemandKey,demandId);
            return RedirectToAction("Index", new { id = 0 });
        }


    }
}
