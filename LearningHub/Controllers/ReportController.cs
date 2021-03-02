using LearningHub.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LearningHub.Controllers
{
    [Authorized(Roles = ContantValue.Staff)]
    public class ReportController : BaseController
    {
        // GET: Report
        public ActionResult AttendanceReport()
        {
            return View(db.StudentsAttentanceTbls.ToList());
        }

        public ActionResult AttendanceReportGenerate()
        {
            try
            {
                long LoginID = Convert.ToInt64(FormsAuthentication.Decrypt(HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name.Split('|')[0]);
                StringBuilder sb = new StringBuilder();
                sb.Append("Student ID,Department,Class,Student Name,Student Mobile,First Login Time,Last Login Time,Logout Time,Spent Time\r\n");
                StaffTbl staffTbl = db.StaffTbls.Find(LoginID);
                List<ClassTbl> classtbllist = db.ClassTbls.Where(x => x.DeptID == staffTbl.DeptID).ToList();
                foreach (var classtblobj in classtbllist)
                {
                    foreach (var item in db.StudentsAttentanceTbls)
                    {
                        if (classtblobj.ClassID == item.StudentTbl.ClassTbl.ClassID)
                        {
                            sb.Append(item.StudentID + "," + item.StudentTbl.ClassTbl.DepartmentTbl.DeptName + "," + item.StudentTbl.ClassTbl.ClassName + "," + item.StudentTbl.StudentName + "," + item.StudentTbl.Mobile + "," + item.PresentDate + "," + item.IsLogin + "," + item.IsLogout + "," + item.LoginMinites + "\r\n");
                        }
                    }
                }

                string TodayDateForm = Convert.ToString(DateTime.Now.Date.Day) + "-" + Convert.ToString(DateTime.Now.Date.Month) + "-" + Convert.ToString(DateTime.Now.Date.Year);
                string FileNameBuilder = "ATReport-" + TodayDateForm + ".csv";
                string fullPath = Path.Combine(Server.MapPath("~/Template"), FileNameBuilder);
                System.IO.File.WriteAllText(fullPath, sb.ToString());
                byte[] fileByteArray = System.IO.File.ReadAllBytes(fullPath);
                System.IO.File.Delete(fullPath);
                return File(fileByteArray, "application/vnd.ms-excel", FileNameBuilder);
            }
            catch (Exception ex)
            {
                log.Error($"Exception in {nameof(SubjectWiseReportGenerate)}", ex);
                return RedirectToAction("AttendanceReport");
            }
        }
        public ActionResult SubjectWiseReportGenerate()
        {
            try
            {
                long LoginID = Convert.ToInt64(FormsAuthentication.Decrypt(HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name.Split('|')[0]);
                StringBuilder sb = new StringBuilder();
                sb.Append("Student ID,Department,Class,Subject,Student Name,Student Mobile,First Login Time,Last Login Time,Logout Time\r\n");
                StaffTbl staffTbl = db.StaffTbls.Find(LoginID);
                List<ClassTbl> classtbllist = db.ClassTbls.Where(x => x.DeptID == staffTbl.DeptID).ToList();
                foreach (var classtblobj in classtbllist)
                {
                    foreach (var item in db.StudentClassAttends)
                    {
                        if (classtblobj.ClassID == item.StudentTbl.ClassTbl.ClassID)
                        {
                            sb.Append(item.StudentID + "," + item.StudentTbl.ClassTbl.DepartmentTbl.DeptName + "," + item.StudentTbl.ClassTbl.ClassName + "," + item.SubjectTbl.SubjectName + "," + item.StudentTbl.StudentName + "," + item.StudentTbl.Mobile + "," + item.PresentDate + "," + item.OpenTime + "," + item.CloseTime + "\r\n");
                        }
                    }
                }
                string TodayDateForm = Convert.ToString(DateTime.Now.Date.Day) + "-" + Convert.ToString(DateTime.Now.Date.Month) + "-" + Convert.ToString(DateTime.Now.Date.Year);
                string FileNameBuilder = "SubATReport-" + TodayDateForm + ".csv";
                string fullPath = Path.Combine(Server.MapPath("~/Template"), FileNameBuilder);
                System.IO.File.WriteAllText(fullPath, sb.ToString());
                byte[] fileByteArray = System.IO.File.ReadAllBytes(fullPath);
                System.IO.File.Delete(fullPath);
                return File(fileByteArray, "application/vnd.ms-excel", FileNameBuilder);
            }
            catch(Exception ex)
            {
                log.Error($"Exception in {nameof(SubjectWiseReportGenerate)}",ex);
                return RedirectToAction("AttendanceReport");
            }
        }
    }
}