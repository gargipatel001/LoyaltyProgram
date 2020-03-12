using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LoyaltyProgram.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace LoyaltyProgram.Controllers
{
    public class PointRedeemHistoryController : Controller
    {
        // GET: PointRedeem
        private LoyaltyProgramContext db = new LoyaltyProgramContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult bindPromotionRedeemHistory([DataSourceRequest]DataSourceRequest request)
        {
            try
            {
                var data = db.PointRedeemHistories.Include(_ => _.Promotion).ToList();
                return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json("", JsonRequestBehavior.AllowGet);
            }
           
        }
    }
}