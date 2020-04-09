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
    public class ManageFAQController : Controller
    {
        private LoyaltyProgramContext db = new LoyaltyProgramContext();
        // GET: ManageFAQ
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult Select([DataSourceRequest]DataSourceRequest request)
        {
            var data = db.FAQs.ToList();
            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, FAQ faq)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(faq).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {

                }
            }
            return RedirectToAction("Index");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, FAQ faq)
        {
            if (ModelState.IsValid)
            {
                db.FAQs.Add(faq);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
        public ActionResult UpdateStatus(int Id)
        {
            try
            {
                FAQ fAQ = new FAQ();
                fAQ = db.FAQs.Where(_ => _.FAQId == Id).FirstOrDefault();
                if (fAQ != null && fAQ.FAQId > 0)
                {
                    fAQ.IsActive = !fAQ.IsActive;
                    db.Entry(fAQ).State = EntityState.Modified;
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
    }
}