using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS_Shared;
using CMS_Shared.CMSCustomers;

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

        public JsonResult VerifyCode (string code)
        {
            try
            {
                var email = TempData["Email_Register"].ToString();
                var result = fac.VerifyCode(code, email);
            }
            catch (Exception) { }
        }
    }
}