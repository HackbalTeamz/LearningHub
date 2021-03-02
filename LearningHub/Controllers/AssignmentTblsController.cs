using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LearningHub.Models;

namespace LearningHub.Controllers
{
    [Authorized(Roles = ContantValue.Staff)]
    public class AssignmentTblsController : BaseController
    {

        // GET: AssignmentTbls
        public ActionResult Index()
        {
            long LoginID = Convert.ToInt64(FormsAuthentication.Decrypt(HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name.Split('|')[0]);
            List<AssignmentTbl> assignmentTbls = new List<AssignmentTbl>();
            foreach (var staffclassdtl in db.StaffAssignClassTbls.Where(x => x.StaffID == LoginID))
            {
                foreach (var chapid in staffclassdtl.ClassTbl.SubjectTbls)
                {
                    foreach (var item in chapid.AssignmentTbls.Where(x=>x.SubjectID == chapid.SubjectID))
                    {
                        assignmentTbls.Add(item);
                    }
                }
            }
            return View(assignmentTbls.ToList());
        }

        // GET: AssignmentTbls/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignmentTbl assignmentTbl = db.AssignmentTbls.Find(id);
            if (assignmentTbl == null)
            {
                return HttpNotFound();
            }
            return View(assignmentTbl);
        }

        // GET: AssignmentTbls/Create
        public ActionResult Create()
        {
            long LoginID = Convert.ToInt64(FormsAuthentication.Decrypt(HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name.Split('|')[0]);
            List<SubjectTbl> sublist = new List<SubjectTbl>();
            foreach (var staffclassdtl in db.StaffAssignClassTbls.Where(x => x.StaffID == LoginID))
            {
                foreach (var chapid in staffclassdtl.ClassTbl.SubjectTbls)
                {
                    SubjectTbl classbj = new SubjectTbl();
                    classbj.SubjectID = chapid.SubjectID;
                    classbj.SubjectName = chapid.ClassTbl.DepartmentTbl.DeptName + " - " + chapid.ClassTbl.ClassName + " - " + chapid.SubjectName;
                    sublist.Add(classbj);
                }
            }
            ViewBag.SubjectID = new SelectList(sublist, "SubjectID", "SubjectName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AssignmentTbl assignmentTbl)
        {
            if (ModelState.IsValid)
            {
                db.AssignmentTbls.Add(assignmentTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            long LoginID = Convert.ToInt64(FormsAuthentication.Decrypt(HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name.Split('|')[0]);
            List<SubjectTbl> sublist = new List<SubjectTbl>();
            foreach (var staffclassdtl in db.StaffAssignClassTbls.Where(x => x.StaffID == LoginID))
            {
                foreach (var chapid in staffclassdtl.ClassTbl.SubjectTbls)
                {
                    SubjectTbl classbj = new SubjectTbl();
                    classbj.SubjectID = chapid.SubjectID;
                    classbj.SubjectName = chapid.ClassTbl.DepartmentTbl.DeptName + " - " + chapid.ClassTbl.ClassName + " - " + chapid.SubjectName;
                    sublist.Add(classbj);
                }
            }
            ViewBag.SubjectID = new SelectList(sublist, "SubjectID", "SubjectName");
            return View(assignmentTbl);
        }

        // GET: AssignmentTbls/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignmentTbl assignmentTbl = db.AssignmentTbls.Find(id);
            if (assignmentTbl == null)
            {
                return HttpNotFound();
            }
            long LoginID = Convert.ToInt64(FormsAuthentication.Decrypt(HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name.Split('|')[0]);
            List<SubjectTbl> sublist = new List<SubjectTbl>();
            foreach (var staffclassdtl in db.StaffAssignClassTbls.Where(x => x.StaffID == LoginID))
            {
                foreach (var chapid in staffclassdtl.ClassTbl.SubjectTbls)
                {
                    SubjectTbl classbj = new SubjectTbl();
                    classbj.SubjectID = chapid.SubjectID;
                    classbj.SubjectName = chapid.ClassTbl.DepartmentTbl.DeptName + " - " + chapid.ClassTbl.ClassName + " - " + chapid.SubjectName;
                    sublist.Add(classbj);
                }
            }
            ViewBag.SubjectID = new SelectList(sublist, "SubjectID", "SubjectName");
            return View(assignmentTbl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AssignmentTbl assignmentTbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignmentTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            long LoginID = Convert.ToInt64(FormsAuthentication.Decrypt(HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name.Split('|')[0]);
            List<SubjectTbl> sublist = new List<SubjectTbl>();
            foreach (var staffclassdtl in db.StaffAssignClassTbls.Where(x => x.StaffID == LoginID))
            {
                foreach (var chapid in staffclassdtl.ClassTbl.SubjectTbls)
                {
                    SubjectTbl classbj = new SubjectTbl();
                    classbj.SubjectID = chapid.SubjectID;
                    classbj.SubjectName = chapid.ClassTbl.DepartmentTbl.DeptName + " - " + chapid.ClassTbl.ClassName + " - " + chapid.SubjectName;
                    sublist.Add(classbj);
                }
            }
            ViewBag.SubjectID = new SelectList(sublist, "SubjectID", "SubjectName");
            return View(assignmentTbl);
        }

        // GET: AssignmentTbls/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignmentTbl assignmentTbl = db.AssignmentTbls.Find(id);
            if (assignmentTbl == null)
            {
                return HttpNotFound();
            }
            return View(assignmentTbl);
        }

        // POST: AssignmentTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            AssignmentTbl assignmentTbl = db.AssignmentTbls.Find(id);
            db.AssignmentTbls.Remove(assignmentTbl);
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
        public ActionResult StudentAssignmentList()
        {
            long LoginID = Convert.ToInt64(FormsAuthentication.Decrypt(HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name.Split('|')[0]);

            return View();
        }
    }
}
