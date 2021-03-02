using LearningHub.Helper;
using LearningHub.Models;
using LearningHub.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LearningHub.Controllers
{
    [Authorized(Roles = ContantValue.Student)]
    public class StudentController : Controller
    {
        private Entities db = new Entities();
        
        public ActionResult StudyMeterials()
        {
            long LoginID = Convert.ToInt64(FormsAuthentication.Decrypt(HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name.Split('|')[0]);
            StudentTbl studenttblobj = db.StudentTbls.Find(LoginID);
            StudentDashboardVM studentDashboardVM = new StudentDashboardVM();
            studentDashboardVM.subjectTbls = db.SubjectTbls.Where(x => x.ClassID == studenttblobj.ClassID).ToList();
            return View(studentDashboardVM);
        }
        public ActionResult ChapterWiseMeterial(long id)
        {
            StudentDashboardVM studentDashboardVM = new StudentDashboardVM();
            studentDashboardVM.studyMeterialTbls = new List<StudyMeterialTbl>();
            foreach(var item in db.StudyMeterialTbls.Where(x => x.ChapterID == id))
            {
                if(Convert.ToDateTime(item.PublishedOn) <= DateTime.Now.Date)
                {
                    studentDashboardVM.studyMeterialTbls.Add(item);
                }
                
            }
            return View(studentDashboardVM);
        }
        public void AttendanceAdd(long id)
        {
            new AttendanceHelper().StudentsAttendanceMinute(id);
        }

        //New Design
        public ActionResult Dashboard()
        {
            long LoginID = Convert.ToInt64(FormsAuthentication.Decrypt(HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name.Split('|')[0]);
            StudentDashboardVM studentDashboardVM = new StudentDashboardVM();
            studentDashboardVM.subjectVideoVMs = new List<SubjectVideoVM>();
            List<StudentNotificationVM> studntnotfvm = new List<StudentNotificationVM>();
            studentDashboardVM.studentNotificationVMs = new List<StudentNotificationVM>();
            StudentTbl studenttblobj = db.StudentTbls.Find(LoginID);
            foreach(var subj in db.SubjectTbls.Where(x => x.ClassID == studenttblobj.ClassID).ToList())
            {
                SubjectVideoVM subjectVideoVM = new SubjectVideoVM();
                subjectVideoVM.VideoCount = 0;
                subjectVideoVM.SubjectName = subj.SubjectName;
                foreach(var chap in subj.ChapterTbls)
                {
                    subjectVideoVM.VideoCount = subjectVideoVM.VideoCount + chap.VideoTbls.Count;
                    foreach (var item in chap.VideoTbls)
                    {
                        StudentNotificationVM studentNotificationVM = new StudentNotificationVM();
                        studentNotificationVM.Message = item.VideoTitle;
                        studentNotificationVM.Time = Convert.ToString(item.PublishedOn);
                        studentNotificationVM.LinkID = chap.ChapterID;
                        studntnotfvm.Add(studentNotificationVM);
                    }
                }
                
                studentDashboardVM.subjectVideoVMs.Add(subjectVideoVM);
            }
            studntnotfvm = studntnotfvm.OrderByDescending(x => x.Time).Take(10).ToList();
            studentDashboardVM.studentNotificationVMs.AddRange(studntnotfvm);
            return View(studentDashboardVM);
        }

        
    }
}