using CMS_DTO;
using CMS_DTO.CMSDepositPackage;
using CMS_Shared;
using CMS_Shared.CMSEmployees;
using CMS_Shared.CMSSystemConfig;
using CMS_Web.Web.App_Start;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Controllers
{
    [NuWebAuth]
    public class DepositPackageController : Controller
    {
        private readonly CMSDepositPackageFactory fac;
        private readonly CMSPaymentMethodFactory facP;
        private readonly CMSSysConfigFactory facC;
        public DepositPackageController()
        {
            fac = new CMSDepositPackageFactory();
            facP = new CMSPaymentMethodFactory();
            facC = new CMSSysConfigFactory();
        }
        // GET: DepositPackage
        public ActionResult Index()
        {
            var model = fac.GetList();
            model.ForEach(x =>
            {
                x.sPrice = string.Format("{0:N0}", x.PriceDefault);
            });
            ViewBag.Payment = facP.GetList();
            return View(model);
        }

        [HttpPost]
        public ActionResult PayNow(List<CMS_DepositTransactionsModel> model)
        {
            var Payment = facP.GetDetail(model.FirstOrDefault().PaymentMethodId);
            var Config = facC.GetList().Where(x => x.ValueType == (int)Commons.ConfigType.USD).FirstOrDefault();
            model.ForEach(x =>
            {
                x.PaymentMethodName = Payment.PaymentName;
                x.WalletMoney = Payment.WalletMoney;
                x.ExchangeRate = Config != null ? Config.Value : 0;
            });
            return RedirectToAction("Index", "Home");
        }

        private string GetURLApi(string paymentId)
        {
            var data = facP.GetDetail(paymentId);
            if(data != null)
                return data.URLApi;
            return null;
        }

        [HttpPost]
        public async Task<ActionResult> GetPrice(string paymentId)
        {
            var URLApi = GetURLApi(paymentId);
            if(!string.IsNullOrEmpty(URLApi))
            {
                var Priceobj = new PriceObjects();
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //GET Method  
                    HttpResponseMessage response = await client.GetAsync(URLApi);
                    if (response.IsSuccessStatusCode)
                    {
                        var res = await response.Content.ReadAsStringAsync();
                        Priceobj = JsonConvert.DeserializeObject<PriceObjects>(res);
                    }
                }
                return Json(Priceobj, JsonRequestBehavior.AllowGet);
            } else
            {
                var obj = new
                {
                    Status = 0
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
    }
}