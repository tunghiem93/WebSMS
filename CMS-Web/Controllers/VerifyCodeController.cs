using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS_Shared;
using CMS_Shared.CMSCustomers;
using CMS_Shared.Utilities;

namespace CMS_Web.Controllers
{
    public class VerifyCodeController : Controller
    {
        private CMSCustomersFactory fac;
        public VerifyCodeController()
        {
            fac = new CMSCustomersFactory();
        }
        // GET: VerifiCode
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ResendCode()
        {
            try
            {
                var email = TempData["Email_Register"].ToString();
                string activeCode = "";
                var result = fac.ResendCode(email, ref activeCode);
                if (result)
                {
                    // send mail active code
                    string body = "<div>" + activeCode + " </div>";
                    var isOk = CommonHelper.SendContentMail(email, body, null, "ACTIVE CODE", null);
                }
            } catch(Exception) { }
            var obj = new
            {
                msg = "Resend successfull"
            };
            return Json(obj);
        }

        [HttpPost]
        public ActionResult VerifyCode (string code)
        {
            try
            {
                var email = TempData["Email_Register"].ToString();
                var result = fac.VerifyCode(code, email);
                if (result)
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            catch (Exception) { }
            var obj = new
            {
                msg = "Code not correct"
            };
            return Json(obj);
        }
    }
}