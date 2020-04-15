using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LoyaltyProgram.DAL;
using LoyaltyProgram.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoyaltyProgram.Controllers
{
    public class CustomerLevelController : Controller
    {
        private LoyaltyProgramContext db = new LoyaltyProgramContext();
        // GET: CustomerLevel
        public ActionResult Index()
        {
            if (Session["User"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        // Bind CustomerLevel Grid
        public ActionResult Select([DataSourceRequest]DataSourceRequest request)
        {
            var data = db.CustomerLevels.ToList();
            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        // Customer LeveL Update
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, CustomerLevel customerLevel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(customerLevel).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {

                }
            }
            return RedirectToAction("Index");
        }
        // CustomerLevel Create
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, CustomerLevel customerLevel)
        {
            if (ModelState.IsValid)
            {
                db.CustomerLevels.Add(customerLevel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        //Check for existing customerlevels
        public ActionResult CheckCustomerLevels(string customerLevel)
        {
            try
            {
                int Count = db.CustomerLevels.Where(_ => _.LevelName == customerLevel).Count();
                return Json(Count, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }
       
    }
}