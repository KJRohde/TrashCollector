using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(string username)
        {
            if (User.IsInRole("Customer"))
            {
                Customer customer = db.Customers.FirstOrDefault(c => c.UserName == username);
                return RedirectToAction("Index", "Customer", new { id = customer.Id });
            }
            if (User.IsInRole("Employee"))
            {
                Employee employee = db.Employees.FirstOrDefault(e => e.UserName == username);
                return RedirectToAction("Index", "Employee", new { id = employee.Id });
            }
            else
            {
                return View();
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}