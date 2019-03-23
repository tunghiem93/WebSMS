using CMS_DTO.CMSCustomer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Controllers
{
    public class BasesController : Controller
    {
        public CustomerModels CurrentUser
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["UserC"] != null)
                    return (CustomerModels)System.Web.HttpContext.Current.Session["UserC"];
                else
                    return new CustomerModels();
            }
        }
    }
}