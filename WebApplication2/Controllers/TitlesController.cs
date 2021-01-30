using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class TitlesController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: Titles
        public ActionResult Index()
        {
            var titles = db.titles;
            return View(titles.ToList());
        }

        // GET: Titles/Details/5
        public ActionResult Details(string title_id)
        {
            if (title_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            title title = db.titles.Find(title_id);
            if (title == null)
            {
                return HttpNotFound();
            }
            return View(title);
        }

        // GET: Titles/Create
        public ActionResult Create()
        {
            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name");
            ViewBag.title_id = new SelectList(db.royscheds, "title_id", "title_id");
            ViewBag.title_id = putBookNumber();
            var query = db.titles.Select(x => x.type).Distinct();
            ViewBag.type = new SelectList(query);
            return View();
        }

        // POST: Titles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "title_id,title1,type,pub_id,price,advance,royalty,ytd_sales,notes,pubdate")] title title)
        {
            if (title.title1 != null && title.type != null && title.pubdate != null)
            {
                if (ModelState.IsValid)
                {
                    db.titles.Add(title);
                    db.SaveChanges();
                    ViewBag.Message = "Book added successfully!";
                }
            }
            else ViewBag.Message = "Please fill in all the required fields!";

            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name", title.pub_id);
            var query = db.titles.Select(x => x.type).Distinct();
            ViewBag.type = new SelectList(query);
            //ViewBag.title_id = new SelectList(db.royscheds, "title_id", "title_id", title.title_id);
            return View(title);
        }

        // GET: Titles/Edit/5
        public ActionResult Edit(string title_id)
        {
           
            if (title_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            title title = db.titles.Find(title_id);
            if (title == null)
            {
                return HttpNotFound();
            }
            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name", title.pub_id);
            var query = db.titles.Select(x => x.type).Distinct();
            ViewBag.type = new SelectList(query);
            ViewBag.pubdate = title.pubdate;
            return View(title);
        }

        // POST: Titles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "title_id,title1,type,pub_id,price,advance,royalty,ytd_sales,notes,pubdate")] title title)
        {
            if (ModelState.IsValid)
            {
                db.Entry(title).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name", title.pub_id);
            var query = db.titles.Select(x => x.type).Distinct();
            ViewBag.type = new SelectList(query);
            return View(title);
        }
        public string putBookNumber()
        {
            var books = db.titles.Select(x => x.title_id).ToList();
            string newBook = generateID();
            while (books.Contains(newBook))
                newBook = generateID();
            return newBook;
        }

        public string generateID()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // GET: Titles/Delete/5
        public ActionResult Delete(string title_id)
        {
            if (title_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            title title = db.titles.Find(title_id);
            if (title == null)
            {
                return HttpNotFound();
            }
            return View(title);
        }

        // POST: Titles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string title_id)
        {
            try
            {
                if (title_id != null)
                {
                    title title = db.titles.Find(title_id);
                    db.titles.Remove(title);
                    db.SaveChanges();
                    ViewBag.Message = "Book deleted successfully!";
                }
                
            }catch(DbUpdateException e)
            {
                ViewBag.Message = "Deletion of this book is not available due to system restrictions.";
            }

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
