using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class RoyschedsController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: Royscheds
        public ActionResult Index()
        {
            string query = "select * from roysched";
            IEnumerable<roysched> royscheds = db.Database.SqlQuery<roysched>(query);
            return View(royscheds.ToList());
        }

        // GET: Royscheds/Details/5
        public ActionResult Details(string title_id, int lorange, int hirange, int royalty)
        {
            if (title_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //roysched roysched = db.royscheds.Find(title_id);
            //var roysched = db.royscheds.Where(r => r.title_id.Equals(title_id)).Where(r => r.royalty.Equals(royalty));
            string query = "select top 1 * from roysched where title_id = @p0 and lorange = @p1 and hirange = @p2 and royalty = @p3 ";
            roysched roysched = db.royscheds.SqlQuery(query, new string[] { title_id, lorange.ToString(), hirange.ToString(), royalty.ToString()}).SingleOrDefault();
            if (roysched == null)
            {
                return HttpNotFound();
            }
            return View(roysched);
        }

        // GET: Royscheds/Create
        public ActionResult Create()
        {
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title1");
            return View();
        }

        // POST: Royscheds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "title_id,lorange,hirange,royalty")] roysched roysched)
        {
            if (ModelState.IsValid)
            {
                db.royscheds.Add(roysched);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.title_id = new SelectList(db.titles, "title_id", "title1", roysched.title_id);
            return View(roysched);
        }

        // GET: Royscheds/Edit/5
        public ActionResult Edit(string title_id, int lorange, int hirange, int royalty)
        {
            if (title_id == null )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //roysched roysched = db.royscheds.Find(title_id);
            string query = "update roysched set title_id = @p0 and lorange = @p1 and hirange = @p2 and royalty = @p3";
            roysched roysched = db.royscheds.SqlQuery(query, new string[] { title_id, lorange.ToString(), hirange.ToString(), royalty.ToString() }).SingleOrDefault();

            if (roysched == null)
            {
                return HttpNotFound();
            }
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title1", roysched.title_id);
            return View(roysched);
        }

        // POST: Royscheds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "title_id,lorange,hirange,royalty")] roysched roysched)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roysched).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title1", roysched.title_id);
            return View(roysched);
        }

        // GET: Royscheds/Delete/5
        public ActionResult Delete(string title_id, int lorange, int hirange, int royalty)
        {
            if (title_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //roysched roysched = db.royscheds.Find(title_id);
            string query = "delete from roysched where title_id=@p0 and lorange = @p1 and hirange = @p2 and royalty = @p3";
            IEnumerable<roysched> roysched = db.Database.SqlQuery<roysched>(query,new string[] { title_id, lorange.ToString(), hirange.ToString(), royalty.ToString()});
            if (roysched == null)
            {
                return HttpNotFound();
            }
            return View(roysched);
        }

        // POST: Royscheds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string title_id, int lorange, int hirange, int royalty)
        {
            roysched roysched = db.royscheds.Find(title_id);
            db.royscheds.Remove(roysched);
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
