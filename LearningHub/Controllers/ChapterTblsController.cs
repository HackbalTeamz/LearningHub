using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LearningHub.Models;

namespace LearningHub.Controllers
{
    [Authorized(Roles = ContantValue.Admin)]
    public class ChapterTblsController : Controller
    {
        private Entities db = new Entities();

        // GET: ChapterTbls
        public ActionResult Index()
        {
            var chapterTbls = db.ChapterTbls.Include(c => c.SubjectTbl);
            return View(chapterTbls.ToList());
        }

        // GET: ChapterTbls/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChapterTbl chapterTbl = db.ChapterTbls.Find(id);
            if (chapterTbl == null)
            {
                return HttpNotFound();
            }
            return View(chapterTbl);
        }

        // GET: ChapterTbls/Create
        public ActionResult Create()
        {
            List<SubjectTbl> sublist = new List<SubjectTbl>();
            foreach (var item in db.SubjectTbls)
            {
                SubjectTbl classbj = new SubjectTbl();
                classbj.SubjectID = item.SubjectID;
                classbj.SubjectName = item.ClassTbl.DepartmentTbl.DeptName + " - " + item.ClassTbl.ClassName + " - " + item.SubjectName;
                sublist.Add(classbj);
            }
            ViewBag.SubjectID = new SelectList(sublist, "SubjectID", "SubjectName");
            return View();
        }

        // POST: ChapterTbls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChapterID,SubjectID,ChapterName")] ChapterTbl chapterTbl)
        {
            if (ModelState.IsValid)
            {
                db.ChapterTbls.Add(chapterTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SubjectID = new SelectList(db.SubjectTbls, "SubjectID", "SubjectName", chapterTbl.SubjectID);
            return View(chapterTbl);
        }

        // GET: ChapterTbls/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChapterTbl chapterTbl = db.ChapterTbls.Find(id);
            if (chapterTbl == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubjectID = new SelectList(db.SubjectTbls, "SubjectID", "SubjectName", chapterTbl.SubjectID);
            return View(chapterTbl);
        }

        // POST: ChapterTbls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChapterID,SubjectID,ChapterName")] ChapterTbl chapterTbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chapterTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SubjectID = new SelectList(db.SubjectTbls, "SubjectID", "SubjectName", chapterTbl.SubjectID);
            return View(chapterTbl);
        }

        // GET: ChapterTbls/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChapterTbl chapterTbl = db.ChapterTbls.Find(id);
            if (chapterTbl == null)
            {
                return HttpNotFound();
            }
            return View(chapterTbl);
        }

        // POST: ChapterTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ChapterTbl chapterTbl = db.ChapterTbls.Find(id);
            db.ChapterTbls.Remove(chapterTbl);
            db.SaveChanges();
            return RedirectToAction("Index");
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
