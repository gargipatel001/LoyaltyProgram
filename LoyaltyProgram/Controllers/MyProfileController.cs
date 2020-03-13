﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoyaltyProgram.ViewModels;
using LoyaltyProgram.Models;
using LoyaltyProgram.DAL;


namespace LoyaltyProgram.Controllers
{
    public class MyProfileController : Controller
    {
        // GET: MyProfile
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