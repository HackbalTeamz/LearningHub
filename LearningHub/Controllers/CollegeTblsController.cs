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
    public class CollegeTblsController : Controller
    {
        private Entities db = new Entities();

        // GET: CollegeTbls
        public ActionResult Index()
        {
            return View(db.CollegeTbls.ToList());
        }

        // GET: CollegeTbls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CollegeTbl collegeTbl = db.CollegeTbls.Find(id);
            if (collegeTbl == null)
            {
                return HttpNotFound();
            }
            return View(collegeTbl);
        }

        // GET: CollegeTbls/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CollegeTbls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CollegeID,CollegeName")] CollegeTbl collegeTbl)
        {
            if (ModelState.IsValid)
            {
                db.CollegeTbls.Add(collegeTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(collegeTbl);
        }

        // GET: CollegeTbls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CollegeTbl collegeTbl = db.CollegeTbls.Find(id);
            if (collegeTbl == null)
            {
                return HttpNotFound();
            }
            return View(collegeTbl);
        }

        // POST: CollegeTbls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CollegeID,CollegeName")] CollegeTbl collegeTbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(collegeTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(collegeTbl);
        }

        // GET: CollegeTbls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CollegeTbl collegeTbl = db.CollegeTbls.Find(id);
            if (collegeTbl == null)
            {
                return HttpNotFound();
            }
            return View(collegeTbl);
        }

        // POST: CollegeTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CollegeTbl collegeTbl = db.CollegeTbls.Find(id);
            db.CollegeTbls.Remove(collegeTbl);
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
