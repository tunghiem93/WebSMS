using CMS_DTO;
using CMS_DTO.CMSGSM;
using CMS_DTO.CMSSims;
using CMS_Shared.CMSGSM;
using CMS_Shared.CMSSims;
using CMS_Web.Web.App_Start;
using System;
using System.Net;
using System.Web.Mvc;

namespace CMS_Web.Areas.Admin.Controllers
{
    [NuAuth]
    public class CMSGSMController : BaseController
    {
        private CMSGSMFactory _factory;
        public CMSGSMController()
        {
            _factory = new CMSGSMFactory();
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
            CMS_GMSModels model = new CMS_GMSModels();
            return PartialView("_Create", model);
        }

        public CMS_GMSModels GetDetail(string Id)
        {
            var data =  _factory.GetDetail(Id);
            return data;
        }

        [HttpPost]
        public ActionResult Create(CMS_GMSModels model)
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
        public ActionResult Edit(CMS_GMSModels model)
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
        public ActionResult Delete(CMS_GMSModels model)
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