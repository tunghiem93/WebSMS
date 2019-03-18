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
        public decimal Rate { get; set; }
        public int RateType { get; set; }// otp, marketing ....
        public int WaitingTime { get; set; }
        [AllowHtml]
        public string SiteContent { get; set; }
        public string Description { get; set; }

        //show html
        public string RateUSD { get; set; }
        public string RatePMUSD { get; set; }
        public string RateMarketing { get; set; }
        public string RateOTP { get; set; }
        public decimal CreditNewMember { get; set; }

        public CMS_SysConfigModels()
        {
            WaitingTime = 15;
        }
    }
}
