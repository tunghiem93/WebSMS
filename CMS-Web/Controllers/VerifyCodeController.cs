using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMS_DTO.CMSCustomer;
using CMS_Shared;
using CMS_Shared.CMSCustomers;
using CMS_Shared.Utilities;

namespace CMS_Web.Controllers
{
    public class VerifyCodeController : BasesController
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
                TempData.Keep();
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
        public ActionResult Index(CustomerActiveCode model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return View(model);
                }
                var email = TempData["Email_Register"].ToString();
                TempData.Keep();
                var result = fac.VerifyCode(model.Code, email);
                if (result)
                {
                    return RedirectToAction("Index", "Login");
                } else
                {
                    ModelState.AddModelError("Code", "Code InCorrect");
                    return View(model);
                }
            }
            catch (Exception ex) { }
            return View();
        }
    }
}