using LoyaltyProgram.Models;
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
        public ActionResult Index()
        {
            if (Session["Customer"]!= null)
            {
                Customer c = new Customer();
                c = (Customer)Session["Customer"];
            }
            return View();
        }
    }
}