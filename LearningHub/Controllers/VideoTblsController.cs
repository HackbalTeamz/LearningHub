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
    [Authorized(Roles = ContantValue.Staff)]
    public class VideoTblsController : Controller
    {
        private Entities db = new Entities();

        // GET: VideoTbls
        public ActionResult Index()
        {
            var videoTbls = db.VideoTbls.Include(v => v.ChapterTbl);
            return View(videoTbls.ToList());
        }

        // GET: VideoTbls/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoTbl videoTbl = db.VideoTbls.Find(id);
            if (videoTbl == null)
            {
                return HttpNotFound();
            }
            return View(videoTbl);
        }

        // GET: VideoTbls/Create
        public ActionResult Create()
        {
            List<ChapterTbl> chaplist = new List<ChapterTbl>();
            foreach (var item in db.ChapterTbls)
            {
                ChapterTbl classbj = new ChapterTbl();
                classbj.ChapterID = item.ChapterID;
                classbj.ChapterName = item.SubjectTbl.ClassTbl.DepartmentTbl.DeptName + " - " + item.SubjectTbl.ClassTbl.ClassName + " - " + item.SubjectTbl.SubjectName + " - " +item.ChapterName;
                chaplist.Add(classbj);
            }
            ViewBag.ChapterID = new SelectList(chaplist, "ChapterID", "ChapterName");
            return View();
        }

        // POST: VideoTbls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VideoTbl videoTbl)
        {
            if (ModelState.IsValid)
            {
                videoTbl.EnteredOn = videoTbl.UpdatedOn = DateTime.Now;
                db.VideoTbls.Add(videoTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ChapterID = new SelectList(db.ChapterTbls, "ChapterID", "ChapterName", videoTbl.ChapterID);
            return View(videoTbl);
        }

        // GET: VideoTbls/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoTbl videoTbl = db.VideoTbls.Find(id);
            if (videoTbl == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChapterID = new SelectList(db.ChapterTbls, "ChapterID", "ChapterName", videoTbl.ChapterID);
            return View(videoTbl);
        }

        // POST: VideoTbls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VideoTbl videoTbl)
        {
            if (ModelState.IsValid)
            {
                videoTbl.UpdatedOn = DateTime.Now;
                db.Entry(videoTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ChapterID = new SelectList(db.ChapterTbls, "ChapterID", "ChapterName", videoTbl.ChapterID);
            return View(videoTbl);
        }

        // GET: VideoTbls/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoTbl videoTbl = db.VideoTbls.Find(id);
            if (videoTbl == null)
            {
                return HttpNotFound();
            }
            return View(videoTbl);
        }

        // POST: VideoTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            VideoTbl videoTbl = db.VideoTbls.Find(id);
            db.VideoTbls.Remove(videoTbl);
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
