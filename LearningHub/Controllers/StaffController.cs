using LearningHub.Helper;
using LearningHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LearningHub.Controllers
{
    [Authorized(Roles = ContantValue.Staff)]
    public class StaffController : BaseController
    {
        // GET: Staff
        public ActionResult Dashboard()
        {
            long LoginID = Convert.ToInt64(FormsAuthentication.Decrypt(HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name.Split('|')[0]);
            using (var context = new Entities())
            {
                ViewBag.IsHead = context.StaffTbls.Find(LoginID).IsHead;
            }
            new AttendanceHelper().StaffIsLogin(LoginID);
            return View();
        }
    }
}