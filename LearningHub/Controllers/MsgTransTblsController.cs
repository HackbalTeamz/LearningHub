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
    public class MsgTransTblsController : Controller
    {
        private Entities db = new Entities();

        // GET: MsgTransTbls
        public ActionResult Index()
        {
            return View(db.MsgTransTbls.ToList());
        }

        // GET: MsgTransTbls/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MsgTransTbl msgTransTbl = db.MsgTransTbls.Find(id);
            if (msgTransTbl == null)
            {
                return HttpNotFound();
            }
            return View(msgTransTbl);
        }

        // GET: MsgTransTbls/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MsgTransTbls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MsgTransID,ErrCode,ErrMsg,JobId")] MsgTransTbl msgTransTbl)
        {
            if (ModelState.IsValid)
            {
                db.MsgTransTbls.Add(msgTransTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(msgTransTbl);
        }

        // GET: MsgTransTbls/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MsgTransTbl msgTransTbl = db.MsgTransTbls.Find(id);
            if (msgTransTbl == null)
            {
                return HttpNotFound();
            }
            return View(msgTransTbl);
        }

        // POST: MsgTransTbls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MsgTransID,ErrCode,ErrMsg,JobId")] MsgTransTbl msgTransTbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(msgTransTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(msgTransTbl);
        }

        // GET: MsgTransTbls/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MsgTransTbl msgTransTbl = db.MsgTransTbls.Find(id);
            if (msgTransTbl == null)
            {
                return HttpNotFound();
            }
            return View(msgTransTbl);
        }

        // POST: MsgTransTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            MsgTransTbl msgTransTbl = db.MsgTransTbls.Find(id);
            db.MsgTransTbls.Remove(msgTransTbl);
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
