using CMS_DTO.CMSCustomer;
using CMS_DTO.CMSDepositPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Controllers
{
    public class PerfectMoneyController : Controller
    {
        // GET: PerfectMoney
        public ActionResult Index(decimal? price)
        {
            PerfectMoneyModel model = new PerfectMoneyModel();
            try
            {
                var customer = Session["UserC"] as CustomerModels;
                var req = Request.Params.AllKeys;
                if(req != null && req.Count() > 0)
                {
                    var Paymnet_Batch_Num = Request.Params["PAYMENT_BATCH_NUM"];
                    if (Paymnet_Batch_Num == "0" && Paymnet_Batch_Num != null)
                    {
                        // cancel
                    }
                    else if(Paymnet_Batch_Num != "0" && Paymnet_Batch_Num != null)
                    {
                        //Success
                    }
                }
                model.CustomerId = customer.ID;
                model.PAYEE_NAME = customer.Name;
                model.PAYMENT_AMOUNT = price;
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }
    }
}