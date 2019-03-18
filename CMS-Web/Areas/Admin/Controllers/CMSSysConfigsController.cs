using CMS_DTO.CMSSysConfig;
using CMS_Shared.CMSSystemConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Areas.Admin.Controllers
{
    public class CMSSysConfigsController : Controller
    {
        private CMSSysConfigFactory _fac;
        
        public CMSSysConfigsController()
        {
            _fac = new CMSSysConfigFactory();
        }
        // GET: Admin/CMSCategories
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadGrid()
        {
            var model = _fac.GetList();
            return PartialView("_ListData", model);
        }

        [HttpGet]
        public ActionResult SiteMaintain(string Id)
        {
            var model = _fac.GetDetailSiteMaintain(Id);
            return View("_SiteMaintain", model);
        }

        [HttpPost]
        public ActionResult SiteMaintain(CMS_SysConfigModels model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_SiteMaintain", model);
                }

                var msg = "";
                var result = _fac.UpdateSiteMaintain(model, ref msg);
                if (result)
                {
                    return RedirectToAction("_SiteMaintain");
                }
                ModelState.AddModelError("_SiteMaintainError: ", msg);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_SiteMaintain", model);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_SiteMaintain", model);
            }

        }
    }
}