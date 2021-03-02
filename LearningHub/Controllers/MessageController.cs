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
    [Authorized(Roles = ContantValue.Staff +","+ ContantValue.Admin)]
    public class MessageController : BaseController
    {
        // GET: Message
        public ActionResult ClassWiseMessage()
        {
            List<ClassTbl> classlist = new List<ClassTbl>();
            long LoginID = Convert.ToInt64(FormsAuthentication.Decrypt(HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name.Split('|')[0]);
            List<StaffAssignClassTbl> staffAssignClassTbl = db.StaffAssignClassTbls.Where(x => x.StaffID == LoginID).ToList();
            foreach (var item in staffAssignClassTbl)
            {
                ClassTbl classbj = new ClassTbl();
                classbj.ClassID = Convert.ToInt32(item.ClassID);
                classbj.ClassName = item.ClassTbl.DepartmentTbl.DeptName + " - " + item.ClassTbl.ClassName;
                classlist.Add(classbj);
            }
            ViewBag.ClassID = new SelectList(classlist, "ClassID", "ClassName");
            ViewBag.MsgID = new SelectList(db.MessageTbls, "MsgID", "MessageText");
            return View();
        }
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> ClassWiseMessage(MessageSendVM messageSendVM)
        {
            SMSSendHelper smshelper = new SMSSendHelper();
            await smshelper.SMSSendAsync(messageSendVM.ClassID, messageSendVM.MsgID);
            List<ClassTbl> classlist = new List<ClassTbl>();
            foreach (var item in db.ClassTbls)
            {
                ClassTbl classbj = new ClassTbl();
                classbj.ClassID = item.ClassID;
                classbj.ClassName = item.DepartmentTbl.DeptName + " - " + item.ClassName;
                classlist.Add(classbj);
            }
            ViewBag.ClassID = new SelectList(classlist, "ClassID", "ClassName");
            ViewBag.MsgID = new SelectList(db.MessageTbls, "MsgID", "MessageText");
            return View();
        }
    }
}