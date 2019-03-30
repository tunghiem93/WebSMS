using CMS_DTO.CMSCustomer;
using CMS_DTO.CMSProfile;
using CMS_Shared;
using CMS_Shared.CMSDepositTransaction;
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
        private CMSDepositTransactionFactory _fac;
        public ProfileController()
        {
            _factory = new CMSMarketingFactory();
            _fac = new CMSDepositTransactionFactory();
        }
        // GET: Profile
        public ActionResult Index()
        {
            var model = new CMS_ProfileModels();
            model.OTPs = _factory.GetList((int)Commons.SMSType.OTP);
            model.Marketings = _factory.GetList((int)Commons.SMSType.Marketing);
            var Customer = Session["UserC"] as CustomerModels;
            model.Transactions = _fac.GetListDepositTransaction(Customer.ID);
            return View(model);
        }
    }
}