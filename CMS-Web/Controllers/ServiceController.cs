using CMS_DTO.CMSCentrifugo;
using CMS_DTO.CMSMarketing;
using CMS_DTO.CMSSimOperator;
using CMS_Shared;
using CMS_Shared.CMSCategories;
using CMS_Shared.CMSGSM;
using CMS_Shared.CMSMarketing;
using CMS_Shared.CMSSimOperator;
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
        public CMSSimOperatorFactory _simOperator = null;
        public CMSGSMFactory _gsmFac = null;
        public ServiceController()
        {
            _fac = new CMSMarketingFactory();
            _gsmFac = new CMSGSMFactory();
            _simOperator = new CMSSimOperatorFactory();
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
                List<CMS_SimOperatorModels> listOp = _simOperator.GetList();
                List<string> listGSMName = _gsmFac.GetList().Where(x => x.IsActive).Select(x => x.GSMName).ToList();
                var random = new Random();
                int index = random.Next(listGSMName.Count);
                string GSMName = listGSMName[index];
                string channelName = Commons.centriSMSChannel + (GSMName == null ? "" : "#" + GSMName);
                decimal rate = _fac.GetSMSRate((int)Commons.ConfigType.SMSOTP);
                string strSMSConvert = Commons.ConvertUnicodeToWithoutAccent(model.Content);
                int smsFee = strSMSConvert.Length / 80;
                string operatorName = _fac.GetOperatorName(model.Phone, listOp);
                CMS_MarketingModels importModel = new CMS_MarketingModels() {
                    OperatorName = operatorName,
                    SendFrom = GSMName,
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
                if (string.IsNullOrEmpty(msg))
                {
                    List<MessageSMSModels> listData = new List<MessageSMSModels>();
                    MessageSMSModels modelCentri = new MessageSMSModels()
                    {
                        id = importModel.Id,
                        operatorName = operatorName,
                        phone = importModel.SendTo,
                        text = importModel.SMSContent
                    };
                    listData.Add(modelCentri);
                    bool isRunSuccess = true;
                    if (listData.Count > 0)
                    {
                        MainSMSModels mod = new MainSMSModels()
                        {
                            messages = listData,
                            callbackURL = Url.Action("UpdateSMSStatus", "Centrifuge", null, HttpContext.Request.Url.Scheme),
                            delay = 10
                        };
                        isRunSuccess = CMSCentrifugoFactory.SendSMSToCentri("publish", Commons.centriURL, Commons.centriApiKey, channelName, mod);
                    }
                    if (!isRunSuccess)
                    {
                        _fac.UpdateSMSStatus(importModel.Id, (int)Commons.SMSStatus.Fail, ref msg);
                        msg = "Service is fail!";
                        
                    }
                }
                

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