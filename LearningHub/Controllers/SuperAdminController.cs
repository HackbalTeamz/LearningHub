using LearningHub.Helper;
using LearningHub.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace LearningHub.Controllers
{
    public class SuperAdminController : Controller
    {
        [Authorized(Roles = ContantValue.SuperAdmin)]
        // GET: SuperAdmin
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult PasswordReset(string UserRole)
        {
            if (UserRole == ContantValue.Student)
            {
                using (var context = new Entities())
                {
                    List<CredentialTbl> credtbl = context.CredentialTbls.Where(x => x.RoleID == 5).ToList();
                    foreach(var item in credtbl)
                    {
                        item.Password = PasswordGenerator.RandomString(8);
                        context.Entry(item).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            else if (UserRole == ContantValue.Staff)
            {
                using (var context = new Entities())
                {
                    List<CredentialTbl> credtbl = context.CredentialTbls.Where(x => x.RoleID == 4).ToList();
                    foreach (var item in credtbl)
                    {
                        item.Password = PasswordGenerator.RandomString(8);
                        context.Entry(item).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Dashboard","SuperAdmin");
        }
        public ActionResult PasswordMail(string UserRole)
        {
            if (UserRole == ContantValue.Student)
            {
                using (var context = new Entities())
                {
                    List<CredentialTbl> credtbl = context.CredentialTbls.Where(x => x.RoleID == 5).ToList();
                    foreach (var item in credtbl)
                    {
                        foreach(var item2 in context.StudentTbls.Where(x=>x.CredID == item.CredID).ToList())
                        {
                            if(item2.ClassID == 4)
                            {
                                try
                                {
                                    MailMessage message = new MailMessage();
                                    SmtpClient smtp = new SmtpClient();
                                    message.From = new MailAddress("hackbalbusiness@gmail.com");
                                    message.To.Add(new MailAddress(item.UserName));
                                    message.Subject = "Learning Hub Credential";
                                    message.IsBodyHtml = true; //to make message body as html  
                                    message.Body = getHtml(context.StudentTbls.Where(x => x.CredID == item.CredID).FirstOrDefault());
                                    smtp.Port = 587;
                                    smtp.Host = "smtp.gmail.com"; //for gmail host  
                                    smtp.EnableSsl = true;
                                    smtp.UseDefaultCredentials = true;
                                    smtp.Credentials = new NetworkCredential("hackbalbusiness@gmail.com", "9539391527");
                                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                    smtp.Send(message);
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                            
                        }
                        
                    }
                }
            }
            else if (UserRole == ContantValue.Staff)
            {
                using (var context = new Entities())
                {
                    List<CredentialTbl> credtbl = context.CredentialTbls.Where(x => x.RoleID == 4).ToList();
                    foreach (var item in credtbl)
                    {
                        item.Password = PasswordGenerator.RandomString(8);
                        context.Entry(item).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Dashboard", "SuperAdmin");
        }
        public static string getHtml(StudentTbl student)
        {
            try
            {
                string messageBody = "<font>The following are the records: </font><br><br> Please Login https://chmm.hackballearning.in Using Following Details <br><br>";
                string htmlTableStart = "<table style=\"border-collapse:collapse; text-align:center;\" >";
                string htmlTableEnd = "</table>";
                string htmlHeaderRowStart = "<tr style=\"background-color:#6FA1D2; color:#ffffff;\">";
                string htmlHeaderRowEnd = "</tr>";
                string htmlTrStart = "<tr style=\"color:#555555;\">";
                string htmlTrEnd = "</tr>";
                string htmlTdStart = "<td style=\" border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\">";
                string htmlTdEnd = "</td>";
                messageBody += htmlTableStart;
                messageBody += htmlHeaderRowStart;
                messageBody += htmlTdStart + "Student Name:- " + student.StudentName + htmlTdEnd;
                messageBody += htmlTdStart + "Email/UserName:- " + student.CredentialTbl.UserName + htmlTdEnd;
                messageBody += htmlTdStart + "Password:- " + student.CredentialTbl.Password + htmlTdEnd;
                messageBody += htmlHeaderRowEnd;
                messageBody = messageBody + htmlTableEnd;
                return messageBody; // return HTML Table as string from this function  
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
    
}