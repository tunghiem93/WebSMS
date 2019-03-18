using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS_Shared.CMSCustomers;
using CMS_DTO.CMSCustomer;
using System.Net;
using CMS_Shared.Utilities;
namespace CMS_Web.Controllers
{
    public class LoginController : Controller
    {
        private CMSCustomersFactory fac;
        public LoginController()
        {
            fac = new CMSCustomersFactory();
        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(CustomerLoginModels model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return View(model);
                }
                model.Password = CommonHelper.Decrypt(model.Password);
                var data = fac.CustomerLogin(model);
                if (data == null)
                {
                    ModelState.AddModelError("UserName", "Your username or password not correct");
                    return View(model);
                }
                Session.Add("User", data);
                
            } catch(Exception ex) { }
            return RedirectToAction("Index", "Home");
        }
    }
}