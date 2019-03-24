using CMS_DTO;
using CMS_Shared.CMSDepositTransaction;
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
    public class CMSDepositTransactionController : BaseController
    {
        private CMSDepositTransactionFactory _factory;
        public CMSDepositTransactionController()
        {
            _factory = new CMSDepositTransactionFactory();
        }
        // GET: Admin/CMSDepositTransaction
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoadGrid()
        {
            var model = _factory.GetList();
            return PartialView("_ListData", model);
        }

        public CMS_DepositTransactionsModel GetDetail(string Id)
        {
            var data = _factory.GetDetail(Id);
            return data;
        } 

        [HttpGet]
        public ActionResult Edit(string Id)
        {
            var model = GetDetail(Id);
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(CMS_DepositTransactionsModel model)
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
                var result = _factory.ChangeStatus(model, CurrentUser.UserId);
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