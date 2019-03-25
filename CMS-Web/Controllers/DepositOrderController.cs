using CMS_DTO;
using CMS_DTO.CMSCustomer;
using CMS_Shared;
using CMS_Shared.CMSDepositTransaction;
using CMS_Shared.CMSSystemConfig;
using CMS_Web.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Controllers
{
    [NuWebAuth]
    public class DepositOrderController : Controller
    {
        private readonly CMSDepositTransactionFactory fac;
        private readonly CMSSysConfigFactory facS;
        public DepositOrderController()
        {
            fac = new CMSDepositTransactionFactory();
            facS = new CMSSysConfigFactory();
        }
        // GET: DepositOrder
        public ActionResult Index()
        {
            var Customer = Session["UserC"] as CustomerModels;
            var Status = new List<int>();
            Status.Add((int)Commons.DepositStatus.WaitingPay);
            Status.Add((int)Commons.DepositStatus.Cancel);
            var model = fac.GetListDepositTransaction(Customer.ID, Status);
            ViewBag.disabled = model != null && model.Count > 0;
            var config = facS.GetList().Where(x => x.ValueType == (int)Commons.ConfigType.WaitingTime).FirstOrDefault();
            var IsCancel = model.Any(x => x.Status == (int)Commons.DepositStatus.Cancel);
            if (config != null && !IsCancel)
                ViewBag.time = Convert.ToInt16(config.Value) * 60;
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(List<CMS_DepositTransactionsModel> model)
        {
            var Customer = Session["UserC"] as CustomerModels;
            if(model != null)
            {
                int StatusOrder = (int)Commons.DepositStatus.ConfirmedPay;
                if (model.Any(x => x.IsClose))
                {
                    StatusOrder = (int)Commons.DepositStatus.Cancel;
                }
                var result = fac.ChangeStatusDepositTransaction(model, StatusOrder);
                ViewBag.disabled = false;// don't show button thanh toán
                ViewBag.notification = result;
            }
            var Status = new List<int>();
            Status.Add((int)Commons.DepositStatus.ConfirmedPay);
            Status.Add((int)Commons.DepositStatus.Cancel);
            var Ids = model.Select(x => x.Id).ToList();
            model = fac.GetListDepositTransaction(Customer.ID, Status).Where(x => Ids.Contains(x.Id)).ToList();
            ViewBag.disabled = model != null && model.Any(x => x.Status == (int)Commons.DepositStatus.Cancel);
            return View(model);
        }
    }
}