using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using LoyaltyProgram.ViewModels;

namespace LoyaltyProgram.Controllers
{
    public class ContactUsController : Controller
    {
        // GET: ContactUs
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult sendMail(ContactUsViewModel contactusViewModel)
        {
            sendMailToCompany(contactusViewModel);

            sendMailToCustomer(contactusViewModel);


            return RedirectToAction("Index");
        }
        private void sendMailToCustomer(ContactUsViewModel c)
        {
            try
            {

                string emailSender = ConfigurationManager.AppSettings["emailsender"].ToString();
                string emailSenderPassword = ConfigurationManager.AppSettings["password"].ToString();
                string emailSenderHost = ConfigurationManager.AppSettings["smtp"].ToString();
                int emailSenderPort = Convert.ToInt16(ConfigurationManager.AppSettings["portnumber"]);
                Boolean emailIsSSL = Convert.ToBoolean(ConfigurationManager.AppSettings["IsSSL"]);
                //string filePath = "~/Templates/SignUpMail.html";
                string path = Server.MapPath("~/Templates/CustomerContactUsMail.html");

                StreamReader str = new StreamReader(path);
                string MailText = str.ReadToEnd();
                str.Close();

                //Repalce [newusername] = signup user name   
                MailText = MailText.Replace("[newusername]", c.CustomerEmail);



                string subject = "Thankyou for Contacting";
                MailMessage _mailmsg = new MailMessage();

                //Make TRUE because our body text is html  
                _mailmsg.IsBodyHtml = true;

                //Set From Email ID  
                _mailmsg.From = new MailAddress(emailSender);

                //Set To Email ID  
                _mailmsg.To.Add(c.CustomerEmail);

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
                //return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message("Sorry we are facing Problem here" + ex);

            }
        }

        private void sendMailToCompany(ContactUsViewModel contactusViewModel)
        {
            try
            {

                string emailSender = ConfigurationManager.AppSettings["emailsender"].ToString();
                string emailSenderPassword = ConfigurationManager.AppSettings["password"].ToString();
                string emailSenderHost = ConfigurationManager.AppSettings["smtp"].ToString();
                int emailSenderPort = Convert.ToInt16(ConfigurationManager.AppSettings["portnumber"]);
                Boolean emailIsSSL = Convert.ToBoolean(ConfigurationManager.AppSettings["IsSSL"]);
                //string filePath = "~/Templates/SignUpMail.html";
                string path = Server.MapPath("~/Templates/ContactUsMail.html");

                StreamReader str = new StreamReader(path);
                string MailText = str.ReadToEnd();
                str.Close();

                //Repalce [newusername] = signup user name   
                MailText = MailText.Replace("[FirstName]", contactusViewModel.CustomerFirstName).Replace("[LastName]", contactusViewModel.CustomerLastName)
                .Replace("[username]", contactusViewModel.CustomerEmail).Replace("[cardNo]", contactusViewModel.CustomerCardNo).Replace("[comment]",contactusViewModel.Comment);



                string subject = " Customer Contact Mail";
                MailMessage _mailmsg = new MailMessage();

                //Make TRUE because our body text is html  
                _mailmsg.IsBodyHtml = true;

                //Set From Email ID  
                _mailmsg.From = new MailAddress(emailSender);

                //Set To Email ID  
                _mailmsg.To.Add(emailSender);

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
                //return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message("Sorry we are facing Problem here" + ex);

            }

        }
    }


}