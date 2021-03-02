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
    [Authorized(Roles = ContantValue.SuperAdmin)]
    public class AdminTblsController : BaseController
    {

        // GET: AdminTbls
        public ActionResult Index()
        {
            var adminTbls = db.AdminTbls.Include(a => a.CredentialTbl);
            return View(adminTbls.ToList());
        }

        // GET: AdminTbls/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminTbl adminTbl = db.AdminTbls.Find(id);
            if (adminTbl == null)
            {
                return HttpNotFound();
            }
            return View(adminTbl);
        }

        // GET: AdminTbls/Create
        public ActionResult Create()
        {
            //ViewBag.RoleID = new SelectList(db.RoleTbls.Where(x), "RoleID", "RoleName",2);
            return View();
        }

        // POST: AdminTbls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegistrationVM adminTbl)
        {
            if (ModelState.IsValid)
            {
                adminTbl.cred.EnteredOn = adminTbl.cred.UpdatedOn = DateTime.Now;
                adminTbl.cred.RoleID = 2;
                CredentialTbl crdtbl = db.CredentialTbls.Add(adminTbl.cred);
                db.SaveChanges();
                adminTbl.admin.CredID = crdtbl.CredID;
                db.AdminTbls.Add(adminTbl.admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(adminTbl);
        }

        // GET: AdminTbls/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminTbl adminTbl = db.AdminTbls.Find(id);
            if (adminTbl == null)
            {
                return HttpNotFound();
            }
            CredentialTbl credtbl = db.CredentialTbls.Find(adminTbl.CredID);
            RegistrationVM regvm = new RegistrationVM()
            {
                admin = adminTbl,
                cred = credtbl
            };
            //ViewBag.CredID = new SelectList(db.CredentialTbls, "CredID", "UserName", adminTbl.CredID);
            return View(regvm);
        }

        // POST: AdminTbls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RegistrationVM adminTbl)
        {
            if (ModelState.IsValid)
            {
                AdminTbl admin = db.AdminTbls.Find(adminTbl.admin.AdminID);
                admin.AdminName = adminTbl.admin.AdminName;
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

        // GET: AdminTbls/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminTbl adminTbl = db.AdminTbls.Find(id);
            if (adminTbl == null)
            {
                return HttpNotFound();
            }
            return View(adminTbl);
        }

        // POST: AdminTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            AdminTbl adminTbl = db.AdminTbls.Find(id);
            db.AdminTbls.Remove(adminTbl);
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
