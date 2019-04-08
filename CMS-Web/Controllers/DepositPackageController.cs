using CMS_DTO;
using CMS_DTO.CMSCustomer;
using CMS_DTO.CMSDepositPackage;
using CMS_DTO.CMSPaymentMethod;
using CMS_DTO.CMSSession;
using CMS_Shared;
using CMS_Shared.CMSDepositTransaction;
using CMS_Shared.CMSEmployees;
using CMS_Shared.CMSSystemConfig;
using CMS_Shared.Utilities;
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
    public class DepositPackageController : BasesController
    {
        private readonly CMSDepositPackageFactory fac;
        private readonly CMSPaymentMethodFactory facP;
        private readonly CMSSysConfigFactory facC;
        private readonly CMSDepositTransactionFactory facT;
        private readonly CMSSysConfigFactory facS;
        public DepositPackageController()
        {
            fac = new CMSDepositPackageFactory();
            facP = new CMSPaymentMethodFactory();
            facC = new CMSSysConfigFactory();
            facT = new CMSDepositTransactionFactory();
            facS = new CMSSysConfigFactory();
        }
        // GET: DepositPackage
        public ActionResult Index()
        {
            var model = fac.GetList();
            model.ForEach(x =>
            {
                x.sPrice = x.PriceDefault.ToString();
            });
            ViewBag.Payment = facP.GetList().OrderBy(o=>o.PaymentType).ToList();
            return View(model);
        }
                
        [HttpPost]
        public async Task<ActionResult> PayNow(List<CMS_DepositTransactionsModel> model)
        {
            var Customer = Session["UserC"] as UserSession;
            var Payment = facP.GetDetail(model.FirstOrDefault().PaymentMethodId);            
            var Config = facC.GetList().Where(x => x.ValueType == (int)Commons.ConfigType.USD).FirstOrDefault();
            decimal? Coin = 0;
            if (!string.IsNullOrEmpty(Payment.URLApi))
            {
                var Priceobj = new PriceObjects();
                Priceobj = await GetLastPrice(Payment.URLApi);
                
                if (Priceobj.last.HasValue)
                {
                    Coin = model[0].Price / Priceobj.last.Value;
                }
                else
                {
                    Coin = model[0].Price / Priceobj.lastPrice.Value;
                }
            }
            model.ForEach(x =>
            {
                x.PaymentMethodName = Payment.PaymentName;
                x.WalletMoney = Payment.WalletMoney;
                x.ExchangeRate = Config != null ? Config.Value : 0;
                x.PayCoin = Coin == 0 ? x.PayCoin : Coin.Value;
                x.CustomerId = Customer.UserId;
                x.CustomerName = Customer.UserName;
                x.DepositNo = CommonHelper.RandomDepositNo();
            });
            var msg = "";
            var result = facT.CreateDepositTransaction(model, ref msg, null);
            var obj = new
            {
                msg = msg,
                status = result
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        private CMS_PaymentMethodModels GetURLApi(string paymentId)
        {
            var data = facP.GetDetail(paymentId);
            if(data != null)
                return data;
            return null;
        }

        private async Task<PriceObjects> GetLastPrice (string URLApi)
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
                    if (Priceobj.result != null)
                    {
                        Priceobj.last = Priceobj.result.Last;
                    }
                }
            }
            return Priceobj;
        }

        [HttpPost]
        public async Task<ActionResult> GetPrice(string paymentId, decimal priceUSD)
        {
            var data = facP.GetDetail(paymentId);
            if (data != null)
            {
                if (data.ReferenceExchange == (int)Commons.ExchangeType.None)
                {
                    var lstDataSys = facS.GetList();
                    var Priceobj = new PriceObjects();
                    var ratePMUSD = lstDataSys.Where(o => o.ValueType.Equals((int)Commons.ConfigType.PMUSD)).Select(o => o.Value).FirstOrDefault();
                    Priceobj.lastPrice = priceUSD / ratePMUSD;
                    Priceobj.tempPrice = Math.Round(Priceobj.lastPrice.Value, 2).ToString();
                    Priceobj.PaymentName = !string.IsNullOrEmpty(data.PaymentName) ? "PMUSD" : "PMUSD";
                    return Json(Priceobj, JsonRequestBehavior.AllowGet);
                }
            }            
            var URLApi = GetURLApi(paymentId);
            if(URLApi != null && !string.IsNullOrEmpty(URLApi.URLApi))
            {
                var Priceobj = new PriceObjects();
                Priceobj = await GetLastPrice(URLApi.URLApi);
                Priceobj.ScaleNumber = URLApi.ScaleNumber.HasValue ? URLApi.ScaleNumber.Value : 0;
                Priceobj.PaymentName = !string.IsNullOrEmpty(data.PaymentName) ? data.PaymentName : "";
                if (Priceobj.ScaleNumber > 0)
                {
                    if(Priceobj.last.HasValue)
                    {
                        var temp = priceUSD/Priceobj.last.Value;
                        Priceobj.last = temp;
                        Priceobj.tempPrice = string.Format("{0:N" + Priceobj.ScaleNumber + "}", Priceobj.last.Value);
                    }
                    else
                    {
                        var temp = priceUSD / Priceobj.lastPrice.Value;
                        Priceobj.lastPrice = temp;
                        Priceobj.tempPrice = string.Format("{0:N" + Priceobj.ScaleNumber + "}", Priceobj.lastPrice.Value);
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