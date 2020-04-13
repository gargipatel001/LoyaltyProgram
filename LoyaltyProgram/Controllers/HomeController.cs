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
            try
            {
                // Check for customer session
                if (Session["Customer"] != null)
                {
                    CustomerViewModel cvm = new CustomerViewModel();
                    cvm = (CustomerViewModel)Session["Customer"];
                    return View(cvm);
                }
                else
                {
                    // check for User(admin) session
                    if (Session["User"] != null)
                    {
                        User user = new User();
                        user = (User)Session["User"];
                        //cvm = (CustomerViewModel)Session["Customer"];
                        return View("AdminHome",user);
                    }
                    return RedirectToAction("Index", "Login");
                }
            }
            catch (Exception ex)
            {

                return RedirectToAction("Index", "Login");
            }
         
        
        }

        // Get customer by mail id
        public Customer GetCustomer(String email)
        {
            Customer customer = new Customer();
            try
            {
               
                customer = db.Customers.Where(_ => _.CustomerEmail == email && _.IsLoggedIn == true).FirstOrDefault();
                
            }
            catch (Exception ex)
            {

             
            }
            return customer;

        }
    }
}