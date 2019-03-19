using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CMS_DTO.CMSSysConfig
{
    public class CMS_SysConfigModels
    {
        public string Id { get; set; }
        public decimal Value { get; set; }
        public int ValueType { get; set; }// otp, marketing ....
        [AllowHtml]
        public string SiteContent { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }        

        public CMS_SysConfigModels()
        {
        }
    }
}
