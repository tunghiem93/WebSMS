using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS_Shared.CMSCustomers;
using CMS_DTO.CMSCustomer;
using CMS_Shared.Utilities;
using CMS_Web.Web.App_Start;

namespace CMS_Web.Controllers
{
    [NuWebAuth]
    public class ChangePasswordController : Controller
    {
        private readonly CMSCustomersFactory fac;
        public ChangePasswordController()
        {
            fac = new CMSCustomersFactory();
        }
        // GET: ChangePassword
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(CustomerChangePasswordModel model)
        {
            try
            {
                var User = Session["UserC"] as CustomerModels;
                if (User == null)
                    return RedirectToAction("Index", "Home");
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var msg = "";
                model.LastPassword = CommonHelper.Encrypt(model.LastPassword);
                model.Password = CommonHelper.Encrypt(model.Password);
                model.Password2 = CommonHelper.Encrypt(model.Password2);
                model.Email = User.Email;
                var result = fac.ChangePassword(model, ref msg);
                if(result)
                {
                    model.Status = true;
                }else
                {
                    ModelState.AddModelError("LastPassword", msg);
                }
            } catch(Exception ex) { }
            return View(model);
        }
    }
}