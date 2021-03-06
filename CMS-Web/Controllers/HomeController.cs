﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace CMS_Web.Controllers
{
    public class HomeController : BasesController
    {
        // GET: Home
        public ActionResult Index()
        {
            if (TempData["ErrorMessage"] != null)
            {
                ViewData["ErrorMessage"] = TempData["ErrorMessage"].ToString();
            }
            return View();
        }
    }
}