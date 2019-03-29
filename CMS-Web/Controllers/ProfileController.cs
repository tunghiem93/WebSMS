using CMS_DTO.CMSProfile;
using CMS_Shared;
using CMS_Shared.CMSMarketing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Controllers
{
    public class ProfileController : Controller
    {
        private CMSMarketingFactory _factory;
        public ProfileController()
        {
            _factory = new CMSMarketingFactory();
        }
        // GET: Profile
        public ActionResult Index()
        {
            var model = new CMS_ProfileModels();
            model.OTPs = _factory.GetList((int)Commons.SMSType.OTP);
            model.Marketings = _factory.GetList((int)Commons.SMSType.Marketing);
            return View(model);
        }
    }
}