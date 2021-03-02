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
    public class CredentialTblsController : Controller
    {
        private Entities db = new Entities();

        // GET: CredentialTbls
        public ActionResult Index()
        {
            var credentialTbls = db.CredentialTbls.Include(c => c.RoleTbl);
            return View(credentialTbls.ToList());
        }

        // GET: CredentialTbls/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CredentialTbl credentialTbl = db.CredentialTbls.Find(id);
            if (credentialTbl == null)
            {
                return HttpNotFound();
            }
            return View(credentialTbl);
        }

        // GET: CredentialTbls/Create
        public ActionResult Create()
        {
            ViewBag.RoleID = new SelectList(db.RoleTbls, "RoleID", "RoleName");
            return View();
        }

        // POST: CredentialTbls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CredID,RoleID,UserName,Password,EnteredOn,UpdatedOn")] CredentialTbl credentialTbl)
        {
            if (ModelState.IsValid)
            {
                db.CredentialTbls.Add(credentialTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleID = new SelectList(db.RoleTbls, "RoleID", "RoleName", credentialTbl.RoleID);
            return View(credentialTbl);
        }

        // GET: CredentialTbls/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CredentialTbl credentialTbl = db.CredentialTbls.Find(id);
            if (credentialTbl == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleID = new SelectList(db.RoleTbls, "RoleID", "RoleName", credentialTbl.RoleID);
            return View(credentialTbl);
        }

        // POST: CredentialTbls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CredID,RoleID,UserName,Password,EnteredOn,UpdatedOn")] CredentialTbl credentialTbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(credentialTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleID = new SelectList(db.RoleTbls, "RoleID", "RoleName", credentialTbl.RoleID);
            return View(credentialTbl);
        }

        // GET: CredentialTbls/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CredentialTbl credentialTbl = db.CredentialTbls.Find(id);
            if (credentialTbl == null)
            {
                return HttpNotFound();
            }
            return View(credentialTbl);
        }

        // POST: CredentialTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            CredentialTbl credentialTbl = db.CredentialTbls.Find(id);
            db.CredentialTbls.Remove(credentialTbl);
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
