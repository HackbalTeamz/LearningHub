using LearningHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LearningHub
{
    public class Authorized : AuthorizeAttribute
    {
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Authorized));
        public string Roles { get; set; }
        public bool stat;
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            try
            {
                stat = false;
                if (FormsAuthentication.Decrypt(httpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name != null)
                {
                    string[] values = Roles.Split(',');
                    for (int i = 0; i < values.Length; i++)
                    {
                        //  values[i] = values[i].Trim();
                        string session_role = FormsAuthentication.Decrypt(httpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name.Split('|')[1];
                        if (values[i].Trim() == session_role)
                        {
                            stat = true;
                            //GlobalVariable.RoleName = FormsAuthentication.Decrypt(httpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name.Split('|')[1];
                            GlobalVariable.LoginUserID = FormsAuthentication.Decrypt(httpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name.Split('|')[0];
                        }
                    }
                }
                return stat;
            }
            catch (Exception ex)
            {
                //log.Error(ex.Message.ToString());
                return false;
            }

        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/Home/Index");
        }
    }
    public static class GlobalVariable
    {
        //public static string RoleName { get; set; }
        public static string LoginUserID { get; set; }
    }

}