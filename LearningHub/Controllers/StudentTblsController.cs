using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LearningHub.Helper;
using LearningHub.Models;
using LumenWorks.Framework.IO.Csv;
using Newtonsoft.Json;

namespace LearningHub.Controllers
{
    [Authorized(Roles = ContantValue.Staff + "," + ContantValue.Admin)]
    public class StudentTblsController : BaseController
    {

        // GET: StudentTbls
        public ActionResult Index()
        {
            string RoleName = FormsAuthentication.Decrypt(HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name.Split('|')[1];
            if (RoleName == ContantValue.Admin)
            {
                var studentTbls = db.StudentTbls.Include(s => s.ClassTbl).Include(s => s.CredentialTbl).Include(s => s.ParentTbl);
                return View(studentTbls.ToList());
            }
            else
            {
                long LoginID = Convert.ToInt64(FormsAuthentication.Decrypt(HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name.Split('|')[0]);
                List<StaffAssignClassTbl> staffAssignClassTbl = db.StaffAssignClassTbls.Where(x => x.StaffID == LoginID).ToList();
                List<StudentTbl> studlist = new List<StudentTbl>();
                foreach(var item in staffAssignClassTbl)
                {
                    studlist.AddRange(db.StudentTbls.Include(s => s.ClassTbl).Include(s => s.CredentialTbl).Include(s => s.ParentTbl).Where(x => x.ClassTbl.ClassID == item.ClassID));
                }
                return View(studlist);
            }
            
            
        }

        // GET: StudentTbls/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentTbl studentTbl = db.StudentTbls.Find(id);
            if (studentTbl == null)
            {
                return HttpNotFound();
            }
            return View(studentTbl);
        }

        // GET: StudentTbls/Create
        public ActionResult Create()
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
            List<ParentTbl> parentlist = new List<ParentTbl>();
            foreach (var item in db.ParentTbls)
            {
                ParentTbl parentbj = new ParentTbl();
                parentbj.ParentID = item.ParentID;
                parentbj.ParentName = item.ParentName+ " - " + item.Mobile;
                parentlist.Add(parentbj);
            }
            ViewBag.ParentID = new SelectList(parentlist, "ParentID", "ParentName");
            return View();
        }

        // POST: StudentTbls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegistrationVM adminTbl)
        {
            if (ModelState.IsValid)
            {
                adminTbl.cred.EnteredOn = adminTbl.cred.UpdatedOn = DateTime.Now;
                adminTbl.cred.RoleID = 5;
                adminTbl.cred.Password = PasswordGenerator.RandomString(8);
                CredentialTbl crdtbl = db.CredentialTbls.Add(adminTbl.cred);
                db.SaveChanges();
                adminTbl.student.CredID = crdtbl.CredID;
                db.StudentTbls.Add(adminTbl.student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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
            ViewBag.ParentID = new SelectList(db.ParentTbls, "ParentID", "ParentName", adminTbl.student.ParentID);
            return View(adminTbl);
        }

        // GET: StudentTbls/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentTbl studentTbl = db.StudentTbls.Find(id);
            if (studentTbl == null)
            {
                return HttpNotFound();
            }
            CredentialTbl credtbl = db.CredentialTbls.Find(studentTbl.CredID);
            RegistrationVM regvm = new RegistrationVM()
            {
                student = studentTbl,
                cred = credtbl
            };
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
            ViewBag.ParentID = new SelectList(db.ParentTbls, "ParentID", "ParentName", regvm.student.ParentID);
            return View(regvm);
        }

        // POST: StudentTbls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RegistrationVM adminTbl)
        {
            if (ModelState.IsValid)
            {
                StudentTbl admin = db.StudentTbls.Find(adminTbl.student.StudentID);
                if(admin!=null)
                {
                    admin.StudentName = adminTbl.student.StudentName;
                    admin.Mobile = adminTbl.student.Mobile;
                    db.Entry(admin).State = EntityState.Modified;
                    db.SaveChanges();
                    CredentialTbl cred = db.CredentialTbls.Find(admin.CredID);
                    cred.UserName = adminTbl.cred.UserName;
                    cred.UpdatedOn = DateTime.Now;
                    db.Entry(cred).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Some Details are Missing Please Contact Admin";
                    log.Error($"Exception in {nameof(Edit)} , StudentTbls is Null");
                }
                
            }
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
            ViewBag.ParentID = new SelectList(db.ParentTbls, "ParentID", "ParentName", adminTbl.student.ParentID);
            return View(adminTbl);
        }

        // GET: StudentTbls/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentTbl studentTbl = db.StudentTbls.Find(id);
            if (studentTbl == null)
            {
                return HttpNotFound();
            }
            return View(studentTbl);
        }

        // POST: StudentTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            StudentTbl studentTbl = db.StudentTbls.Find(id);
            try
            {
                db.StudentTbls.Remove(studentTbl);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Cannot Delete Details";
                log.Error($"Exception in {nameof(DeleteConfirmed)} , Params {JsonConvert.SerializeObject(studentTbl)}", ex);
                return View("Delete", studentTbl);
            }
            return RedirectToAction("Index");
        }

        public ActionResult ClassWiseTemplate()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Parent Email,Parent Name,Parent Mobile,Student Email,Student Name,Student Mobile\r\n");
            string fullPath = Path.Combine(Server.MapPath("~/Template"), "StudentsUploadHL.csv");
            System.IO.File.WriteAllText(fullPath, sb.ToString());
            byte[] fileByteArray = System.IO.File.ReadAllBytes(fullPath);
            System.IO.File.Delete(fullPath);
            return File(fileByteArray, "application/vnd.ms-excel", "StudentsUploadHL.csv");
        }

        public ActionResult StudentsUpload()
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StudentsUpload(RegistrationVM RegVM, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                //List<SampleModel> samlist = new List<SampleModel>();
                if (upload != null && upload.ContentLength > 0)
                {

                    if (upload.FileName.EndsWith(".csv"))
                    {
                        Stream stream = upload.InputStream;
                        DataTable csvTable = new DataTable();
                        using (CsvReader csvReader =
                            new CsvReader(new StreamReader(stream), true))
                        {
                            csvTable.Load(csvReader);
                        }
                        foreach (DataColumn col in csvTable.Columns)
                        {
                            _ = col.ColumnName;
                        }
                        foreach (DataRow row in csvTable.Rows)
                        {

                            CredentialTbl cred = null;
                            ParentTbl parent = null;
                            StudentTbl stud = null;
                            foreach (DataColumn col in csvTable.Columns)
                            {

                                if (col.ColumnName == "Parent Email")
                                {
                                    string Username = row[col.ColumnName].ToString();
                                    cred = db.CredentialTbls.Where(x => x.UserName == Username).FirstOrDefault();
                                    if(cred == null)
                                    {
                                        cred = new CredentialTbl();
                                        cred.RoleID = 4;
                                        cred.UserName = row[col.ColumnName].ToString();
                                        cred.Password = PasswordGenerator.RandomString(8);
                                        cred.EnteredOn = cred.UpdatedOn = DateTime.Now;
                                        cred = db.CredentialTbls.Add(cred);
                                        db.SaveChanges();
                                    }
                                    
                                }
                                if (col.ColumnName == "Parent Name")
                                {
                                    parent = db.ParentTbls.Where(x => x.CredID == cred.CredID).FirstOrDefault();
                                    if (parent == null)
                                    {
                                        parent = new ParentTbl();
                                        parent.CredID = cred.CredID;
                                        parent.ParentName = row[col.ColumnName].ToString();
                                    }
                                }
                                if (col.ColumnName == "Parent Mobile")
                                {
                                    if (parent.ParentID == 0)
                                    {
                                        parent.Mobile = row[col.ColumnName].ToString();
                                        parent.UpdatedOn = parent.EnteredOn = DateTime.Now;
                                        parent = db.ParentTbls.Add(parent);
                                        db.SaveChanges();
                                    }
                                    else
                                    {
                                        parent = db.ParentTbls.Where(x => x.CredID == cred.CredID).FirstOrDefault();
                                    }
                                }
                                if (col.ColumnName == "Student Email")
                                {
                                    cred = new CredentialTbl();
                                    cred.RoleID = 5;
                                    cred.UserName = row[col.ColumnName].ToString();
                                    cred.Password = PasswordGenerator.RandomString(8);
                                    cred.EnteredOn = cred.UpdatedOn = DateTime.Now;
                                    cred = db.CredentialTbls.Add(cred);
                                    db.SaveChanges();
                                }
                                if (col.ColumnName == "Student Name")
                                {
                                    stud = new StudentTbl();
                                    stud.StudentName = row[col.ColumnName].ToString();
                                    stud.CredID = cred.CredID;
                                    stud.ParentID = parent.ParentID;
                                    stud.ClassID = RegVM.student.ClassID;
                                }
                                if (col.ColumnName == "Student Mobile")
                                {
                                    stud.Mobile = row[col.ColumnName].ToString();
                                    stud = db.StudentTbls.Add(stud);
                                    db.SaveChanges();
                                }

                            }

                        }
                    }
                    else
                    {
                        ModelState.AddModelError("File", "This file format is not supported");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
            }
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
            return View();
        }
        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
