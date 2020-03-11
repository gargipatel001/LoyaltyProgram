using LoyaltyProgram.DAL;
using LoyaltyProgram.Models;
using LoyaltyProgram.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoyaltyProgram.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private LoyaltyProgramContext db = new LoyaltyProgramContext();
        public ActionResult Index()
        {
            if (Session["Customer"] != null)
            {
                CustomerViewModel cvm = new CustomerViewModel();
                cvm = (CustomerViewModel)Session["Customer"];
                //Customer c = new Customer();
                //c = GetCustomer(cvm.CustomerEmail);
                //if (true)
                //{

                //}

               
                return View(cvm);
            }
            else {
                return RedirectToAction("Index", "Login");
            }
        
        }

        public Customer GetCustomer(String email)
        {
            Customer customer = new Customer();
            customer = db.Customers.Where(_ => _.CustomerEmail == email && _.IsLoggedIn == true).FirstOrDefault();
            return customer;
        }
    }
}