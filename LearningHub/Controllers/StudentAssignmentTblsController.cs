using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LearningHub.Models;
using LearningHub.ViewModel;

namespace LearningHub.Controllers
{
    [Authorized(Roles = ContantValue.Student)]
    public class StudentAssignmentTblsController : Controller
    {
        private Entities db = new Entities();

        // GET: StudentAssignmentTbls
        public ActionResult Index()
        {
            long LoginID = Convert.ToInt64(FormsAuthentication.Decrypt(HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name.Split('|')[0]);
            StudentDashboardVM studentDashboardVM = new StudentDashboardVM();
            studentDashboardVM.studentAssignmentTbls = db.StudentAssignmentTbls.Include(s => s.AssignmentTbl).Include(s => s.StudentTbl).Where(x => x.StudentID == LoginID);
            return View(studentDashboardVM);
        }

        
        public ActionResult Create()
        {
            long LoginID = Convert.ToInt64(FormsAuthentication.Decrypt(HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name.Split('|')[0]);
            List<AssignmentTbl> assignmentTbllist = new List<AssignmentTbl>();
            using (var context = new Entities())
            {
                StudentTbl studentTbl = context.StudentTbls.Find(LoginID);
                List<SubjectTbl> subjectTbllist = context.SubjectTbls.Where(x => x.ClassID == studentTbl.ClassID).ToList();
                foreach (var item in subjectTbllist)
                {
                    foreach(var item2 in item.AssignmentTbls)
                    {
                        if(Convert.ToDateTime(item2.PublishedOn).Date <= Convert.ToDateTime(DateTime.Now.Date))
                        {
                            item2.Topic = item2.Topic +" - Description :- " + item2.Description + " - Last Date :- " + Convert.ToDateTime(item2.DeadLineOn).Date;
                            assignmentTbllist.Add(item2);
                        }
                        
                    }
                    
                }
            }
            StudentDashboardVM studentDashboardVM = new StudentDashboardVM();
            studentDashboardVM.studentNotificationVMs = null;
            ViewBag.AssignmentID = new SelectList(assignmentTbllist, "AssignmentID", "Topic");
            ViewBag.StudentID = new SelectList(db.StudentTbls, "StudentID", "StudentName");
            return View(studentDashboardVM);
        }

        // POST: StudentAssignmentTbls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentAssignmentTbl studentAssignmentTbl, HttpPostedFileBase file)
        {
            long LoginID = Convert.ToInt64(FormsAuthentication.Decrypt(HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name.Split('|')[0]);
            if (ModelState.IsValid)
            {
                try
                {
                    if (file.ContentLength > 0)
                    {
                        string _Extenstion = Path.GetExtension(file.FileName);
                        if (_Extenstion == ".pdf")
                        {
                            //string _FileName = Path.GetFileName(file.FileName);
                            string _FileName = Path.GetFileName(file.FileName);
                            string _path = Path.Combine(Server.MapPath("~/StudentAssignment"), _FileName);  //Folder Name ee Blue
                            file.SaveAs(_path);
                            studentAssignmentTbl.StudentID = LoginID;
                            studentAssignmentTbl.SubmittedDate = DateTime.Now;
                            studentAssignmentTbl.MeterailPath = _FileName;
                            db.StudentAssignmentTbls.Add(studentAssignmentTbl);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        ModelState.AddModelError("MeterailPath", "Not Valid File Type");
                    }

                }
                catch
                {
                    ModelState.AddModelError("MeterailPath", "Uploading Failed");
                }
            }
            ModelState.AddModelError("MeterailPath", "Validation Failed");
            
            List<AssignmentTbl> assignmentTbllist = new List<AssignmentTbl>();
            using (var context = new Entities())
            {
                StudentTbl studentTbl = context.StudentTbls.Find(LoginID);
                List<SubjectTbl> subjectTbllist = context.SubjectTbls.Where(x => x.ClassID == studentTbl.ClassID).ToList();
                foreach (var item in subjectTbllist)
                {
                    assignmentTbllist.AddRange(item.AssignmentTbls);
                }
            }
            StudentDashboardVM studentDashboardVM = new StudentDashboardVM();
            studentDashboardVM.studentNotificationVMs = null;
            ViewBag.AssignmentID = new SelectList(assignmentTbllist, "AssignmentID", "Topic");
            ViewBag.StudentID = new SelectList(db.StudentTbls, "StudentID", "StudentName");
            return View(studentDashboardVM);
        }

        // GET: StudentAssignmentTbls/Edit/5
        //public ActionResult Edit(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    StudentAssignmentTbl studentAssignmentTbl = db.StudentAssignmentTbls.Find(id);
        //    if (studentAssignmentTbl == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.AssignmentID = new SelectList(db.AssignmentTbls, "AssignmentID", "Topic", studentAssignmentTbl.AssignmentID);
        //    ViewBag.AssignmentID = new SelectList(db.AssignmentTbls, "AssignmentID", "Topic", studentAssignmentTbl.AssignmentID);
        //    ViewBag.StudentID = new SelectList(db.StudentTbls, "StudentID", "StudentName", studentAssignmentTbl.StudentID);
        //    return View(studentAssignmentTbl);
        //}

        // POST: StudentAssignmentTbls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "StudentAssignmentID,StudentID,AssignmentID,MeterailPath,SubmittedDate")] StudentAssignmentTbl studentAssignmentTbl)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(studentAssignmentTbl).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.AssignmentID = new SelectList(db.AssignmentTbls, "AssignmentID", "Topic", studentAssignmentTbl.AssignmentID);
        //    ViewBag.AssignmentID = new SelectList(db.AssignmentTbls, "AssignmentID", "Topic", studentAssignmentTbl.AssignmentID);
        //    ViewBag.StudentID = new SelectList(db.StudentTbls, "StudentID", "StudentName", studentAssignmentTbl.StudentID);
        //    return View(studentAssignmentTbl);
        //}

        // GET: StudentAssignmentTbls/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentAssignmentTbl studentAssignmentTbl = db.StudentAssignmentTbls.Find(id);
            if (studentAssignmentTbl == null)
            {
                return HttpNotFound();
            }
            return View(studentAssignmentTbl);
        }

        // POST: StudentAssignmentTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            StudentAssignmentTbl studentAssignmentTbl = db.StudentAssignmentTbls.Find(id);
            string _path = Path.Combine(Server.MapPath("~/StudentAssignment"), studentAssignmentTbl.MeterailPath);
            if (System.IO.File.Exists(_path))
            {
                System.IO.File.Delete(_path);
            }
            db.StudentAssignmentTbls.Remove(studentAssignmentTbl);
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
