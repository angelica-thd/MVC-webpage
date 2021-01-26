using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class AuthorsController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: Authors
        public ActionResult Index()
        {
            return View(db.authors.ToList());
        }

        // GET: Authors/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            author author = db.authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }
        
        // GET: Authors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "au_id,au_lname,au_fname,phone,address,city,state,zip,contract")] author author)
        {
            if (author.zip == null) author.zip ="00000";
            if (author.au_id != null && author.phone != null )
            {
                StringBuilder au_id_format = new StringBuilder();
                StringBuilder phone_format = new StringBuilder();
                au_id_format.Append(author.au_id.Substring(0, 3)).Append("-").
               Append(author.au_id.Substring(3, 2)).Append("-").
               Append(author.au_id.Substring(5, 4));
                author.au_id = au_id_format.ToString();

                phone_format.Append(author.phone.Substring(0, 3)).Append(" ").
                    Append(author.phone.Substring(3, 3)).Append("-").
                    Append(author.phone.Substring(6, 4));

                author.phone = phone_format.ToString();
            }
                                  
            if (ModelState.IsValid)
            {
                if (author.au_id == null || author.au_lname == null || author.au_fname == null || author.phone == null)
                    ViewBag.Message = "Id, first name, last name and phone fields are required!";
                else
                {
                    db.authors.Add(author);
                    db.SaveChanges();
                    ViewBag.Message = "Author added successfully.";
                }
            }
            else { ViewBag.Message = "There has been an error during the creation. Please, check the fields again."; }

            return View(author);
        }

        // GET: Authors/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            author author = db.authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "au_id,au_lname,au_fname,phone,address,city,state,zip,contract")] author author)
        {
            if (ModelState.IsValid)
            {
                db.Entry(author).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Message = "Author's information edited successfully.";
            }
            return View(author);
        }

        // GET: Authors/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            author author = db.authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                author author = db.authors.Find(id);
                db.authors.Remove(author);
                db.SaveChanges();
                ViewBag.Message = "Author deleted successfully.";               
            }
            catch(DbUpdateException e)     //db conflict 
            {
                ViewBag.Message = "Deletion of this author is not available due to system restrictions.";
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
