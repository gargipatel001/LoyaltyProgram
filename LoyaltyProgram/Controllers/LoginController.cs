using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoyaltyProgram.ViewModels;
using LoyaltyProgram.Models;
using LoyaltyProgram.DAL;
using System.Data.Entity;

namespace LoyaltyProgram.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        private LoyaltyProgramContext db = new LoyaltyProgramContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(string email, string password)
        {
            String message = "";
            Customer customer = new Customer();
            CustomerViewModel cvm = new CustomerViewModel();
            customer = db.Customers.Where(_ => _.CustomerEmail == email).FirstOrDefault();
            if (customer != null && customer.CustomerId > 0)
            {
                if (customer.CustomerPassword != password)
                {
                    message = "Enter Correct Password";
                }
                else
                {
                    customer.IsLoggedIn = true;
                    db.Entry(customer).State = EntityState.Modified;
                    db.SaveChanges();

                    cvm.CustomerFirstName = customer.CustomerFirstName;
                    cvm.CustomerLastName = customer.CustomerLastName;
                    cvm.CustomerEmail = customer.CustomerEmail;
                    cvm.CustomerDOB = customer.CustomerDOB;
                    cvm.CustomerGender = customer.CustomerGender;
                    cvm.CustomerLoyaltyPoints = customer.CustomerLoyaltyPoints;
                    cvm.CustomerLevelId = customer.LevelId;
                    cvm.RoleId = customer.RoleId;
                    cvm.CustomerAddress = customer.CustomerAddress;
                    cvm.CustomerCity = customer.CustomerCity;
                    cvm.CustomerProvince = customer.CustomerProvince;
                    cvm.IsLoggedIn = customer.IsLoggedIn;

                    Session["Customer"] = cvm;
                }

            }
            else
            {
                message = "Invalid Email ID";
            }

            return Json(message, JsonRequestBehavior.AllowGet);





        }
    }
}