using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LoyaltyProgram.DAL;
using LoyaltyProgram.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoyaltyProgram.Controllers
{
    public class PartnerController : Controller
    {
        private LoyaltyProgramContext db = new LoyaltyProgramContext();

        // GET: Partner
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
          
            try
            {
                
               var data = db.Partners.ToList();
                return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
                //throw;
            }
           
           
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, Partner partner)
        {
            if (ModelState.IsValid)
            {
                if (TempData["File"]!=null)
                {
                    string fileName = "";
                    //var img = Convert.ToBase64String((byte[])TempData["File"]);
                    //Byte[] file = (byte[])TempData["File"]);
                     HttpPostedFileWrapper file = TempData["File"] as HttpPostedFileWrapper;
                    if (file!=null)
                    {
                        string path = Server.MapPath("~/Content/Images/PromotionPartners/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        fileName = file.FileName;
                        file.SaveAs(path + Path.GetFileName(file.FileName));
                    }
                    partner.PartnerLogo = "Images/PromotionPartners/" + fileName;

                }
               
                db.Partners.Add(partner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, Partner partner)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (TempData["File"]!=null)
                    {
                        string fileName = "";
                        string fullPath = Request.MapPath("~/Content/" + partner.PartnerLogo);
                       
                        
                        //var img = Convert.ToBase64String((byte[])TempData["File"]);
                        //Byte[] file = (byte[])TempData["File"]);
                        HttpPostedFileWrapper file = TempData["File"] as HttpPostedFileWrapper;
                        if (file != null)
                        {
                            string path = Server.MapPath("~/Content/Images/PromotionPartners/");
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            fileName = file.FileName;
                            file.SaveAs(path + Path.GetFileName(file.FileName));
                            if (System.IO.File.Exists(fullPath))
                            {
                                System.IO.File.Delete(fullPath);
                            }
                            TempData["File"] = null;
                        }
                        partner.PartnerLogo = "Images/PromotionPartners/" + fileName;


                    }
                    db.Entry(partner).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {

                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult CheckPartner(string partnerName)
        {
            try
            {
                int Count = db.Partners.Where(_ => _.PartnerName == partnerName).Count();

                return Json(Count, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult Save(IEnumerable<HttpPostedFileBase> files)
        {
            // The Name of the Upload component is "files"
            if (files != null)
            {
                foreach (var file in files)
                {
                    TempData["File"] = file;
                    // Some browsers send file names with full path.
                    // We are only interested in the file name.
                   // var fileName = Path.GetFileName(file.FileName);
                    //var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);

                    // The files are not actually saved in this demo
                    // file.SaveAs(physicalPath);
                }
            }

            // Return an empty string to signify success
            return Content("");
        }
        public ActionResult Remove(string[] fileNames)
        {
            // The parameter of the Remove action must be called "fileNames"

            if (fileNames != null)
            {
                if (TempData["File"]!=null)
                {
                    TempData["File"] = null;
                }
                //foreach (var fullName in fileNames)
                //{
                //    var fileName = Path.GetFileName(fullName);
                //    var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);

                //    // TODO: Verify user permissions

                //    if (System.IO.File.Exists(physicalPath))
                //    {
                //        // The files are not actually removed in this demo
                //        // System.IO.File.Delete(physicalPath);
                //    }
                //}
            }

            // Return an empty string to signify success
            return Content("");
        }
    }
}