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
            User user = new User();
            CustomerViewModel cvm = new CustomerViewModel();
            try
            {
                
                customer = db.Customers.Where(_ => _.CustomerEmail == email && _.IsActive == true).FirstOrDefault();
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
                    user = db.Users.Where(_ => _.UserEmail == email && _.IsActive == true).FirstOrDefault();
                    if (user != null && user.UserId > 0)
                    {
                        if (user.UserPassword != password)
                        {
                            message = "Enter correct password";
                        }
                        else {
                            user.isLoggedIn = true;
                            db.Entry(user).State = EntityState.Modified;
                            db.SaveChanges();

                            // User Session
                            Session["User"] = user;
                        }
                    }
                    else {
                        message = "Invalid Email ID";
                    }
                  
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
            User u = new User();
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
                else {
                    if (Session["User"] != null)
                    {
                        u = (User)Session["User"];
                        if (u!=null && u.UserId >0)
                        {
                            u = db.Users.Where(_ => _.isLoggedIn == true && _.UserEmail == u.UserEmail).FirstOrDefault();
                            u.isLoggedIn = false;
                            db.Entry(u).State = EntityState.Modified;
                            db.SaveChanges();

                        }
                        Session["User"] = null;
                    }
                }
            }
            catch (Exception ex)
            {

               
            }
          


            return RedirectToAction("Index");
        }
    }
}