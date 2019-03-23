using CMS_DTO;
using CMS_DTO.CMSCustomer;
using CMS_Shared;
using CMS_Shared.CMSDepositTransaction;
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
        public DepositOrderController()
        {
            fac = new CMSDepositTransactionFactory();
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
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(List<CMS_DepositTransactionsModel> model)
        {
            var Customer = Session["UserC"] as CustomerModels;
            if(model != null)
            {
                var result = fac.ChangeStatusDepositTransaction(model, (int)Commons.DepositStatus.ConfirmedPay);
                ViewBag.disabled = false;// don't show button thanh toán
                ViewBag.notification = result;
            }
            var Status = new List<int>();
            Status.Add((int)Commons.DepositStatus.ConfirmedPay);
            var Ids = model.Select(x => x.Id).ToList();
            model = fac.GetListDepositTransaction(Customer.ID, Status).Where(x => Ids.Contains(x.Id)).ToList();
            return View(model);
        }
    }
}