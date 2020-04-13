using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LoyaltyProgram.DAL;
using LoyaltyProgram.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LoyaltyProgram.Controllers
{
    public class PromotionTypeController : Controller
    {
        private LoyaltyProgramContext db = new LoyaltyProgramContext();

        // GET: PromotionType
        public ActionResult Index()
        {

            return View();
        }
        // Get All Promotion Types
        public ActionResult Select([DataSourceRequest]DataSourceRequest request)
        {
            var data = db.PromotionTypes.ToList();
            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        // Update Promotion Type

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, PromotionType promotionType)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(promotionType).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {

                }
            }
            return RedirectToAction("Index");
        }

        // Create New Promotion

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, PromotionType promotionType)
        {
            if (ModelState.IsValid)
            {
                db.PromotionTypes.Add(promotionType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        // Check Existing PromotionType
        public ActionResult CheckPromotionType(string promotionType)
        {
            try
            {
                int Count = db.PromotionTypes.Where(_ => _.PromotionTypeName == promotionType).Count();

                return Json(Count, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }

        // Update PromotionType Status
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateStatus(int Id)
        {
            try
            {
                PromotionType promotionType = new PromotionType();
                promotionType = db.PromotionTypes.Where(_ => _.PromotionTypeId == Id).FirstOrDefault();
                if (promotionType != null && promotionType.PromotionTypeId > 0)
                {
                    promotionType.IsActive = !promotionType.IsActive;
                    db.Entry(promotionType).State = EntityState.Modified;
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