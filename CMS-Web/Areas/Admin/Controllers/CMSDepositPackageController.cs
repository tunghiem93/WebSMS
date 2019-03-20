using CMS_DTO;
using CMS_DTO.CMSEmployee;
using CMS_Shared;
using CMS_Shared.CMSEmployees;
using CMS_Shared.Utilities;
using CMS_Web.Web.App_Start;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Areas.Admin.Controllers
{
    [NuAuth]
    public class CMSDepositPackageController : BaseController
    {
        private CMSDepositPackageFactory _factory;
        public CMSDepositPackageController()
        {
            _factory = new CMSDepositPackageFactory();
        }
        // GET: Admin/CMSCategories
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadGrid()
        {
            var model = _factory.GetList();
            return PartialView("_ListData", model);
        }

        public ActionResult Create()
        {
            CMS_DepositPackageModel model = _factory.CreateNew();
            return PartialView("_Create", model);
        }

        public CMS_DepositPackageModel GetDetail(string Id)
        {
            var data =  _factory.GetDetail(Id);
            return data;
        }

        [HttpPost]
        public ActionResult Create(CMS_DepositPackageModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Create", model);
                }

                var msg = "";
                model.CreatedBy = CurrentUser.UserId;
                model.UpdatedBy = CurrentUser.UserId;
                var result = _factory.CreateOrUpdate(model, ref msg);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                   
                ModelState.AddModelError("ErrorMessage", msg);
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
        public ActionResult Edit(CMS_DepositPackageModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Edit", model);
                }
                
                var msg = "";
                model.UpdatedBy = CurrentUser.UserId;
                var result = _factory.CreateOrUpdate(model, ref msg);
                if (result)
                {                   
                    return RedirectToAction("Index");
                }
                    
                ModelState.AddModelError("ErrorMessage", msg);
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
        public ActionResult Delete(CMS_DepositPackageModel model)
        {
            try
            {
                ModelState.Clear();
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Delete", model);
                }
                var msg = "";
                var result = _factory.Delete(model.Id, ref msg);
                if (result)
                    return RedirectToAction("Index");
                ModelState.AddModelError("ErrorMessage", msg);
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