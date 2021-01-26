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
    public class EmployeesController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: Employees
        public ActionResult Index()
        {
            var employees = db.employees.Include(e => e.job).Include(e => e.publisher);
            return View(employees.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(string emp_id)
        {
            if (emp_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee employee = db.employees.Find(emp_id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.job_id = new SelectList(db.jobs, "job_id", "job_desc");
            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "emp_id,fname,minit,lname,job_id,job_lvl,pub_id,hire_date")] employee employee)
        {

            if (ModelState.IsValid)
            {
                if (employee.emp_id != null || employee.fname != null || employee.lname != null || employee.hire_date != null)
                {
                    db.employees.Add(employee);
                    db.SaveChanges();
                    ViewBag.Message = "Employee added successfully.";
                }
                else ViewBag.Message = "ID, first name, last name and hire date fields are required!";
            }
            ViewBag.job_id = new SelectList(db.jobs, "job_id", "job_desc", employee.job_id);
            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name", employee.pub_id);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(string emp_id)
        {
            if (emp_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee employee = db.employees.Find(emp_id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.job_id = new SelectList(db.jobs, "job_id", "job_desc", employee.job_id);
            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name", employee.pub_id);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "emp_id,fname,minit,lname,job_id,job_lvl,pub_id,hire_date")] employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Message = "Employee`s information edited successfully.";
            }
            ViewBag.job_id = new SelectList(db.jobs, "job_id", "job_desc", employee.job_id);
            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name", employee.pub_id);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(string emp_id)
        {
            if (emp_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee employee = db.employees.Find(emp_id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string emp_id)
        {
            employee employee = db.employees.Find(emp_id);
            db.employees.Remove(employee);
            db.SaveChanges();
            ViewBag.Message = "Employee deleted successfully.";
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
