using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LearningHub.Models;
using LumenWorks.Framework.IO.Csv;

namespace LearningHub.Controllers
{
    public class ExamQuestionTblsController : Controller
    {
        private Entities db = new Entities();

        // GET: ExamQuestionTbls
        public ActionResult Index()
        {
            var examQuestionTbls = db.ExamQuestionTbls.Include(e => e.ExamTbl);
            return View(examQuestionTbls.ToList());
        }

        // GET: ExamQuestionTbls/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamQuestionTbl examQuestionTbl = db.ExamQuestionTbls.Find(id);
            if (examQuestionTbl == null)
            {
                return HttpNotFound();
            }
            return View(examQuestionTbl);
        }

        // GET: ExamQuestionTbls/Create
        public ActionResult Create()
        {
            ViewBag.ExamID = new SelectList(db.ExamTbls, "ExamID", "ExamName");
            return View();
        }

        // POST: ExamQuestionTbls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExamQuestionID,ExamID,Question,OptionA,OptionB,OptionC,OptionD,Answer")] ExamQuestionTbl examQuestionTbl)
        {
            if (ModelState.IsValid)
            {
                db.ExamQuestionTbls.Add(examQuestionTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ExamID = new SelectList(db.ExamTbls, "ExamID", "ExamName", examQuestionTbl.ExamID);
            return View(examQuestionTbl);
        }

        // GET: ExamQuestionTbls/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamQuestionTbl examQuestionTbl = db.ExamQuestionTbls.Find(id);
            if (examQuestionTbl == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExamID = new SelectList(db.ExamTbls, "ExamID", "ExamName", examQuestionTbl.ExamID);
            return View(examQuestionTbl);
        }

        // POST: ExamQuestionTbls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExamQuestionID,ExamID,Question,OptionA,OptionB,OptionC,OptionD,Answer")] ExamQuestionTbl examQuestionTbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(examQuestionTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExamID = new SelectList(db.ExamTbls, "ExamID", "ExamName", examQuestionTbl.ExamID);
            return View(examQuestionTbl);
        }

        // GET: ExamQuestionTbls/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamQuestionTbl examQuestionTbl = db.ExamQuestionTbls.Find(id);
            if (examQuestionTbl == null)
            {
                return HttpNotFound();
            }
            return View(examQuestionTbl);
        }

        // POST: ExamQuestionTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ExamQuestionTbl examQuestionTbl = db.ExamQuestionTbls.Find(id);
            db.ExamQuestionTbls.Remove(examQuestionTbl);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ExamQuestionUpload()
        {
            List<ExamTbl> examTblslist = new List<ExamTbl>();
            foreach (var item in db.ExamTbls)
            {
                ExamTbl examTblsobj = new ExamTbl();
                examTblsobj.ExamID = item.ExamID;
                examTblsobj.ExamName = item.SubjectTbl.SubjectName + " - " + item.ExamName;
                examTblslist.Add(examTblsobj);
            }
            ViewBag.ExamID = new SelectList(examTblslist, "ExamID", "ExamName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExamQuestionUpload(ExamQuestionTbl examQuestionTbl, HttpPostedFileBase upload)
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
                            ExamQuestionTbl examQuestionObj = null;
                            foreach (DataColumn col in csvTable.Columns)
                            {

                                if (col.ColumnName == "Question")
                                {
                                    examQuestionObj = new ExamQuestionTbl();
                                    examQuestionObj.ExamID = examQuestionTbl.ExamID;
                                    examQuestionObj.Question = row[col.ColumnName].ToString();

                                }
                                if (col.ColumnName == "OptionA")
                                {
                                    examQuestionObj.OptionA = row[col.ColumnName].ToString();
                                }
                                if (col.ColumnName == "OptionB")
                                {
                                    examQuestionObj.OptionB = row[col.ColumnName].ToString();
                                }
                                if (col.ColumnName == "OptionC")
                                {
                                    examQuestionObj.OptionC = row[col.ColumnName].ToString();
                                }
                                if (col.ColumnName == "OptionD")
                                {
                                    examQuestionObj.OptionD = row[col.ColumnName].ToString();
                                }
                                if (col.ColumnName == "Answer")
                                {
                                    examQuestionObj.Answer = row[col.ColumnName].ToString();
                                    db.ExamQuestionTbls.Add(examQuestionObj);
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
            ViewBag.ExamID = new SelectList(db.ExamTbls, "ExamID", "ExamID");
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
