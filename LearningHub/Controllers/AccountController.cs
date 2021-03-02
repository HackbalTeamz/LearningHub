using LearningHub.Helper;
using LearningHub.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LearningHub.Controllers
{
    public class AccountController : BaseController
    {
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(CredentialTbl credentialDetail)
        {
            try
            {
                CredentialTbl creddbobj = db.CredentialTbls.Where(x => x.UserName == credentialDetail.UserName && x.Password == credentialDetail.Password).FirstOrDefault();
                if (creddbobj != null)
                {
                    if (creddbobj.RoleID == Convert.ToInt32(ContantValue.Student))
                    {
                        StudentTbl adminDetail = db.StudentTbls.Where(x => x.CredID == creddbobj.CredID).FirstOrDefault();
                        if (adminDetail != null)
                        {
                            FormsAuthentication.SetAuthCookie(Convert.ToString(adminDetail.StudentID + "|" + ContantValue.Student), false);
                            //GlobalVariable.RoleName = ContantValue.Student;
                            new AttendanceHelper().StudentIsLogin(adminDetail.StudentID);
                            return RedirectToAction("Dashboard", "Student");
                        }
                        else
                        {
                            return View(credentialDetail);
                        }

                    }
                    else if (creddbobj.RoleID == Convert.ToInt64(ContantValue.Staff))
                    {
                        StaffTbl adminDetail = db.StaffTbls.Where(x => x.CredID == creddbobj.CredID).FirstOrDefault();
                        FormsAuthentication.SetAuthCookie(Convert.ToString(adminDetail.StaffID + "|" + ContantValue.Staff), false);
                        //GlobalVariable.RoleName = ContantValue.Staff;
                        return RedirectToAction("Dashboard", "Staff");

                    }
                    else if (creddbobj.RoleID == Convert.ToInt64(ContantValue.Admin))
                    {
                        AdminTbl adminDetail = db.AdminTbls.Where(x => x.CredID == creddbobj.CredID).FirstOrDefault();
                        FormsAuthentication.SetAuthCookie(Convert.ToString(adminDetail.AdminID + "|" + ContantValue.Admin), false);
                        //GlobalVariable.RoleName = ContantValue.Admin;
                        return RedirectToAction("Dashboard", "Admin");
                    }
                    else if (creddbobj.RoleID == Convert.ToInt64(ContantValue.Parent))
                    {
                        ParentTbl adminDetail = db.ParentTbls.Where(x => x.CredID == creddbobj.CredID).FirstOrDefault();
                        FormsAuthentication.SetAuthCookie(Convert.ToString(adminDetail.ParentID + "|" + ContantValue.Parent), false);
                        //GlobalVariable.RoleName = ContantValue.Parent;
                        return RedirectToAction("ParentDashboard", "Home");

                    }
                    else
                    {
                        ViewBag.Error ="No User Found";
                        return View(credentialDetail);
                    }

                }
                else
                {
                    ViewBag.Error = "Username Or Password is Invalid";
                    return View(credentialDetail);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Please Contact Admin";
                log.Error($"Exception in {nameof(Login)} , Params {JsonConvert.SerializeObject(credentialDetail)}", ex);
                return View(credentialDetail);
            }

        }
        public ActionResult SuperLogin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuperLogin(CredentialTbl credentialDetail)
        {
            long RoleID = Convert.ToInt64(ContantValue.SuperAdmin);
            CredentialTbl creddbobj = db.CredentialTbls.Where(x => x.UserName == credentialDetail.UserName && x.Password == credentialDetail.Password && x.RoleID == RoleID).FirstOrDefault();
            if (creddbobj != null)
            {
                SuperAdminTbl superadminDetail = db.SuperAdminTbls.Where(x => x.CredID == creddbobj.CredID).FirstOrDefault();
                FormsAuthentication.SetAuthCookie(Convert.ToString(superadminDetail.SuperAdminID + "|" + ContantValue.SuperAdmin), false);
                //GlobalVariable.RoleName = ContantValue.SuperAdmin;
                return RedirectToAction("Dashboard", "SuperAdmin");
            }
            else
            {
                ModelState.AddModelError("Invalid", "Username Or Password Invalid");
                return View(credentialDetail);
            }
        }
        public ActionResult Logout()
        {
            string RoleName = FormsAuthentication.Decrypt(HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name.Split('|')[1];
            if (RoleName == ContantValue.Student)
            {
                long LoginID = Convert.ToInt64(FormsAuthentication.Decrypt(HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name.Split('|')[0]);
                new AttendanceHelper().StudentIsLogout(LoginID);
            }
            else if (RoleName == ContantValue.Staff)
            {
                long LoginID = Convert.ToInt64(FormsAuthentication.Decrypt(HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name.Split('|')[0]);
                new AttendanceHelper().StaffIsLogout(LoginID);
            }
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }
    }
}