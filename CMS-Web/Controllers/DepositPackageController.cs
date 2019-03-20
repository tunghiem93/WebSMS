using CMS_DTO;
using CMS_DTO.CMSDepositPackage;
using CMS_Shared.CMSEmployees;
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
        public DepositPackageController()
        {
            fac = new CMSDepositPackageFactory();
            facP = new CMSPaymentMethodFactory();
        }
        // GET: DepositPackage
        public ActionResult Index()
        {
            var model = fac.GetList();
            ViewBag.Payment = facP.GetList();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddDepositPackage(CMS_DepositPackageModel item)
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetPrice()
        {
            var Priceobj = new PriceObjects();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //GET Method  
                HttpResponseMessage response = await client.GetAsync("https://api.binance.com/api/v1/ticker/24hr?symbol=XRPUSDT");
                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsStringAsync();
                    Priceobj = JsonConvert.DeserializeObject<PriceObjects>(res);
                }
            }
            return Json(Priceobj, JsonRequestBehavior.AllowGet);
        }
    }
}