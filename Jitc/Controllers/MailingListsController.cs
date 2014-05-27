using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Jitc.Models;

namespace Jitc.Controllers
{
    public class MailingListsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MailingLists
        public ActionResult Index()
        {
            var mailingLists = db.MailingLists.Include(m => m.Recipient);
            return View(mailingLists.ToList());
        }

        // GET: MailingLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MailingList mailingList = db.MailingLists.Find(id);
            if (mailingList == null)
            {
                return HttpNotFound();
            }
            return View(mailingList);
        }

        // GET: MailingLists/Create
        public ActionResult Create()
        {
            ViewBag.MailRecipientId = new SelectList(db.MailRecipients, "MailRecipientId", "LastName");
            return View();
        }

        // POST: MailingLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MailingListId,MailingName,MailRecipientId")] MailingList mailingList)
        {
            if (ModelState.IsValid)
            {
                db.MailingLists.Add(mailingList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MailRecipientId = new SelectList(db.MailRecipients, "MailRecipientId", "LastName", mailingList.MailRecipientId);
            return View(mailingList);
        }

        // GET: MailingLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MailingList mailingList = db.MailingLists.Find(id);
            if (mailingList == null)
            {
                return HttpNotFound();
            }
            ViewBag.MailRecipientId = new SelectList(db.MailRecipients, "MailRecipientId", "LastName", mailingList.MailRecipientId);
            return View(mailingList);
        }

        // POST: MailingLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MailingListId,MailingName,MailRecipientId")] MailingList mailingList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mailingList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MailRecipientId = new SelectList(db.MailRecipients, "MailRecipientId", "LastName", mailingList.MailRecipientId);
            return View(mailingList);
        }

        // GET: MailingLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MailingList mailingList = db.MailingLists.Find(id);
            if (mailingList == null)
            {
                return HttpNotFound();
            }
            return View(mailingList);
        }

        // POST: MailingLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MailingList mailingList = db.MailingLists.Find(id);
            db.MailingLists.Remove(mailingList);
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
