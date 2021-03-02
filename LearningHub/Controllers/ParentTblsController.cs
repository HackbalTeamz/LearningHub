using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LearningHub.Helper;
using LearningHub.Models;
using Newtonsoft.Json;

namespace LearningHub.Controllers
{
    [Authorized(Roles = ContantValue.Staff)]
    public class ParentTblsController : BaseController
    {

        // GET: ParentTbls
        public ActionResult Index()
        {
            var parentTbls = db.ParentTbls.Include(p => p.CredentialTbl);
            return View(parentTbls.ToList());
        }

        // GET: ParentTbls/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParentTbl parentTbl = db.ParentTbls.Find(id);
            if (parentTbl == null)
            {
                return HttpNotFound();
            }
            return View(parentTbl);
        }

        // GET: ParentTbls/Create
        public ActionResult Create()
        {
            ViewBag.CredID = new SelectList(db.CredentialTbls, "CredID", "UserName");
            return View();
        }

        // POST: ParentTbls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegistrationVM adminTbl)
        {
            if (ModelState.IsValid)
            {
                adminTbl.cred.EnteredOn = adminTbl.cred.UpdatedOn = DateTime.Now;
                adminTbl.cred.RoleID = 4;
                adminTbl.cred.Password = PasswordGenerator.RandomString(8);
                CredentialTbl crdtbl = db.CredentialTbls.Add(adminTbl.cred);
                db.SaveChanges();
                adminTbl.parent.CredID = crdtbl.CredID;
                adminTbl.parent.EnteredOn = adminTbl.parent.UpdatedOn = DateTime.Now;
                db.ParentTbls.Add(adminTbl.parent);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(adminTbl);
        }

        // GET: ParentTbls/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParentTbl parentTbl = db.ParentTbls.Find(id);
            if (parentTbl == null)
            {
                return HttpNotFound();
            }
            CredentialTbl credtbl = db.CredentialTbls.Find(parentTbl.CredID);
            RegistrationVM regvm = new RegistrationVM()
            {
                parent = parentTbl,
                cred = credtbl
            };
            return View(regvm);
        }

        // POST: ParentTbls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RegistrationVM adminTbl)
        {
            if (ModelState.IsValid)
            {
                ParentTbl admin = db.ParentTbls.Find(adminTbl.parent.ParentID);
                if(admin!=null)
                {
                    admin.ParentName = adminTbl.parent.ParentName;
                    admin.Mobile = adminTbl.parent.Mobile;
                    admin.UpdatedOn = DateTime.Now;
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
                    log.Error($"Exception in {nameof(Edit)} , ParentTbls is Null");
                }
            }
            return View(adminTbl);
        }

        // GET: ParentTbls/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParentTbl parentTbl = db.ParentTbls.Find(id);
            if (parentTbl == null)
            {
                return HttpNotFound();
            }
            return View(parentTbl);
        }

        // POST: ParentTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ParentTbl parentTbl = db.ParentTbls.Find(id);
            try
            {
                db.ParentTbls.Remove(parentTbl);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Cannot Delete Details";
                log.Error($"Exception in {nameof(DeleteConfirmed)} , Params {JsonConvert.SerializeObject(parentTbl)}", ex);
                return View("Delete", parentTbl);
            }
           
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
