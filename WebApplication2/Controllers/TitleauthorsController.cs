﻿using System;
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
    public class TitleauthorsController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: Titleauthors
        public ActionResult Index()
        {
            var titleauthors = db.titleauthors.Include(t => t.author).Include(t => t.title);
            return View(titleauthors.ToList());
        }
        
        public String getauthorID(String id)
        {
            var authors = db.authors.Find(id);
            return authors.ToString();
        }

        // GET: Titleauthors/Details/5
        public ActionResult Details(string au_id,string title_id)
        {
            if (title_id == null || au_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            titleauthor titleauthor = db.titleauthors.Find(au_id, title_id);

            if (titleauthor == null)
            {
                return HttpNotFound();
            }
            return View(titleauthor);
        }

        // GET: Titleauthors/Create
        public ActionResult Create()
        {
            ViewBag.au_id = new SelectList(db.authors, "au_id", "au_lname");
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title1");
            return View();
        }

        // POST: Titleauthors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "au_id,title_id,au_ord,royaltyper")] titleauthor titleauthor)
        {
            if(titleauthor.au_ord<2 && titleauthor.au_ord>0 && titleauthor.royaltyper > 0)
            {
                if (ModelState.IsValid)
                {
                    db.titleauthors.Add(titleauthor);
                    db.SaveChanges();
                    ViewBag.Message = "Author's book added successfully!";
                }
            }else
                ViewBag.Message = "Author order should be between 1 and 2 and royalty typer should be greater than 0.";
            ViewBag.au_id = new SelectList(db.authors, "au_id", "au_lname", titleauthor.au_id);
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title1", titleauthor.title_id);
            return View(titleauthor);
        }

        // GET: Titleauthors/Edit/5
        public ActionResult Edit(string au_id, string title_id)
        {
            if (title_id == null || au_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            titleauthor titleauthor = db.titleauthors.Find(au_id, title_id);
            if (titleauthor == null)
            {
                return HttpNotFound();
            }
            ViewBag.au_id = new SelectList(db.authors, "au_id", "au_lname", titleauthor.au_id);
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title1", titleauthor.title_id);
            return View(titleauthor);
        }

        // POST: Titleauthors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "au_id,title_id,au_ord,royaltyper")] titleauthor titleauthor)
        {
            if (titleauthor.au_ord < 2 && titleauthor.au_ord > 0 && titleauthor.royaltyper > 0)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(titleauthor).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Message = "Author's book added successfully!";
                }
            }else
                ViewBag.Message = "Author order should be between 1 and 2 and royalty typer should be greater than 0.";
            ViewBag.au_id = new SelectList(db.authors, "au_id", "au_lname", titleauthor.au_id);
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title1", titleauthor.title_id);
            return View(titleauthor);
        }

        // GET: Titleauthors/Delete/5
        public ActionResult Delete(string au_id, string title_id)
        {
            if (title_id == null || au_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            titleauthor titleauthor = db.titleauthors.Find(au_id, title_id);
            if (titleauthor == null)
            {
                return HttpNotFound();
            }
            return View(titleauthor);
        }

        // POST: Titleauthors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string au_id, string title_id)
        {
            titleauthor titleauthor = db.titleauthors.Find(au_id, title_id);
            db.titleauthors.Remove(titleauthor);
            db.SaveChanges();
            ViewBag.Message = "Author's book deleted successfully!";
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
