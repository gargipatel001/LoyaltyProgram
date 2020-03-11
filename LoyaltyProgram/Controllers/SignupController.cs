using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoyaltyProgram.DAL;
using LoyaltyProgram.ViewModels;
using LoyaltyProgram.Models;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace LoyaltyProgram.Controllers
{
    public class SignupController : Controller
    {
        // GET: Signup
        private LoyaltyProgramContext db = new LoyaltyProgramContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCustomer(CustomerViewModel customerViewModel)
        {
            try
            {
                if (customerViewModel!=null)
                {
                    Customer customer = new Customer();
                    customer.CustomerFirstName = customerViewModel.CustomerFirstName;
                    customer.CustomerLastName = customerViewModel.CustomerLastName;
                    customer.CustomerGender = customerViewModel.CustomerGender;
                    customer.CustomerDOB = customerViewModel.CustomerDOB;
                    customer.CustomerAddress = customerViewModel.CustomerAddress;
                    customer.CustomerCity = customerViewModel.CustomerCity;
                    customer.CustomerProvince = customerViewModel.CustomerProvince;
                    customer.CustomerPostalCode = customerViewModel.CustomerPostalCode;
                    customer.CustomerCardNo = customerViewModel.CustomerCardNo;
                    customer.CustomerPassword = customerViewModel.CustomerPassword;
                    customer.CustomerEmail = customerViewModel.CustomerEmail;
                    customer.CustomerPhoneNumber = customerViewModel.CustomerPhoneNumber;
                    customer.RoleId = getRoleIdForCustomerRole();
                    customer.CreatedOn = DateTime.Now;
                    customer.CustomerLoyaltyPoints = 10000;
                    customer.LevelId = getCustomerLevelId(customerViewModel.CustomerLoyaltyPoints);
                   db.Customers.Add(customer);
                   db.SaveChanges();
                    sendMail(customerViewModel.CustomerEmail);
                    return RedirectToAction("Index", "Login");

                }

            }
            catch (Exception ex)
            {

               
            }
            return View("Index");
        }
        public int getRoleIdForCustomerRole()
        {
            Roles role = new Roles();
            role = db.Roles.Where(_ => _.RoleName.Equals("Customer", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            return role.RoleId; 
             
        }
        public int getCustomerLevelId(double points)
        {
            int levelId = 0;
            List<CustomerLevel> customerLevels = new List<CustomerLevel>();
            customerLevels = db.CustomerLevels.ToList();
            foreach (CustomerLevel clevel in customerLevels)
            {
                if (points>=clevel.PointsRangeFrom && points<=clevel.PointsRangeTo)
                {
                    levelId = clevel.LevelId;
                    break;
                }
            }
            return levelId;

        }
        public void sendMail(String username)
        {
            string emailSender = ConfigurationManager.AppSettings["emailsender"].ToString();
            string emailSenderPassword = ConfigurationManager.AppSettings["password"].ToString();
            string emailSenderHost = ConfigurationManager.AppSettings["smtp"].ToString();
            int emailSenderPort = Convert.ToInt16(ConfigurationManager.AppSettings["portnumber"]);
            Boolean emailIsSSL = Convert.ToBoolean(ConfigurationManager.AppSettings["IsSSL"]);
            //string filePath = "~/Templates/SignUpMail.html";
            string path = Server.MapPath("~/Templates/SignUpMail.html");

            StreamReader str = new StreamReader(path);
            string MailText = str.ReadToEnd();
            str.Close();

            //Repalce [newusername] = signup user name   
            MailText = MailText.Replace("[newusername]", username);


            string subject = "Welcome to Loyalty Program";
            MailMessage _mailmsg = new MailMessage();

            //Make TRUE because our body text is html  
            _mailmsg.IsBodyHtml = true;

            //Set From Email ID  
            _mailmsg.From = new MailAddress(emailSender);

            //Set To Email ID  
            _mailmsg.To.Add(username);

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
        public ActionResult checkMail(string email)
        {
            string message = "";
            Customer customer = new Customer();
            customer = db.Customers.Where(_ => _.CustomerEmail == email).FirstOrDefault();
            if (customer!=null && customer.CustomerId > 0)
            {
                message = "Email Id already exists";
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }
    }
}