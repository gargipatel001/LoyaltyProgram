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
        public ActionResult Select([DataSourceRequest]DataSourceRequest request)
        {
            var data = db.PromotionTypes.ToList();
            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }






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
    }
}