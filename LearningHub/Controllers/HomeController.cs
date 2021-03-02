using LearningHub.Helper;
using LearningHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearningHub.Controllers
{
    public class HomeController : Controller
    {
        private Entities db = new Entities();
        // GET: Home
        public ActionResult Index()
        {
            //TestingClass test = new TestingClass();
            //test.CreateCSV();
            return View();
        }
        
        public ActionResult AdminDashboard()
        {
            return View();
        }

        [Authorized(Roles = ContantValue.Parent)]
        public ActionResult ParentDashboard()
        {
            
            return View();
        }

        [Authorized(Roles = ContantValue.SuperAdmin)]
        public ActionResult SuperAdminDashboard()
        {
            return View();
        }
}
}