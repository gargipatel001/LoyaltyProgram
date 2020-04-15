using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LoyaltyProgram.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using LoyaltyProgram.ViewModels;

namespace LoyaltyProgram.Controllers
{
    public class PointRedeemHistoryController : Controller
    {
        // GET: PointRedeem
        private LoyaltyProgramContext db = new LoyaltyProgramContext();
        public ActionResult Index()
        {
            if (Session["Customer"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        // Get PointRedeemtionHistory by customer's ID
        public ActionResult bindPromotionRedeemHistory([DataSourceRequest]DataSourceRequest request)
        {
            try
            {

                if (Session["Customer"] != null)
                {
                    CustomerViewModel cvm = new CustomerViewModel();
                    cvm = (CustomerViewModel)Session["Customer"];
                    var data = db.PointRedeemHistories.Include(_ => _.Promotion).Where(_=>_.CustomerId==cvm.CustomerId).ToList();
                    return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                }
                else {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
              
            }
            catch (Exception ex)
            {

                return Json("", JsonRequestBehavior.AllowGet);
            }
           
        }
    }
}