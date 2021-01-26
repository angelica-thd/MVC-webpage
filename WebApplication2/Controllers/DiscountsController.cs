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
            string query = "select stor_name,discounts.stor_id,discounttype,lowqty,highqty,discount as discount1 from discounts inner join stores on discounts.stor_id = stores.stor_id";
            IEnumerable<discount> discounts = db.Database.SqlQuery<discount>(query);
         
            foreach (discount dis in discounts.ToList())
            {
                ViewBag.stor_name = new SelectList(db.stores, "stor_name", "stor_id", dis.stor_id);
            }
            return View(discounts.ToList());
        }

        // GET: Discounts/Details/5
        public ActionResult Details(string discounttype, string stor_id, string discount1)
        {
            if (discounttype == null || discount1 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string query = "select * from discounts where discounttype = @p0 and stor_id = @p1  and discount1= @p2";
            discount discount = db.discounts.SqlQuery(query,  discounttype, stor_id, discount1 ).SingleOrDefault();
            ViewBag.stor_name = new SelectList(db.stores, "stor_name", "stor_id", discount.stor_id);
            if (discount == null)
            {
                return HttpNotFound();
            }
            return View(discount);
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
