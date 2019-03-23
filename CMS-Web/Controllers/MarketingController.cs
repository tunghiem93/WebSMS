using ClosedXML.Excel;
using CMS_DTO.CMSBase;
using CMS_DTO.CMSMarketing;
using CMS_Shared.CMSMarketing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Controllers
{
    public class MarketingController : BasesController
    {
        public CMSMarketingFactory _fac = null;
        public MarketingController()
        {
            _fac = new CMSMarketingFactory();
        }
        // GET: Marketing
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(CMS_MarketingModels model)
        {
            try
            {
                if (CurrentUser == null || string.IsNullOrEmpty(CurrentUser.ID))
                {
                    return RedirectToAction("Index", "Login");
                }
                if (model.ExcelUpload == null)
                {
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
                var result = _fac.Import(filePath, CurrentUser.ID, CurrentUser.Name, CurrentUser.Phone, ref msg);
                model.ListSMS = result;
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);

                if (msg.Equals(""))
                {
                    return View("Index", model);
                }
                else
                {
                    ModelState.AddModelError("Import_Marketing: ", msg);
                    return View("Index", model);
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error(ex);
                ModelState.AddModelError("Import_Marketing: ", ex.Message);
                return View("Index", model);
            }
        }
        [HttpPost]
        public ActionResult SendSMS(List<CMS_MarketingModels> model)
        {
            /*
             * ok 0.Check user login
            ok 1. Check total credit of customer
             2. Check phone number get Operator
            ok 3. Insert to DB tru credit
             4. send sms to Centri server
             */
            if(CurrentUser == null || string.IsNullOrEmpty(CurrentUser.ID))
            {
                return RedirectToAction("Index","Login");
            } 
            if (model != null)
            {
               
                decimal totalPrice = 0;
                string msg = "";
                totalPrice = model.Sum(x => x.SMSPrice);
                if(!_fac.CheckTotalCredit(CurrentUser.ID, totalPrice))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach(CMS_MarketingModels item in model)
                    {
                        item.OperatorName = "";
                        item.RunTime = 60;
                        item.SendFrom = "";
                        item.TimeInput = DateTime.Now;
                        item.UpdatedBy = CurrentUser.ID;
                        item.CreatedBy = CurrentUser.ID;
                        _fac.InsertFromExcel(item, ref msg);
                    }
                }

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
                var wsTime = wb.Worksheets.Add("Time");

                var data = _fac.Export(ref wsMarketing, ref wsTime);

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