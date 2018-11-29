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
    public class RegistrationController : Controller
    {
        private MilkVitaEntities db = new MilkVitaEntities();

        // GET: /Registration/
        public ActionResult Index()
        {
            return View(db.tbl_Registration.ToList());
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.LoginModel loginModel)
        {
            if(ModelState.IsValid)
            {
                var lg = db.tbl_Registration.Where(l => l.userId.ToLower() == loginModel.userId.ToLower() && l.password == loginModel.password).First();
                if(lg == null)
                {
                    ViewBag.error = "Invalid Login";
                }
                else
                {
                    Session["id"] = lg.id;
                    Session["userId"] = lg.userId;
                    Session["name"] = lg.name;
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(loginModel);
        }

        public ActionResult Logout()
        {
            Session["id"] = "";
            Session["userId"] = "";
            Session["name"] = "";

            return RedirectToAction("Login", "Registration");
            //return View();
        }

        public ActionResult MyAccount()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register([Bind(Include = "id,userId,name,contact,email,password,registrationDate,address")] tbl_Registration tbl_registration)
        {
            if (ModelState.IsValid)
            {
                tbl_registration.registrationDate = DateTime.Now;

                ViewBag.massage = "Registered Succesfully";
                db.tbl_Registration.Add(tbl_registration);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(tbl_registration);
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
