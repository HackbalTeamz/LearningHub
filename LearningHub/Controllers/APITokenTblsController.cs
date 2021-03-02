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
    public class APITokenTblsController : Controller
    {
        private Entities db = new Entities();

        // GET: APITokenTbls
        public ActionResult Index()
        {
            return View(db.APITokenTbls.ToList());
        }

        // GET: APITokenTbls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            APITokenTbl aPITokenTbl = db.APITokenTbls.Find(id);
            if (aPITokenTbl == null)
            {
                return HttpNotFound();
            }
            return View(aPITokenTbl);
        }

        // GET: APITokenTbls/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: APITokenTbls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "APITokenID,APIName,APIKey,SenderID,IsActive,IsTest,IsChannel,IsUnicord,IsFlash,EnteredOn,UpdatedOn,Route")] APITokenTbl aPITokenTbl)
        {
            if (ModelState.IsValid)
            {
                aPITokenTbl.EnteredOn = DateTime.Now;
                aPITokenTbl.UpdatedOn = DateTime.Now;
                db.APITokenTbls.Add(aPITokenTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aPITokenTbl);
        }

        // GET: APITokenTbls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            APITokenTbl aPITokenTbl = db.APITokenTbls.Find(id);
            if (aPITokenTbl == null)
            {
                return HttpNotFound();
            }
            return View(aPITokenTbl);
        }

        // POST: APITokenTbls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "APITokenID,APIName,APIKey,SenderID,IsActive,IsTest,IsChannel,IsUnicord,IsFlash,EnteredOn,UpdatedOn,Route")] APITokenTbl aPITokenTbl)
        {
            if (ModelState.IsValid)
            {
                aPITokenTbl.UpdatedOn = DateTime.Now;
                db.Entry(aPITokenTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aPITokenTbl);
        }

        // GET: APITokenTbls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            APITokenTbl aPITokenTbl = db.APITokenTbls.Find(id);
            if (aPITokenTbl == null)
            {
                return HttpNotFound();
            }
            return View(aPITokenTbl);
        }

        // POST: APITokenTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            APITokenTbl aPITokenTbl = db.APITokenTbls.Find(id);
            db.APITokenTbls.Remove(aPITokenTbl);
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
