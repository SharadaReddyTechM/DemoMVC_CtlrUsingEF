using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DemoMVC_CtlrUsingEF.Models;

namespace DemoMVC_CtlrUsingEF.Controllers
{
    public class tblemployeesController : Controller
    {
        private VizagDbEntities db = new VizagDbEntities();

        // GET: tblemployees
        public ActionResult Index()
        {
            if (Session["usrname"] != null)
            {
                if (Session["usrname"].ToString() != string.Empty)
                    return View(db.tblemployees.ToList());
                else
                    return RedirectToAction("InvalidLogin", "Account");
            }
            else
            {
                Session["usrname"] = "";
                Session.Clear();
                Session.Abandon();
                return RedirectToAction("InvalidLogin", "Account");
            }
        }

        // GET: tblemployees/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblemployee tblemployee = db.tblemployees.FirstOrDefault(e => e.empid == id);//db.tblemployees.Find(id);
            if (tblemployee == null)
            {
                return HttpNotFound();
            }
            return View(tblemployee);
        }

        // GET: tblemployees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tblemployees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "empid,empname,empdob,empage,empmobile,empdept,salary,empgender")] tblemployee tblemployee)
        {
            if (ModelState.IsValid)
            {
                db.tblemployees.Add(tblemployee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblemployee);
        }

        // GET: tblemployees/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblemployee tblemployee = db.tblemployees.Find(id);
            if (tblemployee == null)
            {
                return HttpNotFound();
            }
            return View(tblemployee);
        }

        // POST: tblemployees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "empid,empname,empdob,empage,empmobile,empdept,salary,empgender")] tblemployee tblemployee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblemployee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblemployee);
        }

        // GET: tblemployees/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblemployee tblemployee = db.tblemployees.Find(id);
            if (tblemployee == null)
            {
                return HttpNotFound();
            }
            return View(tblemployee);
        }

        // POST: tblemployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tblemployee tblemployee = db.tblemployees.Find(id);
            db.tblemployees.Remove(tblemployee);
            db.SaveChanges();
            return RedirectToAction("Index");
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
