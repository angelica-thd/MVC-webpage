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
            string query = "select top 1 * from roysched where title_id = @p0 and lorange = @p1 and hirange = @p2 and royalty = @p3 ";
            roysched roysched = db.royscheds.SqlQuery(query, new string[] { title_id, lorange.ToString(), hirange.ToString(), royalty.ToString()}).SingleOrDefault();
            if (roysched == null)
            {
                return HttpNotFound();
            }
            return View(roysched);
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
