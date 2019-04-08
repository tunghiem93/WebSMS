using CMS_DTO.CMSCustomer;
using CMS_DTO.CMSSession;
using CMS_Shared.CMSCustomers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Controllers
{
    public class BasesController : Controller
    {
        public readonly CMSCustomersFactory facC = null;

        public UserSession CurrentUser
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["UserC"] != null)
                    return (UserSession)System.Web.HttpContext.Current.Session["UserC"];
                else
                    return new UserSession();
            }
        }

        public BasesController()
        {
            facC = new CMSCustomersFactory();
            try
            {
                if (CurrentUser != null && CurrentUser.UserId != null)
                {
                    var data = facC.GetDetail(CurrentUser.UserId);
                    CurrentUser.TotalCredits = data != null ? data.TotalCredit : 0;
                }                
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}