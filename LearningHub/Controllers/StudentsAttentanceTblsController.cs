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
    public class StudentsAttentanceTblsController : BaseController
    {

        // GET: StudentsAttentanceTbls
        public ActionResult Index()
        {
            var studentsAttentanceTbls = db.StudentsAttentanceTbls.Include(s => s.StudentTbl);
            return View(studentsAttentanceTbls.ToList());
        }

        // GET: StudentsAttentanceTbls/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentsAttentanceTbl studentsAttentanceTbl = db.StudentsAttentanceTbls.Find(id);
            if (studentsAttentanceTbl == null)
            {
                return HttpNotFound();
            }
            return View(studentsAttentanceTbl);
        }

        // GET: StudentsAttentanceTbls/Create
        public ActionResult Create()
        {
            ViewBag.StudentID = new SelectList(db.StudentTbls, "StudentID", "StudentName");
            return View();
        }

        // POST: StudentsAttentanceTbls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentsAttentanceTbl studentsAttentanceTbl)
        {
            if (ModelState.IsValid)
            {
                db.StudentsAttentanceTbls.Add(studentsAttentanceTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudentID = new SelectList(db.StudentTbls, "StudentID", "StudentName", studentsAttentanceTbl.StudentID);
            return View(studentsAttentanceTbl);
        }

        // GET: StudentsAttentanceTbls/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentsAttentanceTbl studentsAttentanceTbl = db.StudentsAttentanceTbls.Find(id);
            if (studentsAttentanceTbl == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentID = new SelectList(db.StudentTbls, "StudentID", "StudentName", studentsAttentanceTbl.StudentID);
            return View(studentsAttentanceTbl);
        }

        // POST: StudentsAttentanceTbls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentAttentanceID,StudentID,PresentDate,IsLogin,IsLogout,LoginMinites")] StudentsAttentanceTbl studentsAttentanceTbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentsAttentanceTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentID = new SelectList(db.StudentTbls, "StudentID", "StudentName", studentsAttentanceTbl.StudentID);
            return View(studentsAttentanceTbl);
        }

        // GET: StudentsAttentanceTbls/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentsAttentanceTbl studentsAttentanceTbl = db.StudentsAttentanceTbls.Find(id);
            if (studentsAttentanceTbl == null)
            {
                return HttpNotFound();
            }
            return View(studentsAttentanceTbl);
        }

        // POST: StudentsAttentanceTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            StudentsAttentanceTbl studentsAttentanceTbl = db.StudentsAttentanceTbls.Find(id);
            db.StudentsAttentanceTbls.Remove(studentsAttentanceTbl);
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
