using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LearningHub.Models;

namespace LearningHub.Controllers
{
    [Authorized(Roles = ContantValue.Admin)]
    public class StaffTblsController : Controller
    {
        private Entities db = new Entities();

        // GET: StaffTbls
        public ActionResult Index()
        {
            var staffTbls = db.StaffTbls.Include(s => s.CredentialTbl);
            return View(staffTbls.ToList());
        }

        // GET: StaffTbls/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffTbl staffTbl = db.StaffTbls.Find(id);
            if (staffTbl == null)
            {
                return HttpNotFound();
            }
            return View(staffTbl);
        }

        // GET: StaffTbls/Create
        public ActionResult Create()
        {
            ViewBag.CredID = new SelectList(db.CredentialTbls, "CredID", "UserName");
            return View();
        }

        // POST: StaffTbls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegistrationVM adminTbl)
        {

            if (ModelState.IsValid)
            {
                adminTbl.cred.EnteredOn = DateTime.Now;
                adminTbl.cred.UpdatedOn = DateTime.Now;
                adminTbl.cred.RoleID = 3;
                CredentialTbl crdtbl = db.CredentialTbls.Add(adminTbl.cred);
                db.SaveChanges();
                adminTbl.staff.CredID = crdtbl.CredID;
                adminTbl.staff.IsHead = false;
                db.StaffTbls.Add(adminTbl.staff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(adminTbl);
        }

        // GET: StaffTbls/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffTbl staffTbl = db.StaffTbls.Find(id);
            if (staffTbl == null)
            {
                return HttpNotFound();
            }
            CredentialTbl credtbl = db.CredentialTbls.Find(staffTbl.CredID);
            RegistrationVM regvm = new RegistrationVM()
            {
                staff = staffTbl,
                cred = credtbl
            };
            return View(regvm);
        }

        // POST: StaffTbls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RegistrationVM adminTbl)
        {
            if (ModelState.IsValid)
            {
                StaffTbl admin = db.StaffTbls.Find(adminTbl.staff.StaffID);
                admin.StaffName = adminTbl.staff.StaffName;
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                CredentialTbl cred = db.CredentialTbls.Find(admin.CredID);
                cred.Password = adminTbl.cred.Password;
                db.Entry(cred).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adminTbl);
        }

        // GET: StaffTbls/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffTbl staffTbl = db.StaffTbls.Find(id);
            if (staffTbl == null)
            {
                return HttpNotFound();
            }
            return View(staffTbl);
        }

        // POST: StaffTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            StaffTbl staffTbl = db.StaffTbls.Find(id);
            db.StaffTbls.Remove(staffTbl);
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
