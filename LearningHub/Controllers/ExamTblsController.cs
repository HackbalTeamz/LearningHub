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
    [Authorized(Roles = ContantValue.Staff)]
    public class ExamTblsController : Controller
    {
        private Entities db = new Entities();

        // GET: ExamTbls
        public ActionResult Index()
        {
            var examTbls = db.ExamTbls.Include(e => e.SubjectTbl);
            return View(examTbls.ToList());
        }

        // GET: ExamTbls/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamTbl examTbl = db.ExamTbls.Find(id);
            if (examTbl == null)
            {
                return HttpNotFound();
            }
            return View(examTbl);
        }

        // GET: ExamTbls/Create
        public ActionResult Create()
        {
            ViewBag.SubjectID = new SelectList(db.SubjectTbls, "SubjectID", "SubjectName");
            return View();
        }

        // POST: ExamTbls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExamID,SubjectID,ExamName,ConductDate")] ExamTbl examTbl)
        {
            if (ModelState.IsValid)
            {
                db.ExamTbls.Add(examTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SubjectID = new SelectList(db.SubjectTbls, "SubjectID", "SubjectName", examTbl.SubjectID);
            return View(examTbl);
        }

        // GET: ExamTbls/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamTbl examTbl = db.ExamTbls.Find(id);
            if (examTbl == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubjectID = new SelectList(db.SubjectTbls, "SubjectID", "SubjectName", examTbl.SubjectID);
            return View(examTbl);
        }

        // POST: ExamTbls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExamID,SubjectID,ExamName,ConductDate")] ExamTbl examTbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(examTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SubjectID = new SelectList(db.SubjectTbls, "SubjectID", "SubjectName", examTbl.SubjectID);
            return View(examTbl);
        }

        // GET: ExamTbls/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamTbl examTbl = db.ExamTbls.Find(id);
            if (examTbl == null)
            {
                return HttpNotFound();
            }
            return View(examTbl);
        }

        // POST: ExamTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ExamTbl examTbl = db.ExamTbls.Find(id);
            db.ExamTbls.Remove(examTbl);
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
