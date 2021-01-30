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
    public class PublishersController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: Publishers
        public ActionResult Index()
        {
            var publishers = db.publishers.Include(p => p.pub_info);
            return View(publishers.ToList());
        }

        //GET: Publishers/Pub_info/Logo/{id}
        public ActionResult Logo(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            publisher publisher = db.publishers.Find(id);
            if (publisher == null)
            {
                return HttpNotFound();
            }
            return File(publisher.pub_info.logo, "image/png");
        }

      


        // GET: Publishers/Details/5
        public ActionResult Details(string pub_id)
        {
            if (pub_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            publisher publisher = db.publishers.Find(pub_id);
            if (publisher == null)
            {
                return HttpNotFound();
            }
            return View(publisher);
        }

        // GET: Publishers/Create
        public ActionResult Create()
        {
            ViewBag.pub_id = new SelectList(db.pub_info, "pub_id", "pr_info");
            return View();
        }

        // POST: Publishers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pub_id,pub_name,city,state,country")] publisher publisher)
        {
            if(publisher.pub_id==null && publisher.pub_name == null)
            {
                ViewBag.Message = "ID and name fields are required!";
            }          
            if (ModelState.IsValid)
            {
                db.publishers.Add(publisher);
                db.SaveChanges();
                ViewBag.Message = "Publisher added successfully!";
                //return RedirectToAction("Index");
            }

            ViewBag.pub_id = new SelectList(db.pub_info, "pub_id", "pr_info", publisher.pub_id);
            return View(publisher);
        }

        // GET: Publishers/Edit/5
        public ActionResult Edit(string pub_id)
        {
            if (pub_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            publisher publisher = db.publishers.Find(pub_id);
            if (publisher == null)
            {
                return HttpNotFound();
            }
            ViewBag.pub_id = new SelectList(db.pub_info, "pub_id", "pr_info", publisher.pub_id);
            return View(publisher);
        }

        

        // POST: Publishers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pub_id,pub_name,city,state,country")] publisher publisher)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(publisher).State = EntityState.Modified; 
                db.SaveChanges();
                ViewBag.Message = "Publisher's information edited successfully!";
                //return RedirectToAction("Index");
            }
            ViewBag.pub_id = new SelectList(db.pub_info, "pub_id", "pr_info", publisher.pub_id);
            return View(publisher);
        }

        // GET: Publishers/Delete/5
        public ActionResult Delete(string pub_id)
        {
            if (pub_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            publisher publisher = db.publishers.Find(pub_id);
            if (publisher == null)
            {
                return HttpNotFound();
            }
            return View(publisher);
        }

        // POST: Publishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string pub_id)
        {
            try
            {
                publisher publisher = db.publishers.Find(pub_id);
                db.publishers.Remove(publisher);
                db.SaveChanges();
                ViewBag.Message = "Publisher deleted successfully!";
            }catch(DbUpdateException e)     //db conflict 
            {
                ViewBag.Message = "Deletion of this publisher is not available due to system restrictions.";
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
