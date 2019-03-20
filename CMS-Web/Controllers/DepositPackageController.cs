using CMS_DTO;
using CMS_Shared.CMSEmployees;
using CMS_Web.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Controllers
{
    [NuWebAuth]
    public class DepositPackageController : Controller
    {
        private readonly CMSDepositPackageFactory fac;
        private readonly CMSPaymentMethodFactory facP;
        public DepositPackageController()
        {
            fac = new CMSDepositPackageFactory();
            facP = new CMSPaymentMethodFactory();
        }
        // GET: DepositPackage
        public ActionResult Index()
        {
            var model = fac.GetList();
            ViewBag.Payment = facP.GetList();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddDepositPackage(CMS_DepositPackageModel item)
        {
            return View();
        }
    }
}