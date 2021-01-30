using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class SalesController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: Sales
        public ActionResult Index()
        {
            var sales = db.sales.Include(s => s.store).Include(s => s.title);
            return View(sales.ToList());
        }

        // GET: Sales/Details/5
        public ActionResult Details(string stor_id, string ord_num, string title_id)
        {
            if (stor_id == null || ord_num == null || title_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sale sale = db.sales.Find(stor_id,ord_num,title_id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // GET: Sales/Create
        public ActionResult Create()
        {
            ViewBag.stor_id = new SelectList(db.stores, "stor_id", "stor_name");
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title1");
            var query = db.sales.Select(x => x.payterms).Distinct();
            ViewBag.payterms = new SelectList(query);
            ViewBag.ord_num = putOrderNumber();
            return View();
        }

        public string putOrderNumber()
        {
            var orders = db.sales.Select(x => x.ord_num).ToList();
            string newOrder = generateOrder();
            while (orders.Contains(newOrder))
                newOrder = generateOrder();
            return newOrder;        
        }

        public string generateOrder()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, random.Next(4, 20)).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        // POST: Sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "stor_id,ord_num,ord_date,qty,payterms,title_id")] sale sale)
        {
            if(sale.stor_id != null && sale.title_id != null && sale.payterms != null && sale.ord_date != null )
            {
                sale.ord_date = sale.ord_date.ToLocalTime();

                if (ModelState.IsValid)
                {
                    db.sales.Add(sale);
                    db.SaveChanges();
                    ViewBag.Message = "Order added successfully!";
                    //return RedirectToAction("Index");
                }
            }
            else ViewBag.Message = "Please fill in all the required fields!";

            ViewBag.stor_id = new SelectList(db.stores, "stor_id", "stor_name", sale.stor_id);
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title1", sale.title_id);
            var query = db.sales.Select(x => x.payterms).Distinct();
            ViewBag.payterms = new SelectList(query);
            return View(sale);
        }

        // GET: Sales/Edit/5
        public ActionResult Edit(string stor_id, string ord_num, string title_id)
        {
            if (stor_id == null || ord_num == null || title_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sale sale = db.sales.Find(stor_id, ord_num, title_id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            ViewBag.stor_id = new SelectList(db.stores, "stor_id", "stor_name", sale.stor_id);
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title1", sale.title_id);
            var payterms_query = db.sales.Select(x => x.payterms).Distinct();
            ViewBag.payterms = new SelectList(payterms_query);
            ViewBag.ord_date = sale.ord_date;
            return View(sale);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "stor_id,ord_num,ord_date,qty,payterms,title_id")] sale sale)
        {
            if (sale.stor_id != null && sale.title_id != null && sale.payterms != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(sale).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Message = "Order changed successfully!";
                    // return RedirectToAction("Index");
                }
            }
            else ViewBag.Message = "Please fill in all the required fields!";

            ViewBag.stor_id = new SelectList(db.stores, "stor_id", "stor_name", sale.stor_id);
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title1", sale.title_id);
            var query = db.sales.Select(x => x.payterms).Distinct();
            ViewBag.payterms = new SelectList(query);
            return View(sale);
        }

        // GET: Sales/Delete/5
        public ActionResult Delete(string stor_id, string ord_num, string title_id)
        {
            if (stor_id == null || ord_num == null || title_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sale sale = db.sales.Find(stor_id, ord_num, title_id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string stor_id, string ord_num, string title_id)
        {
            sale sale = db.sales.Find(stor_id, ord_num, title_id);
            db.sales.Remove(sale);
            db.SaveChanges();
            ViewBag.Message = "Order deleted successfully!";
            return View();
            //return RedirectToAction("Index");
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
