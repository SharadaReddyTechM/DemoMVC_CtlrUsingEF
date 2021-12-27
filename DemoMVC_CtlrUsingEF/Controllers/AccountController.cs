using DemoMVC_CtlrUsingEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoMVC_CtlrUsingEF.Controllers
{
    public class AccountController : Controller
    {
        VizagDbEntities db = new VizagDbEntities();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            tblemployee emp = new tblemployee();

            return View(emp);
        }
        [HttpPost]
        public ActionResult Login([Bind(Include ="empname,empPic")]tblemployee emp)
        {
            var searchEmp = db.tblemployees.FirstOrDefault(e => e.empname == emp.empname && e.empPic == emp.empPic);
            if (searchEmp != null)
            {
                Session["usrname"] = emp.empname;
                return RedirectToAction("Index", "tblemployees");
            }
            return View(emp);
        }
        public ActionResult InvalidLogin()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session["usrname"] = "";
            Session.Clear();
            Session.Abandon();
            return View();            
        }
    }
}