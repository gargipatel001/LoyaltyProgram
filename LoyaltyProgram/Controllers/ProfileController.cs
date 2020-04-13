using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoyaltyProgram.DAL;
using LoyaltyProgram.Models;
using LoyaltyProgram.ViewModels;


namespace LoyaltyProgram.Controllers
{
    public class ProfileController : Controller
    {
        CustomerViewModel cvm = new CustomerViewModel();
        private LoyaltyProgramContext db = new LoyaltyProgramContext(); 
        //Get Customer's detail
        public ActionResult Index()
        {
            if (Session["Customer"] != null)
            {
                cvm = (CustomerViewModel)Session["Customer"];
                return View("Index", cvm);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

          
        } 

        // Update Customer's Profile
        public ActionResult UpdateProfile(CustomerViewModel customerViewModel)
        {
            try
            {
                if (Session["Customer"] != null)
                {
                    //CustomerViewModel cvm = new CustomerViewModel();
                    Customer c = new Customer();
                    cvm = (CustomerViewModel)Session["Customer"];

                    c = db.Customers.Where(_ => _.CustomerEmail == customerViewModel.CustomerEmail).FirstOrDefault();
                    c.CustomerFirstName = customerViewModel.CustomerFirstName;
                    c.CustomerLastName = customerViewModel.CustomerLastName;
                    c.CustomerGender = customerViewModel.CustomerGender;
                    c.CustomerAddress = customerViewModel.CustomerAddress;
                    c.CustomerCity = customerViewModel.CustomerCity;
                    c.CustomerPostalCode = customerViewModel.CustomerPostalCode;
                    c.CustomerProvince = customerViewModel.CustomerProvince;
                    c.CustomerDOB = customerViewModel.CustomerDOB;
                    c.CustomerPhoneNumber = customerViewModel.CustomerPhoneNumber;

                    db.Entry(c).State = EntityState.Modified;
                    db.SaveChanges();

                    cvm.CustomerFirstName = customerViewModel.CustomerFirstName;
                    cvm.CustomerLastName = customerViewModel.CustomerLastName;
                    cvm.CustomerGender = customerViewModel.CustomerGender;
                    cvm.CustomerAddress = customerViewModel.CustomerAddress;
                    cvm.CustomerCity = customerViewModel.CustomerCity;
                    cvm.CustomerPostalCode = customerViewModel.CustomerPostalCode;
                    cvm.CustomerProvince = customerViewModel.CustomerProvince;
                    cvm.CustomerDOB = customerViewModel.CustomerDOB;
                    cvm.CustomerPhoneNumber = customerViewModel.CustomerPhoneNumber;
                    Session["Customer"] = cvm;


                }
                else
                {
                    return RedirectToAction("Index","Login");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
           
          
            return RedirectToAction("Index");
        }

    }
}
