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
        public DepositPackageController()
        {
            fac = new CMSDepositPackageFactory();
        }
        // GET: DepositPackage
        public ActionResult Index()
        {
            var model = fac.GetList();
            return View(model);
        }
    }
}