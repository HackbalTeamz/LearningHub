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
    public class DepartmentTblsController : Controller
    {
        private Entities db = new Entities();

        // GET: DepartmentTbls
        public ActionResult Index()
        {
            return View(db.DepartmentTbls.ToList());
        }

        // GET: DepartmentTbls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentTbl departmentTbl = db.DepartmentTbls.Find(id);
            if (departmentTbl == null)
            {
                return HttpNotFound();
            }
            return View(departmentTbl);
        }

        // GET: DepartmentTbls/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DepartmentTbls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DeptID,DeptName")] DepartmentTbl departmentTbl)
        {
            if (ModelState.IsValid)
            {
                db.DepartmentTbls.Add(departmentTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(departmentTbl);
        }

        // GET: DepartmentTbls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentTbl departmentTbl = db.DepartmentTbls.Find(id);
            if (departmentTbl == null)
            {
                return HttpNotFound();
            }
            return View(departmentTbl);
        }

        // POST: DepartmentTbls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeptID,DeptName")] DepartmentTbl departmentTbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(departmentTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(departmentTbl);
        }

        // GET: DepartmentTbls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentTbl departmentTbl = db.DepartmentTbls.Find(id);
            if (departmentTbl == null)
            {
                return HttpNotFound();
            }
            return View(departmentTbl);
        }

        // POST: DepartmentTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DepartmentTbl departmentTbl = db.DepartmentTbls.Find(id);
            db.DepartmentTbls.Remove(departmentTbl);
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
