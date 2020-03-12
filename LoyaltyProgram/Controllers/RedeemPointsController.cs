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

            List<Promotion> promotions = new List<Promotion>();
            List<PromotionViewModel> promotionsViewModel = new List<PromotionViewModel>();
            try
            {
                if (Session["Customer"] != null)
                {
                    CustomerViewModel cvm = new CustomerViewModel();
                    cvm = (CustomerViewModel)Session["Customer"];
                    promotions = GetAllPromotions();
                    if (promotions != null && promotions.Count > 0)
                    {
                        foreach (var item in promotions)
                        {
                            PromotionViewModel pvm = new PromotionViewModel();
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
                            pvm.customerLoyaltyPoints = cvm.CustomerLoyaltyPoints;
                            pvm.isRedeemable = pvm.customerLoyaltyPoints < pvm.PromotionPoints ? false : true;

                            //string p = "Content\\"+item.Partner.PartnerLogo;
                            string fullpath = "Content\\" + item.Partner.PartnerLogo;

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
            promotions = db.Promotions.Include(_ => _.Partner).Include(_ => _.PromotionType).ToList();
            return promotions;
        }
        public ActionResult CheckPoints(int qty, int promotionpoints)
        {
            string message = "";
            if (Session["Customer"] != null)
            {
                CustomerViewModel cvm = new CustomerViewModel();
                cvm = (CustomerViewModel)Session["Customer"];
                int purchasetotal = qty * promotionpoints;
                if (purchasetotal > cvm.CustomerLoyaltyPoints)
                {
                    message = "You donot have sufficient points. Please change the quantity";
                }
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getData(int promotionPoints)
        {
            try
            {
                double customerPoints = 0;
                int possibleQunatity = 0;
                List<int> quantities = new List<int>();

                if (Session["Customer"] != null)
                {
                    CustomerViewModel cvm = new CustomerViewModel();
                    cvm = (CustomerViewModel)Session["Customer"];
                    customerPoints = cvm.CustomerLoyaltyPoints;
                    if (customerPoints > promotionPoints)
                    {
                        possibleQunatity = (int)customerPoints / promotionPoints;
                        if (possibleQunatity == 1)
                        {
                            quantities.Append(1).ToList();
                        }
                        else
                        {
                            quantities = Enumerable.Range(1, possibleQunatity).ToList();
                        }
                    }


                }
                if (quantities != null && quantities.Count > 0)
                {
                    var stringlist = quantities.Select(x => new SelectListItem
                    {
                        Value = x.ToString(),
                        Text = x.ToString()
                    }).ToList();
                    return Json(stringlist, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);

                }




            }
            catch (Exception ex)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult RedeemPoints(int promotionId)
        {
            Promotion p = new Promotion();
            p = db.Promotions.Include(_ => _.Partner).Include(_ => _.PromotionType).Where(_ => _.PromotionId == promotionId).FirstOrDefault();
            // double pointsleft = 0;
            try
            {
                if (Session["Customer"] != null)
                {
                    CustomerViewModel cvm = new CustomerViewModel();
                    cvm = (CustomerViewModel)Session["Customer"];

                    PointRedeemHistory pointsRedeem = new PointRedeemHistory();
                    pointsRedeem.CustomerId = cvm.CustomerId;
                    pointsRedeem.PointsRedeemedOn = DateTime.Now;
                    pointsRedeem.PromotionId = p.PromotionId;
                    pointsRedeem.PointsRedeemed = p.PromotionPoints;
                    db.PointRedeemHistories.Add(pointsRedeem);

                    // pointsleft = cvm.CustomerLoyaltyPoints - p.PromotionPoints;
                    cvm.CustomerLoyaltyPoints = cvm.CustomerLoyaltyPoints - p.PromotionPoints;

                    Customer c = new Customer();
                    c = db.Customers.Where(_ => _.CustomerId == cvm.CustomerId).FirstOrDefault();
                    c.CustomerLoyaltyPoints = cvm.CustomerLoyaltyPoints;


                    Session["Customer"] = cvm;

                   
                    db.Entry(c).State = EntityState.Modified;
                   
                    //db.SaveChanges();
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                //throw;
            }

            return RedirectToAction("Index", "PointRedeemHistory");
        }

    }
}