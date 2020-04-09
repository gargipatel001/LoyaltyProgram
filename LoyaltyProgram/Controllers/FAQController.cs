using LoyaltyProgram.DAL;
using LoyaltyProgram.Models;
using LoyaltyProgram.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoyaltyProgram.Controllers
{
    public class FAQController : Controller
    {
        // GET: FAQ
        private LoyaltyProgramContext db = new LoyaltyProgramContext();
        public ActionResult Index()
        {
            List<FAQ> faqs = new List<FAQ>();
           
            List<FAQViewModel> faqsvM = new List<FAQViewModel>();
            faqs = db.FAQs.Where(_=>_.IsActive == true).ToList();
            if (faqs!=null && faqs.Count > 0)
            {
                foreach (var item in faqs)
                {
                    FAQViewModel faqvM = new FAQViewModel();
                    faqvM.FAQId = item.FAQId;
                    faqvM.Question = item.Question;
                    faqvM.Answer = item.Answer;
                    faqsvM.Add(faqvM);
                  
                }

            }

            return View(faqsvM);
        }
    }
}