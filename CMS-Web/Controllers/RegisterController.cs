﻿using System;
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
    public class RegisterController : BasesController
    {
        private readonly CMSCustomersFactory fac;
        public RegisterController()
        {
            fac = new CMSCustomersFactory();
        }
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(CustomerModels model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return View(model);
                }
                model.Status = (int)Commons.CustomerStatus.Watting;
                model.Password = CommonHelper.Encrypt(model.Password);
                model.Password2 = CommonHelper.Encrypt(model.Password2);
                string msg = "";
                string ActiveCode = "";
                string key = "";
                var result = fac.InsertOrUpdate(model,ref ActiveCode, ref msg, ref key);
                if (result)
                {
                    // send mail active code
                    string body = "<div>"+ActiveCode+" </div>";
                    var isOk = CommonHelper.SendContentMail(model.Email, body, null, "ACTIVE CODE", null);
                    TempData["Email_Register"] = model.Email;
                    return RedirectToAction("Index", "VerifyCode");
                } else
                {
                    if (!string.IsNullOrEmpty(key))
                    {
                        ModelState.AddModelError(key, msg);
                        return View(model);
                    }
                }
            }
            catch(Exception ex)
            {
              
            }
            return View("Index");
        }
    }
}