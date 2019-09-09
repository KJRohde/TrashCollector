using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class EmployeeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Employees
        public ActionResult Index(int id, string chosenDay)
        {
            var today = DateTime.Now.DayOfYear.ToString();
            var suspendedCustomer = db.Customers.Where(s => s.SuspensionStart == today).ToList();
            if (suspendedCustomer != null)
            {
                foreach(Customer customer in suspendedCustomer)
                {
                    customer.PickupActivity = false;
                }
            }
            var returningCustomer = db.Customers.Where(s => s.SuspensionEnd == today).ToList();
            if (returningCustomer != null)
            {
                foreach (Customer customer in suspendedCustomer)
                {
                    customer.PickupActivity = true;
                }
            }
            Employee employee = db.Employees.Where(c => c.Id == id).Single();
            var zipCustomers = db.Customers.Where(u => u.ZipCode == employee.AreaZipCode).ToList();
            if (chosenDay == null)
            {
                var todayCustomers = zipCustomers.Where(z => z.PickupActivity == true && z.PickupDay.ToString() == today || z.OneTimePickup == today);
                return View(todayCustomers);
            }
            else
            {
                var chosenCustomers = zipCustomers.Where(z => z.PickupActivity == true && z.PickupDay.ToString() == chosenDay.ToString());
                return View(chosenCustomers);
            }
        }

        public ActionResult AllCustomers()
        {
            var currentEmployee = User.Identity.GetUserId();
            Employee employee = db.Employees.Where(c => c.ApplicationUserId == currentEmployee).Single();
            var zipCustomers = db.Customers.Where(u => u.ZipCode == employee.AreaZipCode).ToList();
            return View(zipCustomers);
        }
        public ActionResult DailyCustomers(DateTime chosenDay)
        {
            var currentEmployee = User.Identity.GetUserId();
            Employee employee = db.Employees.Where(c => c.ApplicationUserId == currentEmployee).Single();
            var zipCustomers = db.Customers.Where(u => u.ZipCode == employee.AreaZipCode).ToList();
            var dayCustomers = zipCustomers.Where(z => z.PickupDay.ToString() == chosenDay.ToString());
            return View(dayCustomers);
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            Employee employee = new Employee();
            return View(employee);
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "UserName,Id,AreaZipCode,Email,FirstName,LastName")] Employee employee)
        {
            var currentUserId = User.Identity.GetUserId();
            employee.ApplicationUserId = currentUserId;
            employee.UserName = User.Identity.GetUserName();
            if (employee.ApplicationUserId == currentUserId)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = employee.Id });
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AreaZipCode,Email,FirstName,LastName")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Charge(int id)
        {
            Customer customer = db.Customers.Where(c => c.Id == id).Single();
            {
                var employeeId = User.Identity.GetUserId();
                var employeeCharging = db.Employees.Where(e => e.ApplicationUserId == employeeId).Single();
                double pickupCharge = 15.00;
                
                if (ModelState.IsValid)
                {
                    Customer customerToCharge = db.Customers.Where(c => c.Id == id).Single();
                    customerToCharge.MonthlyBill += pickupCharge;
                    db.SaveChanges();
                    return RedirectToAction("Index", new { id = employeeCharging.Id });
                }
                return View();
            }
        }
    }
}
