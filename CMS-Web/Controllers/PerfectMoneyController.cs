using CMS_DTO;
using CMS_DTO.CMSCustomer;
using CMS_DTO.CMSDepositPackage;
using CMS_Shared;
using CMS_Shared.CMSCompanies;
using CMS_Shared.CMSDepositTransaction;
using CMS_Shared.CMSEmployees;
using CMS_Shared.CMSSystemConfig;
using CMS_Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Controllers
{
    public class PerfectMoneyController : Controller
    {
        private readonly CMSDepositTransactionFactory facT;
        private readonly CMSSysConfigFactory facC;
        private readonly CMSPaymentMethodFactory facP;
        private readonly CMSCompaniesFactory fac_Co;

        public PerfectMoneyController()
        {
            facT = new CMSDepositTransactionFactory();
            facC = new CMSSysConfigFactory();
            facP = new CMSPaymentMethodFactory();
            fac_Co = new CMSCompaniesFactory();
        }

        // GET: PerfectMoney
        public ActionResult Index(decimal? price)
        {
            PerfectMoneyModel model = new PerfectMoneyModel();
            if (TempData["ErrorMessage"] != null)
            {
                ViewData["ErrorMessage"] = TempData["ErrorMessage"].ToString();
            }
            if (TempData["SuccessMessage"] != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"].ToString();
            }
            if (TempData["DataReturnError"] != null)
            {
                model = (PerfectMoneyModel)TempData["DataReturnError"];
                return View(model);
            }
            try
            {
                List<CMS_DepositTransactionsModel> models = new List<CMS_DepositTransactionsModel>();
                var Config = facC.GetList().Where(x => x.ValueType == (int)Commons.ConfigType.USD).FirstOrDefault();
                var customer = Session["UserC"] as CustomerModels;
                CMS_DepositTransactionsModel _data = new CMS_DepositTransactionsModel();
                _data.PaymentMethodName = "Perfect money";
                _data.WalletMoney = Commons.APIType.APIPerfectMonney.ToString();
                _data.ExchangeRate = Config != null ? Config.Value : 0;
                _data.PayCoin = price == 0 ? price.Value : price.Value;
                _data.CustomerId = customer.ID;
                _data.CustomerName = customer.Name;
                _data.DepositNo = CommonHelper.RandomDepositNo();
                models.Add(_data);
                //Set account information of admin to perfect payment
                var payPerfect = facP.GetList().Where(o => o.ReferenceExchange.Equals((int)Commons.ExchangeType.None)).FirstOrDefault();
                var adminInfo = fac_Co.GetList().Select(o => o.Name).FirstOrDefault(); ; 
                model.PAYEE_ACCOUNT = payPerfect.WalletMoney;
                model.PAYEE_NAME = adminInfo;
                model.PAYMENT_AMOUNT = price == 0 ? price.Value : price.Value;
                model.PAYMENT_AMOUNT = Math.Round(model.PAYMENT_AMOUNT.Value, 2);
                model.PAYMENT_UNITS = "USD";
                var req = Request.Params.AllKeys;
                var msg = "";
                if (req != null && req.Count() > 0)
                {
                    var Paymnet_Batch_Num = Request.Params["PAYMENT_BATCH_NUM"];
                    if (Paymnet_Batch_Num == "0" && Paymnet_Batch_Num != null)
                    {
                        models[0].Status = (int)Commons.DepositStatus.Cancel;
                        var result = facT.CreateDepositTransaction(models, ref msg);
                        NSLog.Logger.Info("Perface_Payment", "Perfect payment Cancel");
                        // cancel
                    }
                    else if(Paymnet_Batch_Num != "0" && Paymnet_Batch_Num != null)
                    {
                        models[0].Status = (int)Commons.DepositStatus.Completed;
                        models[0].CustomerId = customer.ID;
                        models[0].CreatedDate = DateTime.Now;
                        models[0].UpdatedDate = DateTime.Now; ;
                        models[0].CreatedBy = customer.Name;
                        models[0].PackageSMS = price.Value;
                        var result = facT.CreateDepositTransaction(models, ref msg);
                        //Success
                    }
                }
                
            }
            catch (Exception ex)
            {
                NSLog.Logger.Info("Perface_Payment", "Perfect payment no successfully");
            }
            return View(model);
        }
    }
}