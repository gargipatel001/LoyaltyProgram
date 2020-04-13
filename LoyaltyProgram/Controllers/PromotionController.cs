using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LoyaltyProgram.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using LoyaltyProgram.Models;
using LoyaltyProgram.ViewModels;

namespace LoyaltyProgram.Controllers
{
    public class PromotionController : Controller
    {
        // GET: Promotion
        private LoyaltyProgramContext db = new LoyaltyProgramContext();
        public ActionResult Index()
        {

            return View();
        }
        //Bind PromotionPartner dropdown
        public JsonResult BindPromotionPartners()
        {

            List<Partner> promotionPartners = new List<Partner>();
            promotionPartners = db.Partners.Where(_ => _.IsActive == true).ToList();
            List<PartnerViewModel> partnersViewModel = new List<PartnerViewModel>();
            foreach (var item in promotionPartners)
            {
                PartnerViewModel pvm = new PartnerViewModel();
                pvm.PartnerId = item.PartnerId;
                pvm.PartnerName = item.PartnerName;
                partnersViewModel.Add(pvm);
            }
            return Json(partnersViewModel, JsonRequestBehavior.AllowGet);
        }
        //Bind PromotionType dropdown
        public JsonResult BindPromotionTypes()
        {
            List<PromotionType> promotionTypes = new List<PromotionType>();
            promotionTypes = db.PromotionTypes.Where(_ => _.IsActive == true).ToList();
            List<PromotionTypeViewModel> promotionTypesViewModel = new List<PromotionTypeViewModel>();
            foreach (var item in promotionTypes)
            {
                PromotionTypeViewModel pvm = new PromotionTypeViewModel();
                pvm.PromotionTypeId = item.PromotionTypeId;
                pvm.PromotionTypeName = item.PromotionTypeName;
                promotionTypesViewModel.Add(pvm);
            }
            return Json(promotionTypesViewModel, JsonRequestBehavior.AllowGet);
        }

        //Get All Promotions
        public ActionResult Select([DataSourceRequest]DataSourceRequest request)
        {
            List<PromotionViewModel> promotionViewModel = new List<PromotionViewModel>();
            var data = db.Promotions.Include(_ => _.PromotionType).Include(_ => _.Partner).ToList();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        // Update Promotions
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, Promotion promotion)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(promotion).State = EntityState.Modified;
                    db.SaveChanges();

                }
                catch (Exception ex)
                {

                }

            }

            return RedirectToAction("Index");
        }

        // Create Promotions

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, Promotion promotion)
        {
            if (ModelState.IsValid)
            {
                promotion.Partner = null;
                promotion.PromotionType = null;
                db.Promotions.Add(promotion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
        // Update Promotion Status
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateStatus(int Id)
        {
            try
            {
                Promotion promotion = new Promotion();
                promotion = db.Promotions.Where(_ => _.PromotionId == Id).FirstOrDefault();
                if (promotion != null && promotion.PromotionId > 0)
                {
                    promotion.IsActive = !promotion.IsActive;
                    db.Entry(promotion).State = EntityState.Modified;
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