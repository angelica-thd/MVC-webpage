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
    public class DiscountsController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: Discounts
        public ActionResult Index()
        {
            string query = "select stores.stor_name,discounts.stor_id,discounttype,lowqty,highqty,discount as discount1 from discounts left outer join stores on discounts.stor_id = stores.stor_id";
            IEnumerable<discount> discounts = db.Database.SqlQuery<discount>(query);
            db.discounts.Include(p => p.store);
            return View(discounts.ToList());
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
