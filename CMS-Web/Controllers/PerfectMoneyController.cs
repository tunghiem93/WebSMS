using CMS_DTO;
using CMS_DTO.CMSCustomer;
using CMS_DTO.CMSDepositPackage;
using CMS_Shared;
using CMS_Shared.CMSDepositTransaction;
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

        public PerfectMoneyController()
        {
            facT = new CMSDepositTransactionFactory();
            facC = new CMSSysConfigFactory();
        }

        // GET: PerfectMoney
        public ActionResult Index(decimal? price)
        {
            PerfectMoneyModel model = new PerfectMoneyModel();
            try
            {
                List<CMS_DepositTransactionsModel> models = new List<CMS_DepositTransactionsModel>();
                var Config = facC.GetList().Where(x => x.ValueType == (int)Commons.ConfigType.USD).FirstOrDefault();
                var customer = Session["UserC"] as CustomerModels;
                models[0].PaymentMethodName = "Perfect money";
                models[0].WalletMoney = Commons.APIType.APIPerfectMonney.ToString();
                models[0].ExchangeRate = Config != null ? Config.Value : 0;
                models[0].PayCoin = price == 0 ? price.Value : price.Value;
                models[0].CustomerId = customer.ID;
                models[0].CustomerName = customer.Name;
                models[0].DepositNo = CommonHelper.RandomDepositNo();
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