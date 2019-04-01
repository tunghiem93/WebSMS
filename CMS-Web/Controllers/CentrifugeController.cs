using CMS_DTO.CMSCentrifugo;
using CMS_DTO.CMSMarketing;
using CMS_DTO.CMSSims;
using CMS_Shared;
using CMS_Shared.CMSCategories;
using CMS_Shared.CMSMarketing;
using CMS_Shared.CMSSims;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Controllers
{
    public class CentrifugeController : Controller
    {
        public CMSMarketingFactory _fac = null;
        public CentrifugeController()
        {
            _fac = new CMSMarketingFactory();
        }
        // GET: SynListSim
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult subscribe(string client, List<string> channels)
        {
            Dictionary< string, List<CentrifugoChannelModels>> result = new Dictionary<string, List<CentrifugoChannelModels>>();
            List<CentrifugoChannelModels> listChannel = new List<CentrifugoChannelModels>();
           foreach(string channel in channels)
            {
                var dataToken = new Dictionary<string, object>();
                dataToken.Add("client", client);
                dataToken.Add("channel", channel);
                string strToken = Commons.GenerateToken(dataToken);
                CentrifugoChannelModels model = new CentrifugoChannelModels() {
                    channel = channel,
                    token = strToken
                };
                listChannel.Add(model);
            }
            result.Add("channels", listChannel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateStatusSim(List<SimCentrifugoModels> model)
        {
            CMSSimsFactory _CMSSimsFactory = new CMSSimsFactory();
            int status = 0;
            bool updateResult = false;
            Dictionary<string, string> result = new Dictionary<string, string>();
            if (model == null)
            {
                result.Add("StatusCode", "500");
                result.Add("Message", "Result is empty!");
            }
            else
            {

                foreach (SimCentrifugoModels item in model)
                {
                    if (!string.IsNullOrEmpty(item.name))
                    {
                        if (!string.IsNullOrEmpty(item.status))
                        {
                            switch (item.status.Trim().ToLower())
                            {
                                case "đang kết nối":
                                    status = (int)Commons.SimStatus.WaitingConnect;
                                    break;
                                case "lỗi kết nối":
                                    status = (int)Commons.SimStatus.ConnectFail;
                                    break;
                                default:
                                    status = (int)Commons.SimStatus.Connected;
                                    break;
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(item.error))
                            {
                                status = (int)Commons.SimStatus.ConnectFail;
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(item.operatorName))
                                {
                                    status = (int)Commons.SimStatus.Connected;
                                }
                                else
                                {
                                    status = (int)Commons.SimStatus.WaitingConnect;
                                }
                            }
                        }

                        updateResult = _CMSSimsFactory.UpdateStatusSim(item.name, status, item.operatorName);
                    }
                }

                
                if (updateResult)
                {
                    result.Add("StatusCode", "200");
                    result.Add("Message", "Success");
                }
                else
                {
                    result.Add("StatusCode", "500");
                    result.Add("Message", "Server update Error");
                }
            }
            
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateSMSStatus(string id, bool success)
        {

            string msg = "";
            int status = (int)Commons.SMSStatus.Fail;
            if (success)
            {
                status = (int)Commons.SMSStatus.Success;
            }
            bool updateResult = _fac.UpdateSMSStatus(id, status, ref msg);
            
            Dictionary<string, string> result = new Dictionary<string, string>();
            if (updateResult && msg.Equals(""))
            {
                result.Add("StatusCode", "200");
                result.Add("Message", "Success");
            }
            else
            {
                result.Add("StatusCode", "500");
                result.Add("Message", "Server update Error");
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}