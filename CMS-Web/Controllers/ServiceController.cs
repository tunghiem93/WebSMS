using CMS_DTO.CMSMarketing;
using CMS_Shared;
using CMS_Shared.CMSMarketing;
using CMS_Web.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Controllers
{
    [NuWebAuth]
    public class ServiceController : BasesController
    {
        public CMSMarketingFactory _fac = null;
        public ServiceController()
        {
            _fac = new CMSMarketingFactory();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(CMS_TestServiceModels model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return View(model);
                }
                decimal rate = _fac.GetSMSRate((int)Commons.ConfigType.SMSOTP);
                string strSMSConvert = Commons.ConvertUnicodeToWithoutAccent(model.Content);
                int smsFee = strSMSConvert.Length / 80;
                CMS_MarketingModels importModel = new CMS_MarketingModels() {
                    OperatorName = "",
                    SendFrom = "",
                    CreatedBy = CurrentUser.ID,
                    CustomerId = CurrentUser.ID,
                    CustomerName = string.Format("{0} ({1})", CurrentUser.Name, CurrentUser.Phone),
                    RunTime = 60,
                    SendTo = model.Phone,
                    SMSContent = model.Content,
                    SMSType = (int)Commons.SMSType.OTP,
                    Status = (int)Commons.SMSStatus.Sent,
                    TimeInput = DateTime.Now,
                    UpdatedBy = CurrentUser.ID,
                    SMSRate = rate,
                    SMSPrice = (smsFee + 1) * rate
                };
                string msg = "";

                var result = _fac.InsertFromExcel(importModel, ref msg);

                if (msg.Equals(""))
                {
                    ViewData["SuccessMessage"] = "Service is successfully!";
                    return View(new CMS_TestServiceModels());
                }
                else
                {
                    ViewData["ErrorMessage"] = msg;
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error(ex);
                ViewData["ErrorMessage"] = ex.Message;
                return View(model);
            }
        }
    }
}