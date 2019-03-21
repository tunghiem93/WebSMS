using CMS_DTO;
using CMS_DTO.CMSEmployee;
using CMS_Shared;
using CMS_Shared.CMSMarketing;
using CMS_Shared.Utilities;
using CMS_Web.Web.App_Start;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Areas.Admin.Controllers
{
    [NuAuth]
    public class CMSMessageOTPController : BaseController
    {
        private CMSMarketingFactory _factory;
        public CMSMessageOTPController()
        {
            _factory = new CMSMarketingFactory();
        }
        // GET: Admin/CMSCategories
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadGrid()
        {
            var model = _factory.GetList((int)Commons.SMSType.OTP);
            return PartialView("_ListData", model);
        }
    }
}