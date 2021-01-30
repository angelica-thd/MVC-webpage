using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class Pub_infoController : Controller
    {
        public byte[] current_logo = null;
        private pubsEntities db = new pubsEntities();

        // GET: Pub_info
        public ActionResult Index()
        {
            var pub_info = db.pub_info.Include(p => p.publisher);
            return View(pub_info.ToList());
        }

       
        // GET: Pub_info/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pub_info pub_info = db.pub_info.Find(id);
            if (pub_info == null)
            {
                return HttpNotFound();
            }
            return View(pub_info);
        }

        // GET: Pub_info/Create
        public ActionResult Create()
        {
            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name");
            return View();
        }

        // POST: Pub_info/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pub_id,logo,pr_info")] pub_info pub_info)
        {
            if (ModelState.IsValid)
            {
                 db.pub_info.Add(pub_info);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name", pub_info.pub_id);
            return View(pub_info);
        }

        // GET: Pub_info/Edit/5
        public ActionResult Edit(string id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pub_info pub_info = db.pub_info.Find(id);
            current_logo = pub_info.logo;
            ViewBag.logo = Base64Encode(pub_info.logo.ToString());
            if (pub_info == null)
            {
                return HttpNotFound();
            }
            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name", pub_info.pub_id);
            return View(pub_info);
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        // POST: Pub_info/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pub_id,logo,pr_info")] pub_info pub_info)
        {
           // pub_info.logo = current_logo;
            if (ModelState.IsValid)
            {
                db.Entry(pub_info).State = EntityState.Modified;
               db.SaveChanges();
                 string query = "update pub_info set pr_info = @p0 where pub_id = @p1 ";
                 
                // pub_info pub_Info = db.pub_info.SqlQuery(query, new string[] {pub_info.pr_info,pub_info.pub_id}).SingleOrDefault();
                ViewBag.Message = "updated";
                //return RedirectToAction("Index");
            }
            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name", pub_info.pub_id);
            ViewBag.Message = "not updated ";
            return View();
        }
        // GET: Pub_info/Delete/5
        public ActionResult Delete(string pub_id)
        {
            if (pub_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pub_info pub_info = db.pub_info.Find(pub_id);
            if (pub_info == null)
            {
                return HttpNotFound();
            }
            return View(pub_info);
        }

        // POST: Pub_info/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string pub_id)
        {
            try
            {
                pub_info pub_Info = db.pub_info.Find(pub_id);
           
                db.pub_info.Remove(pub_Info);
                db.SaveChanges();
                ViewBag.Message = "Publisher's information deleted successfully!";
           
            }
            catch (DbUpdateException e)     //db conflict 
            {
                ViewBag.Message = "Deletion of this publisher's information is not available due to system restrictions.";
            }

            return View();
        }
        /*
         public ActionResult UpdateLogo(HttpPostedFileBase file)
         {
             pub_info pub_info = Include("pub_id,logo,pr_info")

             byte[] logo = null;
             try
             {
                 if (file != null && file.ContentLength > 0)
                 {
                     var inputStream = file.InputStream;
                     var memoryStream = inputStream as MemoryStream;
                     if (memoryStream == null)
                     {
                         memoryStream = new MemoryStream();
                         inputStream.CopyTo(memoryStream);
                     }
                     logo = memoryStream.ToArray();
                     ViewBag.Message = "Updated logo.";
                     return View();
                 }
             }
             catch (Exception e)
             {
                 ViewBag.Message = "Logo update failed.";
                 return null;
             }
             return logo;

         } */

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
