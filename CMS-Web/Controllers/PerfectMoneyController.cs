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
        public ActionResult Index(decimal price)
        {
            return View();
        }
    }
}