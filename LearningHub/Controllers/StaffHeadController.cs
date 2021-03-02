using LearningHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LearningHub.Controllers
{
    [Authorized(Roles = ContantValue.Staff)]
    public class StaffHeadController : Controller
    {
        Entities context = new Entities();
        // GET: StaffHead
        public ActionResult StaffView()
        {
            long LoginID = Convert.ToInt64(FormsAuthentication.Decrypt(HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name.Split('|')[0]);
            StaffTbl staffTbl = context.StaffTbls.Find(LoginID);
            if (Convert.ToBoolean(staffTbl.IsHead))
            {
                var staffTbls = context.StaffTbls.Where(x => x.StaffID == staffTbl.DeptID);
                return View(staffTbls.ToList());
            }
            else
            {
                return RedirectToAction("Dashboard", "Staff");
            }

        }
    }
}