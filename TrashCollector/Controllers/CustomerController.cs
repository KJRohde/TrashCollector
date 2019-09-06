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
    public class CustomerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Employees
        public ActionResult Index(int id)
        {
            Customer customer = db.Customers.Where(c => c.Id == id).Single();
            return View(customer);
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
        public ActionResult Create()
        {
            Customer customer = new Customer();
            return View(customer);
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "UserName,Id,FirstName,LastName,PickupActivity,StreetAddress,City,State,ZipCode,PickupDay,OneTimePickup")] Customer customer)
        {
            var currentUserId = User.Identity.GetUserId();
            customer.ApplicationUserId = currentUserId;
            customer.UserName = User.Identity.GetUserName();
            if (customer.ApplicationUserId == currentUserId)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index", new {id = customer.Id});
            }

            return View(customer);
        }
        public ActionResult OneTime(int id)
        {
            Customer oneTimeCustomer = db.Customers.Where(c => c.Id == id).Single();
            return View(oneTimeCustomer);
        }
        [HttpPost]
        public ActionResult OneTime([Bind(Include = "OneTimePickup")] int id, Customer customer)
        {
            Customer oneTimeCustomer = db.Customers.Where(c => c.Id == id).Single();
            oneTimeCustomer.OneTimePickup = customer.OneTimePickup;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Where(c => c.Id == id).Single();
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,PickupActivity,StreetAddress,City,State,ZipCode,PickupDay")]int id, Customer customer)
        {
            if (ModelState.IsValid)
            {
                Customer customerToEdit = db.Customers.Where(c => c.Id == id).Single();
                customerToEdit.FirstName = customer.FirstName;
                    customerToEdit.LastName = customer.LastName;
                    customerToEdit.PickupActivity = customer.PickupActivity;
                    customerToEdit.StreetAddress = customer.StreetAddress;
                    customerToEdit.City = customer.City;
                    customerToEdit.State = customer.State;
                    customerToEdit.ZipCode = customer.ZipCode;
                    customerToEdit.PickupDay = customer.PickupDay;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = customer.Id });
            }
            return View(customer);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
    }
}
