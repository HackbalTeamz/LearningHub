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
    public class ClassTblsController : Controller
    {
        private Entities db = new Entities();

        // GET: ClassTbls
        public ActionResult Index()
        {
            var classTbls = db.ClassTbls.Include(c => c.DepartmentTbl);
            return View(classTbls.ToList());
        }

        // GET: ClassTbls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassTbl classTbl = db.ClassTbls.Find(id);
            if (classTbl == null)
            {
                return HttpNotFound();
            }
            return View(classTbl);
        }

        // GET: ClassTbls/Create
        public ActionResult Create()
        {
            ViewBag.DeptID = new SelectList(db.DepartmentTbls, "DeptID", "DeptName");
            return View();
        }

        // POST: ClassTbls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClassID,DeptID,ClassName")] ClassTbl classTbl)
        {
            if (ModelState.IsValid)
            {
                db.ClassTbls.Add(classTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeptID = new SelectList(db.DepartmentTbls, "DeptID", "DeptName", classTbl.DeptID);
            return View(classTbl);
        }

        // GET: ClassTbls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassTbl classTbl = db.ClassTbls.Find(id);
            if (classTbl == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeptID = new SelectList(db.DepartmentTbls, "DeptID", "DeptName", classTbl.DeptID);
            return View(classTbl);
        }

        // POST: ClassTbls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClassID,DeptID,ClassName")] ClassTbl classTbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeptID = new SelectList(db.DepartmentTbls, "DeptID", "DeptName", classTbl.DeptID);
            return View(classTbl);
        }

        // GET: ClassTbls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassTbl classTbl = db.ClassTbls.Find(id);
            if (classTbl == null)
            {
                return HttpNotFound();
            }
            return View(classTbl);
        }

        // POST: ClassTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClassTbl classTbl = db.ClassTbls.Find(id);
            db.ClassTbls.Remove(classTbl);
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
