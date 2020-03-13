using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoyaltyProgram.ViewModels;


namespace LoyaltyProgram.Controllers
{
    public class ProfileController : Controller
    {
        CustomerViewModel cvm = new CustomerViewModel();
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
    }
}
