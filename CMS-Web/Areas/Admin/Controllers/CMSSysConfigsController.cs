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

        #region Rate USD 
        [HttpGet]
        public ActionResult ViewRateUSD(string Id)
        {
            var model = _fac.GetDetailRateUSD(Id);
            return PartialView("_ViewRateUSD", model);
        }

        [HttpGet]
        public ActionResult EditRateUSD(string Id)
        {
            var model = _fac.GetDetailRateUSD(Id);
            return PartialView("_EditRateUSD", model);
        }

        [HttpPost]
        public ActionResult EditRateUSD(CMS_SysConfigModels model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_EditRateUSD", model);
                }

                var msg = "";
                var result = _fac.UpdateRateUSD(model, ref msg);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("__EditRateUSDError: ", msg);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_EditRateUSD", model);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_EditRateUSD", model);
            }

        }
        #endregion

        #region Rate PM/USD 
        [HttpGet]
        public ActionResult ViewRatePMUSD(string Id)
        {
            var model = _fac.GetDetailRatePMUSD(Id);
            return PartialView("_ViewRatePMUSD", model);
        }

        [HttpGet]
        public ActionResult EditRatePMUSD(string Id)
        {
            var model = _fac.GetDetailRatePMUSD(Id);
            return PartialView("_EditRatePMUSD", model);
        }

        [HttpPost]
        public ActionResult EditRatePMUSD(CMS_SysConfigModels model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_EditRatePMUSD", model);
                }

                var msg = "";
                var result = _fac.UpdateRatePMUSD(model, ref msg);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("__EditRatePMUSDError: ", msg);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_EditRatePMUSD", model);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_EditRatePMUSD", model);
            }

        }
        #endregion

        #region Rate SMS Marketing 
        [HttpGet]
        public ActionResult ViewRateSMSMarketing(string Id)
        {
            var model = _fac.GetDetailRateSMSMarketing(Id);
            return PartialView("_ViewRateSMSMarketing", model);
        }

        [HttpGet]
        public ActionResult EditRateSMSMarketing(string Id)
        {
            var model = _fac.GetDetailRateSMSMarketing(Id);
            return PartialView("_EditRateSMSMarketing", model);
        }

        [HttpPost]
        public ActionResult EditRateSMSMarketing(CMS_SysConfigModels model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_EditRateSMSMarketing", model);
                }

                var msg = "";
                var result = _fac.UpdateRateSMSMarketing(model, ref msg);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("__EditRateSMSMarketingError: ", msg);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_EditRateSMSMarketing", model);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_EditRateSMSMarketing", model);
            }

        }
        #endregion

        #region Rate SMS OTP 
        [HttpGet]
        public ActionResult ViewRateSMSOTP(string Id)
        {
            var model = _fac.GetDetailRateSMSOTP(Id);
            return PartialView("_ViewRateSMSOTP", model);
        }

        [HttpGet]
        public ActionResult EditRateSMSOTP(string Id)
        {
            var model = _fac.GetDetailRateSMSOTP(Id);
            return PartialView("_EditRateSMSOTP", model);
        }

        [HttpPost]
        public ActionResult EditRateSMSOTP(CMS_SysConfigModels model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_EditRateSMSOTP", model);
                }

                var msg = "";
                var result = _fac.UpdateRateSMSOTP(model, ref msg);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("__EditRateSMSOTPError: ", msg);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_EditRateSMSOTP", model);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_EditRateSMSOTP", model);
            }

        }
        #endregion

        #region Waiting time 
        [HttpGet]
        public ActionResult ViewWaitingTime(string Id)
        {
            var model = _fac.GetDetailWaitingTime(Id);
            return PartialView("_ViewWaitingTime", model);
        }

        [HttpGet]
        public ActionResult EditWaitingTime(string Id)
        {
            var model = _fac.GetDetailWaitingTime(Id);
            return PartialView("_EditWaitingTime", model);
        }

        [HttpPost]
        public ActionResult EditWaitingTime(CMS_SysConfigModels model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_EditWaitingTime", model);
                }

                var msg = "";
                var result = _fac.UpdateWaitingTime(model, ref msg);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("__EditWaitingTimeError: ", msg);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_EditWaitingTime", model);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_EditWaitingTime", model);
            }

        }
        #endregion

        #region Credit default for New member 
        [HttpGet]
        public ActionResult ViewCreditNewMember(string Id)
        {
            var model = _fac.GetDetailCreditNewMember(Id);
            return PartialView("_ViewCreditNewMember", model);
        }

        [HttpGet]
        public ActionResult EditCreditNewMember(string Id)
        {
            var model = _fac.GetDetailCreditNewMember(Id);
            return PartialView("_EditCreditNewMember", model);
        }

        [HttpPost]
        public ActionResult EditCreditNewMember(CMS_SysConfigModels model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_EditCreditNewMember", model);
                }

                var msg = "";
                var result = _fac.UpdateCreditNewMember(model, ref msg);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("__EditCreditNewMemberError: ", msg);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_EditCreditNewMember", model);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_EditCreditNewMember", model);
            }

        }
        #endregion

        #region Site maintain 
        [HttpGet]
        public ActionResult ViewSiteMaintain(string Id)
        {
            var model = _fac.GetDetailSiteMaintain(Id);
            return PartialView("_ViewSiteMaintain", model);
        }

        [HttpGet]
        public ActionResult EditSiteMaintain(string Id)
        {
            var model = _fac.GetDetailSiteMaintain(Id);
            return PartialView("_EditSiteMaintain", model);
        }

        [HttpPost]
        public ActionResult EditSiteMaintain(CMS_SysConfigModels model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_EditSiteMaintain", model);
                }

                var msg = "";
                var result = _fac.UpdateSiteMaintain(model, ref msg);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("_EditSiteMaintainError: ", msg);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_EditSiteMaintain", model);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_EditSiteMaintain", model);
            }

        }
        #endregion
    }
}