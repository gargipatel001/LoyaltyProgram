using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LoyaltyProgram.DAL;
using LoyaltyProgram.Models;

namespace LoyaltyProgram.Controllers
{
    public class RolesController : Controller
    {
        private LoyaltyProgramContext db = new LoyaltyProgramContext();

        // GET: Roles
        public ActionResult Index()
        {
            if (Session["User"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        // Get All Roles
        public ActionResult Select([DataSourceRequest]DataSourceRequest request)
        {
            var data = db.Roles.ToList();
            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        // Edit Roles
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roles roles = db.Roles.Find(id);
            if (roles == null)
            {
                return HttpNotFound();
            }
            return View(roles);
        }
        // Update Roles

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, Roles roles)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(roles).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {

                }
            }
            return RedirectToAction("Index");
        }
        // Create New Role
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, Roles roles)
        {
            if (ModelState.IsValid)
            {
                db.Roles.Add(roles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }


        public ActionResult CheckRole(string roleName)
        {
            try
            {
                int Count = db.Roles.Where(_ => _.RoleName == roleName).Count();

                return Json(Count, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }
    }
}
