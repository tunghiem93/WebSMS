﻿using CMS_DTO;
using CMS_DTO.CMSCustomer;
using CMS_DTO.CMSDepositPackage;
using CMS_DTO.CMSPaymentMethod;
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
    public class DepositPackageController : Controller
    {
        private readonly CMSDepositPackageFactory fac;
        private readonly CMSPaymentMethodFactory facP;
        private readonly CMSSysConfigFactory facC;
        private readonly CMSDepositTransactionFactory facT;
        public DepositPackageController()
        {
            fac = new CMSDepositPackageFactory();
            facP = new CMSPaymentMethodFactory();
            facC = new CMSSysConfigFactory();
            facT = new CMSDepositTransactionFactory();
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
        public async Task<ActionResult> PayNow(List<CMS_DepositTransactionsModel> model)
        {
            var Customer = Session["UserC"] as CustomerModels;
            var Payment = facP.GetDetail(model.FirstOrDefault().PaymentMethodId);
            var Config = facC.GetList().Where(x => x.ValueType == (int)Commons.ConfigType.USD).FirstOrDefault();
            decimal? Coin = 0;
            if (!string.IsNullOrEmpty(Payment.URLApi))
            {
                var Priceobj = new PriceObjects();
                Priceobj = await GetLastPrice(Payment.URLApi);
                if (Priceobj.last.HasValue)
                    Coin = Priceobj.last;
                else
                    Coin = Priceobj.lastPrice;
            }
            model.ForEach(x =>
            {
                x.PaymentMethodName = Payment.PaymentName;
                x.WalletMoney = Payment.WalletMoney;
                x.ExchangeRate = Config != null ? Config.Value : 0;
                x.PayCoin = Coin == 0 ? x.PayCoin : Coin.Value;
                x.CustomerId = Customer.ID;
                x.CustomerName = Customer.Name;
                x.DepositNo = CommonHelper.RandomDepositNo();
            });
            var msg = "";
            var result = facT.CreateDepositTransaction(model,ref msg);
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
        public async Task<ActionResult> GetPrice(string paymentId)
        {
            var URLApi = GetURLApi(paymentId);
            if(URLApi != null && !string.IsNullOrEmpty(URLApi.URLApi))
            {
                var Priceobj = new PriceObjects();
                Priceobj = await GetLastPrice(URLApi.URLApi);
                Priceobj.ScaleNumber = URLApi.ScaleNumber.HasValue ? URLApi.ScaleNumber.Value : 0;
                if(Priceobj.ScaleNumber > 0)
                {
                    Priceobj.tempPrice = string.Format("{0:N"+Priceobj.ScaleNumber+"}", Priceobj.last);
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