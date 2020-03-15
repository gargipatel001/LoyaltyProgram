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
            try
            {
                
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

                        // Session of type customer view model.
                        cvm.CustomerId = customer.CustomerId;
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
                        cvm.CustomerCardNo = customer.CustomerCardNo;
                        cvm.CustomerPhoneNumber = customer.CustomerPhoneNumber;
                        cvm.CustomerPostalCode = customer.CustomerPostalCode;

                        Session["Customer"] = cvm;
                    }

                }
                else
                {
                    message = "Invalid Email ID";
                }
                return Json(message, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                return Json("", JsonRequestBehavior.AllowGet);
            }
          
           

           





        }

        public ActionResult SignOut()
        {
            CustomerViewModel cvm = new CustomerViewModel();
            Customer c = new Customer();
            try
            {
                if (Session["Customer"] != null)
                {
                    cvm = (CustomerViewModel)Session["Customer"];
                    if (cvm != null)
                    {
                        c = db.Customers.Where(_ => _.IsLoggedIn == true && _.CustomerEmail == cvm.CustomerEmail).FirstOrDefault();
                        c.IsLoggedIn = false;
                        db.Entry(c).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    Session["Customer"] = null;

                }
            }
            catch (Exception ex)
            {

               
            }
          


            return RedirectToAction("Index");
        }
    }
}