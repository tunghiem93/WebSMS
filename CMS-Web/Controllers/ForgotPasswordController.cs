using CMS_DTO.CMSCustomer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS_Shared.CMSCustomers;
using CMS_Shared.Utilities;
namespace CMS_Web.Controllers
{
    public class ForgotPasswordController : Controller
    {
        private CMSCustomersFactory fac;
        public ForgotPasswordController()
        {
            fac = new CMSCustomersFactory();
        }
        // GET: ForgotPassword
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(CustomerForgotPassword model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var newPassword = CommonHelper.GeneralPassword();
            var msg = "";
            var result = fac.ForgotPassword(model.Email, CommonHelper.Encrypt(newPassword), ref msg);
            if (result)
            {
                TempData["reset_password"] = true;
                // send mail active code
                string body = "<div>" + newPassword + " </div>";
                var isOk = CommonHelper.SendContentMail(model.Email, body, null, "[New Password]", null, null);
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ModelState.AddModelError("Email", msg);
                return View(model);
            }
        }
    }
}