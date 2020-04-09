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
using System.Configuration;
using System.Net.Mail;
using System.Net;

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
            promotions = db.Promotions.Include(_ => _.Partner).Include(_ => _.PromotionType).Where(_=>_.IsActive == true && _.PromotionStartDate <= DateTime.Now && _.PromotionEndDate >= DateTime.Now).ToList();
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
                    Customer c = new Customer();
                    if (cvm.CustomerLoyaltyPoints > 0)
                    {

                        pointsRedeem.CustomerId = cvm.CustomerId;
                        pointsRedeem.PointsRedeemedOn = DateTime.Now;
                        pointsRedeem.PromotionId = p.PromotionId;
                        pointsRedeem.PointsRedeemed = p.PromotionPoints;
                        db.PointRedeemHistories.Add(pointsRedeem);

                        // pointsleft = cvm.CustomerLoyaltyPoints - p.PromotionPoints;
                        cvm.CustomerLoyaltyPoints = cvm.CustomerLoyaltyPoints - p.PromotionPoints;


                        c = db.Customers.Where(_ => _.CustomerId == cvm.CustomerId).FirstOrDefault();
                        c.CustomerLoyaltyPoints = cvm.CustomerLoyaltyPoints;


                        Session["Customer"] = cvm;


                        db.Entry(c).State = EntityState.Modified;

                        //db.SaveChanges();
                        db.SaveChanges();
                        sendConfirmationMail(cvm.CustomerEmail, p.PromotionName);
                    }

                 
                }
            }
            catch (Exception ex)
            {

                //throw;
            }

            return RedirectToAction("Index", "PointRedeemHistory");
        }

        public void sendConfirmationMail(string cMail, string promotion)
        {
            try
            {
                string couponCode = generateCouponCode();

                string emailSender = ConfigurationManager.AppSettings["emailsender"].ToString();
                string emailSenderPassword = ConfigurationManager.AppSettings["password"].ToString();
                string emailSenderHost = ConfigurationManager.AppSettings["smtp"].ToString();
                int emailSenderPort = Convert.ToInt16(ConfigurationManager.AppSettings["portnumber"]);
                Boolean emailIsSSL = Convert.ToBoolean(ConfigurationManager.AppSettings["IsSSL"]);
                //string filePath = "~/Templates/SignUpMail.html";
                string path = Server.MapPath("~/Templates/RedeemPointsConfirmation.html");

                StreamReader str = new StreamReader(path);
                string MailText = str.ReadToEnd();
                str.Close();

                //Repalce [newusername] = signup user name   
                MailText = MailText.Replace("[newusername]", cMail);
                MailText = MailText.Replace("[promotion]", promotion);
                MailText = MailText.Replace("[couponCode]", couponCode);


                string subject = "Purchase Confirmation";
                MailMessage _mailmsg = new MailMessage();

                //Make TRUE because our body text is html  
                _mailmsg.IsBodyHtml = true;

                //Set From Email ID  
                _mailmsg.From = new MailAddress(emailSender);

                //Set To Email ID  
                _mailmsg.To.Add(cMail);

                //Set Subject  
                _mailmsg.Subject = subject;

                //Set Body Text of Email   
                _mailmsg.Body = MailText;


                //Now set your SMTP   
                SmtpClient _smtp = new SmtpClient();

                //Set HOST server SMTP detail  
                _smtp.Host = emailSenderHost;

                //Set PORT number of SMTP  
                _smtp.Port = emailSenderPort;

                //Set SSL --> True / False  
                _smtp.EnableSsl = emailIsSSL;

                //Set Sender UserEmailID, Password  
                NetworkCredential _network = new NetworkCredential(emailSender, emailSenderPassword);
                _smtp.Credentials = _network;

                //Send Method will send your MailMessage create above.  
                _smtp.Send(_mailmsg);
            }
            catch (Exception ex)
            {


            }

        }
        public string generateCouponCode()
        {
            //string couponCode = "";
            int allowedLength = 8;
            string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
            char[] chars = new char[allowedLength];
            try
            {
                
                Random randNum = new Random();
               
                int allowedCharCount = _allowedChars.Length;
                for (int i = 0; i < allowedLength; i++)
                {
                    chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
                }
               
            }
            catch (Exception)
            {

               // throw;
            }
            return new string(chars);
        }

    }
}