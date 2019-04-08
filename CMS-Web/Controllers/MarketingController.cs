using ClosedXML.Excel;
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
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace CMS_Web.Controllers
{
    [NuWebAuth]
    public class MarketingController : BasesController
    {
        public CMSMarketingFactory _fac = null;
        public CMSSimOperatorFactory _simOperator = null;
        public CMSGSMFactory _gsmFac = null;
        
        public MarketingController()
        {
            _fac = new CMSMarketingFactory();
            _gsmFac = new CMSGSMFactory();
            _simOperator = new CMSSimOperatorFactory();
        }
        // GET: Marketing
        public ActionResult Index()
        {
            if (TempData["ErrorMessage"] != null)
            {
                ViewData["ErrorMessage"] = TempData["ErrorMessage"].ToString();
            }
            if (TempData["SuccessMessage"] != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"].ToString();
            }
            if(TempData["DataReturnError"] != null)
            {
                CMS_MarketingModels returnModel = new CMS_MarketingModels();
                returnModel.ListSMS = (List<CMS_MarketingModels>)TempData["DataReturnError"];
                return View(returnModel);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(CMS_MarketingModels model)
        {
            try
            {
                if (model.ExcelUpload == null)
                {
                    ViewData["ErrorMessage"] = "Please choose file to import!";
                    return View();
                }
                string fileName = Path.GetFileName(model.ExcelUpload.FileName);
                string filePath = string.Format("{0}/{1}", System.Web.HttpContext.Current.Server.MapPath("~/Uploads"), fileName);

                CMS_MarketingModels importModel = new CMS_MarketingModels();
                string msg = "";

                //upload file to server
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);

                model.ExcelUpload.SaveAs(filePath);
                //Import data
                var result = _fac.Import(filePath, CurrentUser.UserId, CurrentUser.UserName, CurrentUser.Phone, ref msg);
                model.ListSMS = result;
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);

                if (msg.Equals(""))
                {
                    return View(model);
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
        [HttpPost]
        public ActionResult SendSMS(List<CMS_MarketingModels> model, int RunTime)
        {
            /*
             * ok 0.Check user login
            ok 1. Check total credit of customer
             ok 2. Check phone number get Operator
            ok 3. Insert to DB tru credit
             ok 4. send sms to Centri server
             */
            if(CurrentUser == null || string.IsNullOrEmpty(CurrentUser.UserId))
            {
                return RedirectToAction("Index","Login");
            }
            string msg = "";
            if (model != null)
            {
               
                decimal totalPrice = 0;
                totalPrice = model.Sum(x => x.SMSPrice);
                if(!_fac.CheckTotalCredit(CurrentUser.UserId, totalPrice))
                {
                    TempData["ErrorMessage"] = "Your credit is not enough to run this service!";
                    TempData["DataReturnError"] = model;
                    return RedirectToAction("Index");
                }
                else
                {
                    List<MessageSMSModels> listData = new List<MessageSMSModels>();
                    List<CMS_SimOperatorModels> listOp = _simOperator.GetList();
                    List<string> listGSMName = _gsmFac.GetList().Where(x=>x.IsActive).Select(x=>x.GSMName).ToList();
                    var random = new Random();
                    int index = random.Next(listGSMName.Count);
                    string GSMName = listGSMName[index];
                    string channelName = Commons.centriSMSChannel + (GSMName == null? "" : "#"+GSMName);
                    foreach (CMS_MarketingModels item in model)
                    {
                        string operatorName = _fac.GetOperatorName(item.SendTo, listOp);
                        item.OperatorName = operatorName;
                        item.RunTime = RunTime;
                        item.SendFrom = GSMName;
                        item.TimeInput = DateTime.Now;
                        item.UpdatedBy = CurrentUser.UserId;
                        item.CreatedBy = CurrentUser.UserId;
                        _fac.InsertFromExcel(item, ref msg);
                        MessageSMSModels modelCentri = new MessageSMSModels()
                        {
                            id = item.Id,
                            operatorName = operatorName,
                            phone = item.SendTo,
                            text = item.SMSContent
                        };
                        listData.Add(modelCentri);
                    }
                    bool isRunSuccess = true;
                    if(listData.Count > 0)
                    {
                        MainSMSModels mod = new MainSMSModels()
                        {
                            messages = listData,
                            callbackURL = Url.Action("UpdateSMSStatus", "Centrifuge",null, HttpContext.Request.Url.Scheme),
                            delay = RunTime
                        };
                        isRunSuccess = CMSCentrifugoFactory.SendSMSToCentri("publish", Commons.centriURL, Commons.centriApiKey, channelName, mod);
                    }
                    if (!isRunSuccess)
                    {
                        foreach (MessageSMSModels item in listData)
                        {
                            _fac.UpdateSMSStatus(item.id, (int)Commons.SMSStatus.Fail, ref msg);
                        }
                        msg = "Run SMS marketing fail!";
                    }
                }

            }
            if (string.IsNullOrEmpty(msg))
            {
                TempData["SuccessMessage"] = "Run SMS marketing successfully!";
            }
            else
            {
                TempData["DataReturnError"] = model;
                TempData["ErrorMessage"] = msg;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Export()
        {
            try
            {
                XLWorkbook wb = new XLWorkbook();
                var wsMarketing = wb.Worksheets.Add("Marketing");
                //var wsTime = wb.Worksheets.Add("Time");

                var data = _fac.Export(ref wsMarketing/*, ref wsTime*/);

                if (!data.IsOk)
                {
                    ModelState.AddModelError("Marketing", data.Message);
                    return View(data);
                }
                ViewBag.wb = wb;
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = UTF8Encoding.UTF8.WebName;
                Response.ContentEncoding = UTF8Encoding.UTF8;
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string fileName = ("Data-Marketing-" + DateTime.Now.ToString("dd/MM/yyyy")).Replace(" ", "_");
                Response.AddHeader("content-disposition", String.Format(@"attachment;filename={0}.xlsx", fileName));

                using (var memoryStream = new System.IO.MemoryStream())
                {
                    wb.SaveAs(memoryStream);
                    memoryStream.WriteTo(HttpContext.Response.OutputStream);
                    memoryStream.Close();
                }
                HttpContext.Response.End();
                ViewBag.IsSuccess = true;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error(ex);
                return new HttpStatusCodeResult(400, ex.Message);
            }
        }
        
    }
}