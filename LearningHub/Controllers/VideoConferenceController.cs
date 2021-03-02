using LearningHub.Helper;
using LearningHub.Models;
using LearningHub.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LearningHub.Controllers
{
    public class VideoConferenceController : BaseController
    {
        // GET: VideoConference
        public ActionResult LiveClass()
        {
            StudentDashboardVM studentDashboardVM = new StudentDashboardVM();
            return View(studentDashboardVM);
        }
        public ActionResult Videos()
        {
            StudentDashboardVM studentDashboardVM = new StudentDashboardVM();
            studentDashboardVM.subjectTbls = new List<SubjectTbl>();
            //studentDashboardVM.subjectVideoVMs = new List<SubjectVideoVM>();
            try
            {
                long LoginID = Convert.ToInt64(FormsAuthentication.Decrypt(HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name.Split('|')[0]);
                StudentTbl studenttblobj = db.StudentTbls.Find(LoginID);
                List<StudentNotificationVM> studntnotfvm = new List<StudentNotificationVM>();
                studentDashboardVM.studentNotificationVMs = new List<StudentNotificationVM>();
                studentDashboardVM.subjectTbls = db.SubjectTbls.Where(x => x.ClassID == studenttblobj.ClassID).ToList();
                foreach (var subj in studentDashboardVM.subjectTbls)
                {
                    foreach (var chap in subj.ChapterTbls)
                    {
                        foreach (var item in chap.VideoTbls)
                        {
                            StudentNotificationVM studentNotificationVM = new StudentNotificationVM();
                            studentNotificationVM.Message = item.VideoTitle;
                            studentNotificationVM.Time = Convert.ToString(item.PublishedOn);
                            studentNotificationVM.LinkID = chap.ChapterID;
                            studntnotfvm.Add(studentNotificationVM);
                        }
                    }}
                studntnotfvm = studntnotfvm.OrderByDescending(x => x.Time).Take(10).ToList();
                studentDashboardVM.studentNotificationVMs.AddRange(studntnotfvm);
                return View(studentDashboardVM);
            }
            catch(Exception ex)
            {
                log.Error($"Exception in {nameof(Videos)}", ex);
            }
            return View(studentDashboardVM);

        }
        public ActionResult ChapterWiseVideo(long id)
        {
            StudentDashboardVM studentDashboardVM = new StudentDashboardVM();
            studentDashboardVM.videoTbls = new List<VideoTbl>();
            try
            {
                long LoginID = Convert.ToInt64(FormsAuthentication.Decrypt(HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name.Split('|')[0]);
                ChapterTbl chap = db.ChapterTbls.Find(id);
                new AttendanceHelper().StudentIsClassAttent(LoginID, Convert.ToInt64(chap.SubjectID));
                foreach (var item in db.VideoTbls.Where(x => x.ChapterID == id).OrderBy(x => x.DisplayOrder).ToList())
                {
                    if (Convert.ToDateTime(item.PublishedOn).Date <= DateTime.Now.Date)
                    {
                        studentDashboardVM.videoTbls.Add(item);
                    }
                }
            }
            catch(Exception ex)
            {
                log.Error($"Exception in {nameof(ChapterWiseVideo)} , Params {JsonConvert.SerializeObject(studentDashboardVM)}", ex);
            }
            
            return View(studentDashboardVM);
        }
    }
}