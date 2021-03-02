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
    public class StaffAssignClassTblsController : Controller
    {
        private Entities db = new Entities();

        // GET: StaffAssignClassTbls
        public ActionResult Index()
        {
            var staffAssignClassTbls = db.StaffAssignClassTbls.Include(s => s.ClassTbl).Include(s => s.StaffTbl);
            return View(staffAssignClassTbls.ToList());
        }

        // GET: StaffAssignClassTbls/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffAssignClassTbl staffAssignClassTbl = db.StaffAssignClassTbls.Find(id);
            if (staffAssignClassTbl == null)
            {
                return HttpNotFound();
            }
            return View(staffAssignClassTbl);
        }

        // GET: StaffAssignClassTbls/Create
        public ActionResult Create()
        {
            List<ClassTbl> chaplist = new List<ClassTbl>();
            foreach (var item in db.ClassTbls)
            {
                ClassTbl classbj = new ClassTbl();
                classbj.ClassID = item.ClassID;
                classbj.ClassName = item.DepartmentTbl.DeptName + " - " + item.ClassName;
                chaplist.Add(classbj);
            }
            ViewBag.ClassID = new SelectList(chaplist, "ClassID", "ClassName");
            ViewBag.StaffID = new SelectList(db.StaffTbls, "StaffID", "StaffName");
            return View();
        }

        // POST: StaffAssignClassTbls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AllotedClassID,StaffID,ClassID")] StaffAssignClassTbl staffAssignClassTbl)
        {
            if (ModelState.IsValid)
            {
                db.StaffAssignClassTbls.Add(staffAssignClassTbl);
                db.SaveChanges();
                StaffTbl staffTbl = db.StaffTbls.Find(staffAssignClassTbl.StaffID);
                ClassTbl classTbl = db.ClassTbls.Find(staffAssignClassTbl.ClassID);
                staffTbl.DeptID = classTbl.DeptID;
                db.Entry(staffTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<ClassTbl> chaplist = new List<ClassTbl>();
            foreach (var item in db.ClassTbls)
            {
                ClassTbl classbj = new ClassTbl();
                classbj.ClassID = item.ClassID;
                classbj.ClassName = item.DepartmentTbl.DeptName + " - " + item.ClassName;
                chaplist.Add(classbj);
            }
            ViewBag.ClassID = new SelectList(chaplist, "ClassID", "ClassName", staffAssignClassTbl.ClassID);
            ViewBag.StaffID = new SelectList(db.StaffTbls, "StaffID", "StaffName", staffAssignClassTbl.StaffID);
            return View(staffAssignClassTbl);
        }

        // GET: StaffAssignClassTbls/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffAssignClassTbl staffAssignClassTbl = db.StaffAssignClassTbls.Find(id);
            if (staffAssignClassTbl == null)
            {
                return HttpNotFound();
            }
            List<ClassTbl> chaplist = new List<ClassTbl>();
            foreach (var item in db.ClassTbls)
            {
                ClassTbl classbj = new ClassTbl();
                classbj.ClassID = item.ClassID;
                classbj.ClassName = item.DepartmentTbl.DeptName + " - " + item.ClassName;
                chaplist.Add(classbj);
            }
            ViewBag.ClassID = new SelectList(chaplist, "ClassID", "ClassName", staffAssignClassTbl.ClassID);
            ViewBag.StaffID = new SelectList(db.StaffTbls, "StaffID", "StaffName", staffAssignClassTbl.StaffID);
            return View(staffAssignClassTbl);
        }

        // POST: StaffAssignClassTbls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AllotedClassID,StaffID,ClassID")] StaffAssignClassTbl staffAssignClassTbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staffAssignClassTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<ClassTbl> chaplist = new List<ClassTbl>();
            foreach (var item in db.ClassTbls)
            {
                ClassTbl classbj = new ClassTbl();
                classbj.ClassID = item.ClassID;
                classbj.ClassName = item.DepartmentTbl.DeptName + " - " + item.ClassName;
                chaplist.Add(classbj);
            }
            ViewBag.ClassID = new SelectList(chaplist, "ClassID", "ClassName", staffAssignClassTbl.ClassID);
            ViewBag.StaffID = new SelectList(db.StaffTbls, "StaffID", "StaffName", staffAssignClassTbl.StaffID);
            return View(staffAssignClassTbl);
        }

        // GET: StaffAssignClassTbls/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffAssignClassTbl staffAssignClassTbl = db.StaffAssignClassTbls.Find(id);
            if (staffAssignClassTbl == null)
            {
                return HttpNotFound();
            }
            return View(staffAssignClassTbl);
        }

        // POST: StaffAssignClassTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            StaffAssignClassTbl staffAssignClassTbl = db.StaffAssignClassTbls.Find(id);
            db.StaffAssignClassTbls.Remove(staffAssignClassTbl);
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
