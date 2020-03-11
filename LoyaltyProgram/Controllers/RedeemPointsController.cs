using LoyaltyProgram.DAL;
using LoyaltyProgram.ViewModels;
using LoyaltyProgram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data.Entity;


namespace LoyaltyProgram.Controllers
{
    public class RedeemPointsController : Controller
    {
        // GET: RedeemPoints
        private LoyaltyProgramContext db = new LoyaltyProgramContext();
        public ActionResult Index()
        {
            PromotionViewModel pvm = new PromotionViewModel();
            List<Promotion> promotions = new List<Promotion>();
            List<PromotionViewModel> promotionsViewModel = new List<PromotionViewModel>();
            try
            {
                if (Session["Customer"] != null)
                {
                    promotions = GetAllPromotions();
                    if (promotions != null && promotions.Count > 0)
                    {
                        foreach (var item in promotions)
                        {
                            pvm.PromotionId = item.PromotionId;
                            pvm.PromotionName = item.PromotionName;
                            pvm.PromotionStartDate = item.PromotionStartDate;
                            pvm.PromotionEndDate = item.PromotionEndDate;
                            pvm.PromotionDesc = item.PromotionDesc;
                            pvm.PromotionTitle = item.PromotionTitle;
                            pvm.PromotionPoints = item.PromotionPoints;
                            pvm.Partner = new PartnerViewModel();
                            pvm.Partner.PartnerId = item.Partner.PartnerId;
                            pvm.Partner.PartnerName = item.Partner.PartnerName;
                           //string p = "Content\\"+item.Partner.PartnerLogo;
                            string fullpath ="Content\\"+item.Partner.PartnerLogo;

                            //string fullpath = Path.GetFullPath(item.Partner.PartnerLogo);
                            pvm.Partner.PartnerLogo = fullpath;
                            promotionsViewModel.Add(pvm);
                        }
                    }

                }
            }
            catch (Exception ex)
            {


            }
       

                return View(promotionsViewModel);
        }
        public List<Promotion> GetAllPromotions()
        {
            List<Promotion> promotions = new List<Promotion>();
            promotions = db.Promotions.Include(_ => _.Partner).Include(_=>_.PromotionType).ToList();
            return promotions;
        }

    }
}