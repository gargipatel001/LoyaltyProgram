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
        public JsonResult BindPromotionPartners()
        {

            List<Partner> promotionPartners = new List<Partner>();
            promotionPartners = db.Partners.Where(_=>_.IsActive == true).ToList();
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
        public ActionResult Select([DataSourceRequest]DataSourceRequest request)
        {
            List<PromotionViewModel> promotionViewModel = new List<PromotionViewModel>();
            var data = db.Promotions.Include(_=>_.PromotionType).Include(_=>_.Partner).ToList();
            //foreach (var item in data)
            //{
            //    PromotionViewModel pvm = new PromotionViewModel();
            //    pvm.PromotionId = item.PromotionId;
            //    pvm.PromotionName = item.PromotionName;
            //    pvm.PromotionTitle = item.PromotionTitle;
            //    pvm.PromotionPoints = item.PromotionPoints;
            //    pvm.PromotionDesc = item.PromotionDesc;
            //    pvm.PromotionStartDate = item.PromotionStartDate;
            //    pvm.PromotionEndDate = item.PromotionEndDate;
            //    pvm.PartnerId = item.PartnerId;
            //    pvm.PartnerName = item.Partner.PartnerName;
            //    pvm.PromotionTypeId = item.PromotionTypeId;
            //    pvm.promotionType = item.PromotionType.PromotionTypeName;
            //    promotionViewModel.Add(pvm);
            //}
            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, Promotion promotion)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Promotion promotion = new Promotion();
                    //promotion.PromotionName = promotionviewModel.PromotionName;
                    //promotion.PromotionTitle = promotionviewModel.PromotionTitle;
                    //promotion.PromotionDesc = promotionviewModel.PromotionDesc;
                    //promotion.PromotionStartDate = promotionviewModel.PromotionStartDate;
                    //promotion.PromotionEndDate = promotionviewModel.PromotionEndDate;
                    //promotion.PartnerId = promotionviewModel.PartnerId;
                    //promotion.PromotionTypeId = promotionviewModel.PromotionTypeId;


                    db.Entry(promotion).State = EntityState.Modified;
                    db.SaveChanges();
                    
                }
                catch (Exception ex)
                {

                }
               
            }
           
            return RedirectToAction("Index");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, Promotion promotion)
        {
            if (ModelState.IsValid)
            {
                promotion.Partner = null;
                promotion.PromotionType = null;
                //Promotion promotion = new Promotion();
                //promotion.PromotionName = promotionviewModel.PromotionName;
                //promotion.PromotionTitle = promotionviewModel.PromotionTitle;
                //promotion.PromotionDesc = promotionviewModel.PromotionDesc;
                //promotion.PromotionStartDate = promotionviewModel.PromotionStartDate;
                //promotion.PromotionEndDate = promotionviewModel.PromotionEndDate;
                //promotion.PartnerId = promotionviewModel.PartnerId;
                //promotion.PromotionTypeId = promotionviewModel.PromotionTypeId;

                db.Promotions.Add(promotion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
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
                else {
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