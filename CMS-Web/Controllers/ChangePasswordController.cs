using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS_Shared.CMSCustomers;
using CMS_DTO.CMSCustomer;
namespace CMS_Web.Controllers
{
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
                var User = Session["User"] as CustomerModels;
                if (User == null)
                    return RedirectToAction("Index", "Home");
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var msg = "";
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