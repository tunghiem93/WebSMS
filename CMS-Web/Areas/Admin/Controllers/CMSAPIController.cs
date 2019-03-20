using CMS_DTO.CMSAPI;
using CMS_Shared;
using CMS_Shared.CMSAPI;
using CMS_Web.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Areas.Admin.Controllers
{
    [NuAuth]
    public class CMSAPIController : HQController
    {
        private CMSAPIFactory _fac = null;

        public CMSAPIController()
        {
            _fac = new CMSAPIFactory();
            ViewBag.ListAPI = GetListAPI();
        }
        // GET: Admin/CMSAPI
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadGrid()
        {
            var model = _fac.GetList();
            model.ForEach(x =>
            {
                x.sStatus = x.IsActive ? "Kích hoạt" : "Chưa kích hoạt";
                if (x.APIType == (int)Commons.APIType.APISMS)
                {
                    x.sTypeName = Commons.APIType.APISMS.ToString();
                }
                if (x.APIType == (int)Commons.APIType.APISim)
                {
                    x.sTypeName = Commons.APIType.APISim.ToString();
                }
            });
            return PartialView("_ListData", model);
        }

        public ActionResult Create()
        {
            CMS_APIModels model = new CMS_APIModels();
            return PartialView("_Create", model);
        }

        public CMS_APIModels GetDetail(string Id)
        {
            var data = _fac.GetDetail(Id);
            if (data != null)
            {
                if (data.APIType == (int)Commons.APIType.APISMS)
                    data.sTypeName = Commons.APIType.APISMS.ToString();
                if (data.APIType == (int)Commons.APIType.APISim)
                    data.sTypeName = Commons.APIType.APISim.ToString();
            }
            return data;
        }

        [HttpPost]
        public ActionResult Create(CMS_APIModels model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Create", model);
                }
                var Id = "";
                var msg = "";
                model.CreatedBy = "admin";
                model.UpdatedBy = "admin";
                var result = _fac.CreateOrUpdate(model, ref Id, ref msg);
                if (result)
                {                   
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("APIName", msg);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Create", model);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Create", model);
            }
        }

        [HttpGet]
        public ActionResult Edit(string Id)
        {
            var model = GetDetail(Id);
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(CMS_APIModels model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Edit", model);
                }
                var Id = "";
                var msg = "";
                var result = _fac.CreateOrUpdate(model, ref Id, ref msg);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("APIName", msg);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Edit", model);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Edit", model);
            }
        }

        [HttpGet]
        public ActionResult View(string Id)
        {
            var model = GetDetail(Id);
            return PartialView("_View", model);
        }

        [HttpGet]
        public ActionResult Delete(string Id)
        {
            var model = GetDetail(Id);
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(CMS_APIModels model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Delete", model);
                }
                var msg = "";
                var result = _fac.Delete(model.Id, ref msg);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("APIName", msg);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Delete", model);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Delete", model);
            }
        }
    }
}