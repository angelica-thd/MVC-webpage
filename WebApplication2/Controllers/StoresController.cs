﻿using System;
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
    public class StoresController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: Stores
        public ActionResult Index()
        {
            return View(db.stores.ToList());
        }

        // GET: Stores/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            store store = db.stores.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        // GET: Stores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "stor_id,stor_name,stor_address,city,state,zip")] store store)
        {
            if (store.stor_id != null && store.stor_name != null && store.stor_address != null)
            {
                if (ModelState.IsValid)
                {
                    db.stores.Add(store);
                    db.SaveChanges();
                    ViewBag.Message = "Store added successfully!";
                }
            }
            else
                ViewBag.Message = "Please fill in all the required fields!";
             return View(store);
        }
       

        // GET: Stores/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            store store = db.stores.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        // POST: Stores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "stor_id,stor_name,stor_address,city,state,zip")] store store)
        {
            if(store.stor_id != null && store.stor_name != null && store.stor_address != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(store).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Message = "Store changed successfully!";
                    // return RedirectToAction("Index");
                }
                else ViewBag.Message = "Please fill in all the required fields!";

            } 
            return View(store);
        }

        // GET: Stores/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            store store = db.stores.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        // POST: Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                store store = db.stores.Find(id);
                db.stores.Remove(store);
                db.SaveChanges();
                ViewBag.Message = "Store deleted successfully!";
            }catch(DbUpdateException e)
            {
                ViewBag.Message = "Deletion of this store is not available due to system restrictions.";
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
