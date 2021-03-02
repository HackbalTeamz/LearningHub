using LearningHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Core.Metadata.Edm;
using System.Net;
using LearningHub.ViewModel;
using System.Web.Security;

namespace LearningHub.Controllers
{
    [Authorized(Roles = ContantValue.Student)]
    public class ExamsController : BaseController
    {
        // GET: Exams
        [HttpPost]
        public ActionResult Index(long ExamID)
        {
            long LoginID = Convert.ToInt64(FormsAuthentication.Decrypt(HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name.Split('|')[0]);
            StudentExamTbl studentExamTbl = db.StudentExamTbls.Where(x => x.ExamID == ExamID && x.StudentID == LoginID).FirstOrDefault();
            StudentExamResultTbl studentExamResultTbl = new StudentExamResultTbl();
            if (studentExamTbl == null)
            {
                studentExamTbl = new StudentExamTbl();
                studentExamTbl.ExamID = ExamID;
                studentExamTbl.StudentID = LoginID;
                db.StudentExamTbls.Add(studentExamTbl);
                db.SaveChanges();
                var examQuestionTbls = db.ExamQuestionTbls.Include(e => e.ExamTbl).Where(x => x.ExamID == studentExamTbl.ExamID);
                List<long> list = new List<long>();
                foreach (var item in examQuestionTbls)
                {
                    list.Add(item.ExamQuestionID);
                }
                List<long> randomNumberList = new List<long>();
                randomNumberList = GetRandomElements(list, list.Count());
                foreach (var item in randomNumberList)
                {
                    studentExamResultTbl = new StudentExamResultTbl();
                    studentExamResultTbl.StudentExamID = studentExamTbl.StudentExamID;
                    studentExamResultTbl.ExamQuestionID = item;
                    db.StudentExamResultTbls.Add(studentExamResultTbl);
                    db.SaveChanges();
                }
            }
            else
            {
                ErrorVM errorVM = new ErrorVM()
                {
                    ErrorCode = "404",
                    ErrorMsg = "Exam Already Attended, Please Contact Teacher"
                };

                return RedirectToAction("ExamDashboard", "Exams", errorVM);
            }
            return RedirectToAction("ExamQuestion", studentExamResultTbl);
        }
        public ActionResult ExamQuestion(StudentExamResultTbl GetstudentExamResultTbl)
        {
            long LoginID = Convert.ToInt64(FormsAuthentication.Decrypt(HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name.Split('|')[0]);
            StudentExamTbl studentExamTbl = db.StudentExamTbls.Where(x => x.StudentID == LoginID).FirstOrDefault();
            List<StudentExamResultTbl> studentExamResultTbl = new List<StudentExamResultTbl>();
            if (studentExamTbl.StudentExamID == GetstudentExamResultTbl.StudentExamID)
            {
                studentExamResultTbl = db.StudentExamResultTbls.Where(x => x.StudentExamID == GetstudentExamResultTbl.StudentExamID).ToList();
            }
            return View(studentExamResultTbl);
        }
        public ActionResult Question(long id)
        {
            long LoginID = Convert.ToInt64(FormsAuthentication.Decrypt(HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name.Split('|')[0]);
            if (LoginID != 0 || id != 0)
            {
                StudentExamResultTbl studentExamResultTbl = db.StudentExamResultTbls.Find(id);
                return View(studentExamResultTbl);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        }
        public ActionResult FinishExam(long id)
        {
            StudentDashboardVM studentDashboardVM = new StudentDashboardVM();
            OnlineExamVM onlineExamVM = new OnlineExamVM();
            StudentExamTbl studentExamTbl = new StudentExamTbl();
            long ExamConv = Convert.ToInt64(id);
            int Attend = 0;
            int Currect = 0;
            int Wrong = 0;
            long LoginID = Convert.ToInt64(FormsAuthentication.Decrypt(HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name.Split('|')[0]);
            if (LoginID != 0)
            {
                studentExamTbl = db.StudentExamTbls.Find(ExamConv);
                if(studentExamTbl.StudentID == LoginID)
                {
                    List<StudentExamResultTbl> studentExamResultTbl = db.StudentExamResultTbls.Where(x => x.StudentExamID == ExamConv).ToList();
                    foreach(var item in studentExamResultTbl)
                    {
                        if(item.SelectedAnswer!=null)
                        {
                            Attend++;
                            if (item.SelectedAnswer == item.ExamQuestionTbl.Answer)
                            {
                                item.IsTrue = true;
                                Currect++;
                            }
                            else
                            {
                                item.IsTrue = false;
                                Wrong++;
                            }
                            db.Entry(item).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        
                    }
                }
                studentExamTbl.TotalAttendQuestion = Attend;
                studentExamTbl.TotalCurrectAnwer = Currect;
                studentExamTbl.TotalWrongAnswer = Wrong;
                db.Entry(studentExamTbl).State = EntityState.Modified;
                db.SaveChanges();
            }
            onlineExamVM.studentExamTbl = studentExamTbl;
            onlineExamVM.studentExamResultTbl = db.StudentExamResultTbls.Where(x => x.StudentExamID == ExamConv).ToList();
            studentDashboardVM.onlineExamVM = onlineExamVM;
            return View(studentDashboardVM);
        }
        [HttpPost]
        public ActionResult Question(StudentExamResultTbl studentExamResultTbl)
        {
            long LoginID = Convert.ToInt64(FormsAuthentication.Decrypt(HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name.Split('|')[0]);
            if(LoginID != 0)
            {
                studentExamResultTbl.AttendTime = DateTime.Now;
                studentExamResultTbl.ExamQuestionTbl = new ExamQuestionTbl()
                {
                    ExamQuestionID = Convert.ToInt64(studentExamResultTbl.ExamQuestionID)
                };
                db.Entry(studentExamResultTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ExamQuestion", studentExamResultTbl);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        public List<t> GetRandomElements<t>(IEnumerable<t> list, int elementsCount)
        {
            return list.OrderBy(x => Guid.NewGuid()).Take(elementsCount).ToList();
        }

        public ActionResult ExamDashboard(ErrorVM errorVM)
        {
            long LoginID = Convert.ToInt64(FormsAuthentication.Decrypt(HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name.Split('|')[0]);
            StudentTbl studenttblobj = db.StudentTbls.Find(LoginID);
            if (errorVM.ErrorCode != null)
                ModelState.AddModelError("ErroMessage", errorVM.ErrorMsg);
            StudentDashboardVM studentDashboardVM = new StudentDashboardVM();
            studentDashboardVM.subjectTbls = db.SubjectTbls.Where(x => x.ClassID == studenttblobj.ClassID).ToList();
            return View(studentDashboardVM);
        }
    }
}