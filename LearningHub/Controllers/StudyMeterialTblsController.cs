using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LearningHub.Models;

namespace LearningHub.Controllers
{
    [Authorized(Roles = ContantValue.Staff)]
    public class StudyMeterialTblsController : BaseController
    {

        // GET: StudyMeterialTbls
        public ActionResult Index()
        {
            long LoginID = Convert.ToInt64(FormsAuthentication.Decrypt(HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name.Split('|')[0]);
            List<StudyMeterialTbl> studymetril = new List<StudyMeterialTbl>();
            foreach (var staffclassdtl in db.StaffAssignClassTbls.Where(x => x.StaffID == LoginID))
            {
                foreach (var chapid in staffclassdtl.ClassTbl.SubjectTbls)
                {
                    foreach (var item in chapid.ChapterTbls)
                    {
                        studymetril.AddRange(db.StudyMeterialTbls.Include(s => s.ChapterTbl).Where(x => x.ChapterID == item.ChapterID));
                    }
                }
            }
            return View(studymetril.ToList());
        }

        // GET: StudyMeterialTbls/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudyMeterialTbl studyMeterialTbl = db.StudyMeterialTbls.Find(id);
            if (studyMeterialTbl == null)
            {
                return HttpNotFound();
            }
            return View(studyMeterialTbl);
        }

        // GET: StudyMeterialTbls/Create
        public ActionResult Create()
        {
            List<ChapterTbl> chaplist = new List<ChapterTbl>();
            long LoginID = Convert.ToInt64(FormsAuthentication.Decrypt(HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name.Split('|')[0]);
            foreach(var staffclassdtl in db.StaffAssignClassTbls.Where(x=>x.StaffID == LoginID))
            {
                foreach(var chapid in staffclassdtl.ClassTbl.SubjectTbls)
                {
                    foreach (var item in chapid.ChapterTbls)
                    {
                        ChapterTbl classbj = new ChapterTbl();
                        classbj.ChapterID = item.ChapterID;
                        classbj.ChapterName = item.SubjectTbl.ClassTbl.DepartmentTbl.DeptName + " - " + item.SubjectTbl.ClassTbl.ClassName + " - " + item.SubjectTbl.SubjectName + " - " + item.ChapterName;
                        chaplist.Add(classbj);
                    }
                }
            }
            ViewBag.ChapterID = new SelectList(chaplist, "ChapterID", "ChapterName");
            return View();
        }

        // POST: StudyMeterialTbls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudyMeterialTbl studyMeterialTbl)
        {
            if (ModelState.IsValid)
            {
                studyMeterialTbl.EnteredOn = studyMeterialTbl.UpdatedOn = DateTime.Now;
                studyMeterialTbl.UpdatedOn = studyMeterialTbl.UpdatedOn = DateTime.Now;
                db.StudyMeterialTbls.Add(studyMeterialTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<ChapterTbl> chaplist = new List<ChapterTbl>();
            long LoginID = Convert.ToInt64(FormsAuthentication.Decrypt(HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name.Split('|')[0]);
            foreach (var staffclassdtl in db.StaffAssignClassTbls.Where(x => x.StaffID == LoginID))
            {
                foreach (var chapid in staffclassdtl.ClassTbl.SubjectTbls)
                {
                    foreach (var item in chapid.ChapterTbls)
                    {
                        ChapterTbl classbj = new ChapterTbl();
                        classbj.ChapterID = item.ChapterID;
                        classbj.ChapterName = item.SubjectTbl.ClassTbl.DepartmentTbl.DeptName + " - " + item.SubjectTbl.ClassTbl.ClassName + " - " + item.SubjectTbl.SubjectName + " - " + item.ChapterName;
                        chaplist.Add(classbj);
                    }
                }
            }
            ViewBag.ChapterID = new SelectList(chaplist, "ChapterID", "ChapterName");
            return View(studyMeterialTbl);
        }

        // GET: StudyMeterialTbls/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudyMeterialTbl studyMeterialTbl = db.StudyMeterialTbls.Find(id);
            if (studyMeterialTbl == null)
            {
                return HttpNotFound();
            }
            List<ChapterTbl> chaplist = new List<ChapterTbl>();
            long LoginID = Convert.ToInt64(FormsAuthentication.Decrypt(HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name.Split('|')[0]);
            foreach (var staffclassdtl in db.StaffAssignClassTbls.Where(x => x.StaffID == LoginID))
            {
                foreach (var chapid in staffclassdtl.ClassTbl.SubjectTbls)
                {
                    foreach (var item in chapid.ChapterTbls)
                    {
                        ChapterTbl classbj = new ChapterTbl();
                        classbj.ChapterID = item.ChapterID;
                        classbj.ChapterName = item.SubjectTbl.ClassTbl.DepartmentTbl.DeptName + " - " + item.SubjectTbl.ClassTbl.ClassName + " - " + item.SubjectTbl.SubjectName + " - " + item.ChapterName;
                        chaplist.Add(classbj);
                    }
                }
            }
            ViewBag.ChapterID = new SelectList(chaplist, "ChapterID", "ChapterName");

            return View(studyMeterialTbl);
        }

        // POST: StudyMeterialTbls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudyMeterialTbl studyMeterialTbl)
        {
            if (ModelState.IsValid)
            {
                studyMeterialTbl.UpdatedOn = DateTime.Now;
                db.Entry(studyMeterialTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            List<ChapterTbl> chaplist = new List<ChapterTbl>();
            long LoginID = Convert.ToInt64(FormsAuthentication.Decrypt(HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name.Split('|')[0]);
            foreach (var staffclassdtl in db.StaffAssignClassTbls.Where(x => x.StaffID == LoginID))
            {
                foreach (var chapid in staffclassdtl.ClassTbl.SubjectTbls)
                {
                    foreach (var item in chapid.ChapterTbls)
                    {
                        ChapterTbl classbj = new ChapterTbl();
                        classbj.ChapterID = item.ChapterID;
                        classbj.ChapterName = item.SubjectTbl.ClassTbl.DepartmentTbl.DeptName + " - " + item.SubjectTbl.ClassTbl.ClassName + " - " + item.SubjectTbl.SubjectName + " - " + item.ChapterName;
                        chaplist.Add(classbj);
                    }
                }
            }
            ViewBag.ChapterID = new SelectList(chaplist, "ChapterID", "ChapterName");

            return View(studyMeterialTbl);
        }

        // GET: StudyMeterialTbls/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudyMeterialTbl studyMeterialTbl = db.StudyMeterialTbls.Find(id);
            if (studyMeterialTbl == null)
            {
                return HttpNotFound();
            }
            return View(studyMeterialTbl);
        }

        // POST: StudyMeterialTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            StudyMeterialTbl studyMeterialTbl = db.StudyMeterialTbls.Find(id);
            db.StudyMeterialTbls.Remove(studyMeterialTbl);
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
