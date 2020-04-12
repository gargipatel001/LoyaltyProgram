using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LoyaltyProgram.DAL;
using LoyaltyProgram.Models;
using LoyaltyProgram.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace LoyaltyProgram.Controllers
{
    public class ManageCustomersController : Controller
    {
        // GET: ManageCustomers
        private LoyaltyProgramContext db = new LoyaltyProgramContext();
        //CustomerViewModel cvm = new CustomerViewModel();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Select([DataSourceRequest]DataSourceRequest request)
        {
            List<CustomerViewModel> customerViewModel = new List<CustomerViewModel>();
            List<Customer> customers = new List<Customer>();
            customers = db.Customers.Include(_ => _.Level).ToList();
            if (customers.Count > 0)
            {
                foreach (Customer customer in customers)
                {
                    CustomerViewModel cvm = new CustomerViewModel();
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
                    cvm.CustomerLoyaltyPoints = customer.CustomerLoyaltyPoints;
                    cvm.IsActive = customer.IsActive;
                    cvm.Level = new CustomerLevelViewModel();
                    cvm.Level.LevelName = customer.Level.LevelName;
                    customerViewModel.Add(cvm);
                }
               
                
            }
            //var data = db.Promotions.Include(_ => _.PromotionType).Include(_ => _.Partner).ToList();
           
            return Json(customerViewModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, CustomerViewModel customer)
        {
          
                try
                {
                    if (customer!=null && customer.CustomerId > 0)
                    {
                        Customer customerModel = new Customer();
                        customerModel = db.Customers.Where(_ => _.CustomerId == customer.CustomerId).FirstOrDefault();
                        customerModel.CustomerFirstName = customer.CustomerFirstName;
                        customerModel.CustomerLastName = customer.CustomerLastName;
                        customerModel.CustomerEmail = customer.CustomerEmail;
                        customerModel.CustomerDOB = customer.CustomerDOB;
                        customerModel.CustomerGender = customer.CustomerGender;
                        customerModel.CustomerAddress = customer.CustomerAddress;
                        customerModel.CustomerCity = customer.CustomerCity;
                        customerModel.CustomerProvince = customer.CustomerProvince;
                        customerModel.CustomerPhoneNumber = customer.CustomerPhoneNumber;
                        customerModel.CustomerPostalCode = customer.CustomerPostalCode;
                        db.Entry(customerModel).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                   


                }
                catch (Exception ex)
                {

                }

           

            return RedirectToAction("Index");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateStatus(int Id)
        {
            try
            {
               Customer customer = new Customer();
                customer = db.Customers.Where(_ => _.CustomerId == Id).FirstOrDefault();
                if (customer != null && customer.CustomerId > 0)
                {
                    customer.IsActive = !customer.IsActive;
                    db.Entry(customer).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { status = "Success", message = "Success" });
                }
                else
                {
                    return Json(new { status = "Error", message = "Error" });
                }


            }
            catch (Exception ex)
            {
                //ErrorLogger.LogError("UpdateStatus();", ex);
            }
            return RedirectToAction("Index");

        }
        public ActionResult CustomersDetail(int customerId)
        {
            CustomerViewModel cvm = new CustomerViewModel();
            Customer customer = new Customer();
            customer = db.Customers.Where(_ => _.CustomerId == customerId).Include(_=>_.Level).FirstOrDefault();
            if (customer != null && customer.CustomerId > 0)
            {
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
                cvm.CustomerLoyaltyPoints = customer.CustomerLoyaltyPoints;
                cvm.IsActive = customer.IsActive;
                cvm.Level = new CustomerLevelViewModel();
                cvm.Level.LevelName = customer.Level.LevelName;
                
            }
            return View("CustomersDetail",cvm);
        }
    }
}