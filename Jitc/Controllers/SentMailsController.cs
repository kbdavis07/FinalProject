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
    public class SentMailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SentMails
        public ActionResult Index()
        {
            var sentMails = db.SentMails.Include(s => s.Recipient);
            return View(sentMails.ToList());
        }

        // GET: SentMails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SentMail sentMail = db.SentMails.Find(id);
            if (sentMail == null)
            {
                return HttpNotFound();
            }
            return View(sentMail);
        }

        // GET: SentMails/Create
        public ActionResult Create()
        {
            ViewBag.MailRecipientId = new SelectList(db.MailRecipients, "MailRecipientId", "LastName");
            return View();
        }

        // POST: SentMails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MailId,MailRecipientId,SentToMail,SentFromMail,Status,SentDate")] SentMail sentMail)
        {
            if (ModelState.IsValid)
            {
                db.SentMails.Add(sentMail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MailRecipientId = new SelectList(db.MailRecipients, "MailRecipientId", "LastName", sentMail.MailRecipientId);
            return View(sentMail);
        }

        // GET: SentMails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SentMail sentMail = db.SentMails.Find(id);
            if (sentMail == null)
            {
                return HttpNotFound();
            }
            ViewBag.MailRecipientId = new SelectList(db.MailRecipients, "MailRecipientId", "LastName", sentMail.MailRecipientId);
            return View(sentMail);
        }

        // POST: SentMails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MailId,MailRecipientId,SentToMail,SentFromMail,Status,SentDate")] SentMail sentMail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sentMail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MailRecipientId = new SelectList(db.MailRecipients, "MailRecipientId", "LastName", sentMail.MailRecipientId);
            return View(sentMail);
        }

        // GET: SentMails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SentMail sentMail = db.SentMails.Find(id);
            if (sentMail == null)
            {
                return HttpNotFound();
            }
            return View(sentMail);
        }

        // POST: SentMails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SentMail sentMail = db.SentMails.Find(id);
            db.SentMails.Remove(sentMail);
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
