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
    public class MesSendTblsController : Controller
    {
        private Entities db = new Entities();

        // GET: MesSendTbls
        public ActionResult Index()
        {
            var mesSendTbls = db.MesSendTbls.Include(m => m.ParentTbl).Include(m => m.MsgTransTbl);
            return View(mesSendTbls.ToList());
        }

        // GET: MesSendTbls/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MesSendTbl mesSendTbl = db.MesSendTbls.Find(id);
            if (mesSendTbl == null)
            {
                return HttpNotFound();
            }
            return View(mesSendTbl);
        }

        // GET: MesSendTbls/Create
        public ActionResult Create()
        {
            ViewBag.ParentID = new SelectList(db.ParentTbls, "ParentID", "ParentName");
            ViewBag.MsgTransID = new SelectList(db.MsgTransTbls, "MsgTransID", "ErrCode");
            return View();
        }

        // POST: MesSendTbls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MsgSendID,MsgTransID,ParentID,MessageText")] MesSendTbl mesSendTbl)
        {
            if (ModelState.IsValid)
            {
                db.MesSendTbls.Add(mesSendTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ParentID = new SelectList(db.ParentTbls, "ParentID", "ParentName", mesSendTbl.ParentID);
            ViewBag.MsgTransID = new SelectList(db.MsgTransTbls, "MsgTransID", "ErrCode", mesSendTbl.MsgTransID);
            return View(mesSendTbl);
        }

        // GET: MesSendTbls/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MesSendTbl mesSendTbl = db.MesSendTbls.Find(id);
            if (mesSendTbl == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentID = new SelectList(db.ParentTbls, "ParentID", "ParentName", mesSendTbl.ParentID);
            ViewBag.MsgTransID = new SelectList(db.MsgTransTbls, "MsgTransID", "ErrCode", mesSendTbl.MsgTransID);
            return View(mesSendTbl);
        }

        // POST: MesSendTbls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MsgSendID,MsgTransID,ParentID,MessageText")] MesSendTbl mesSendTbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mesSendTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParentID = new SelectList(db.ParentTbls, "ParentID", "ParentName", mesSendTbl.ParentID);
            ViewBag.MsgTransID = new SelectList(db.MsgTransTbls, "MsgTransID", "ErrCode", mesSendTbl.MsgTransID);
            return View(mesSendTbl);
        }

        // GET: MesSendTbls/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MesSendTbl mesSendTbl = db.MesSendTbls.Find(id);
            if (mesSendTbl == null)
            {
                return HttpNotFound();
            }
            return View(mesSendTbl);
        }

        // POST: MesSendTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            MesSendTbl mesSendTbl = db.MesSendTbls.Find(id);
            db.MesSendTbls.Remove(mesSendTbl);
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
