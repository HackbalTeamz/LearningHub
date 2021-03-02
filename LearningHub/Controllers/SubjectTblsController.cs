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
    public class SubjectTblsController : Controller
    {
        
        private Entities db = new Entities();

        // GET: SubjectTbls
        public ActionResult Index()
        {
            var subjectTbls = db.SubjectTbls.Include(s => s.ClassTbl);
            return View(subjectTbls.ToList());
        }

        // GET: SubjectTbls/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubjectTbl subjectTbl = db.SubjectTbls.Find(id);
            if (subjectTbl == null)
            {
                return HttpNotFound();
            }
            return View(subjectTbl);
        }

        // GET: SubjectTbls/Create
        public ActionResult Create()
        {
            List<ClassTbl> classlist = new List<ClassTbl>();
            foreach (var item in db.ClassTbls)
            {
                ClassTbl classbj = new ClassTbl();
                classbj.ClassID = item.ClassID;
                classbj.ClassName = item.DepartmentTbl.DeptName + " - " + item.ClassName;
                classlist.Add(classbj);
            }
            ViewBag.ClassID = new SelectList(classlist, "ClassID", "ClassName");
            return View();
        }

        // POST: SubjectTbls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubjectTbl subjectTbl)
        {
            if (ModelState.IsValid)
            {
                db.SubjectTbls.Add(subjectTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            List<ClassTbl> classlist = new List<ClassTbl>();
            foreach (var item in db.ClassTbls)
            {
                ClassTbl classbj = new ClassTbl();
                classbj.ClassID = item.ClassID;
                classbj.ClassName = item.DepartmentTbl.DeptName + " - " + item.ClassName;
                classlist.Add(classbj);
            }
            ViewBag.ClassID = new SelectList(classlist, "ClassID", "ClassName");
            return View(subjectTbl);
        }

        // GET: SubjectTbls/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubjectTbl subjectTbl = db.SubjectTbls.Find(id);
            if (subjectTbl == null)
            {
                return HttpNotFound();
            }
            List<ClassTbl> classlist = new List<ClassTbl>();
            foreach (var item in db.ClassTbls)
            {
                ClassTbl classbj = new ClassTbl();
                classbj.ClassID = item.ClassID;
                classbj.ClassName = item.DepartmentTbl.DeptName + " - " + item.ClassName;
                classlist.Add(classbj);
            }
            ViewBag.ClassID = new SelectList(classlist, "ClassID", "ClassName");
            return View(subjectTbl);
        }

        // POST: SubjectTbls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SubjectTbl subjectTbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subjectTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<ClassTbl> classlist = new List<ClassTbl>();
            foreach (var item in db.ClassTbls)
            {
                ClassTbl classbj = new ClassTbl();
                classbj.ClassID = item.ClassID;
                classbj.ClassName = item.DepartmentTbl.DeptName + " - " + item.ClassName;
                classlist.Add(classbj);
            }
            ViewBag.ClassID = new SelectList(classlist, "ClassID", "ClassName");
            return View(subjectTbl);
        }

        // GET: SubjectTbls/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubjectTbl subjectTbl = db.SubjectTbls.Find(id);
            if (subjectTbl == null)
            {
                return HttpNotFound();
            }
            return View(subjectTbl);
        }

        // POST: SubjectTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            SubjectTbl subjectTbl = db.SubjectTbls.Find(id);
            db.SubjectTbls.Remove(subjectTbl);
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
