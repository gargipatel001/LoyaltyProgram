using LoyaltyProgram.DAL;
using LoyaltyProgram.Models;
using LoyaltyProgram.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace LoyaltyProgram.Controllers
{
    
    public class ForgetPasswordController : Controller
    {
        // GET: ForgetPassword
        private LoyaltyProgramContext db = new LoyaltyProgramContext();
        public ActionResult Index()
        {
            TempData["Message"] = "";
            //TempData["Success"] = "";
            return View();
        }
        public ActionResult ForgetPassword(LoginViewModel loginViewModel)
        {
            String message = "";
            try
            {
                if (loginViewModel != null)
                {
                    Customer customer = new Customer();
                    customer = db.Customers.Where(_ => _.CustomerEmail == loginViewModel.Email).FirstOrDefault();
                    if (customer != null && customer.CustomerId > 0)
                    {
                        customer.CustomerPassword = loginViewModel.Password;
                        db.Entry(customer).State = EntityState.Modified;
                        db.SaveChanges();
                        sendMail(loginViewModel.Email);
                        //TempData["Success"] = "Success";
                    }
                    else
                    {
                        message = "Invalid Email Id";
                        //TempData["Success"] = "Fail";
                    }
                }
            }
            catch (Exception ex)
            {

                //throw;
            }
          
            return RedirectToAction("Index");
        }

        private void sendMail(string email)
        {
            try
            {
                string emailSender = ConfigurationManager.AppSettings["emailsender"].ToString();
                string emailSenderPassword = ConfigurationManager.AppSettings["password"].ToString();
                string emailSenderHost = ConfigurationManager.AppSettings["smtp"].ToString();
                int emailSenderPort = Convert.ToInt16(ConfigurationManager.AppSettings["portnumber"]);
                Boolean emailIsSSL = Convert.ToBoolean(ConfigurationManager.AppSettings["IsSSL"]);
                //string filePath = "~/Templates/SignUpMail.html";
                string path = Server.MapPath("~/Templates/ForgetPassword.html");

                StreamReader str = new StreamReader(path);
                string MailText = str.ReadToEnd();
                str.Close();

                //Repalce [newusername] = signup user name   
                MailText = MailText.Replace("[newusername]", email);


                string subject = "Password Reset";
                MailMessage _mailmsg = new MailMessage();

                //Make TRUE because our body text is html  
                _mailmsg.IsBodyHtml = true;

                //Set From Email ID  
                _mailmsg.From = new MailAddress(emailSender);

                //Set To Email ID  
                _mailmsg.To.Add(email);

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

        public ActionResult checkMail(string email)
        {
            string message = "";
            try
            {

                Customer customer = new Customer();
                customer = db.Customers.Where(_ => _.CustomerEmail == email).FirstOrDefault();
                if (customer == null || customer.CustomerId == 0)
                {
                    message = "Invalid Email Id";
                }

            }
            catch (Exception)
            {

                //return message;
            }

            return Json(message, JsonRequestBehavior.AllowGet);
        }

    }
}