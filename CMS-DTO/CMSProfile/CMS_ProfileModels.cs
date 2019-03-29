using CMS_DTO.CMSMarketing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSProfile
{
    public class CMS_ProfileModels
    {
        public List<CMS_MarketingModels> OTPs { get; set; }
        public List<CMS_MarketingModels> Marketings { get; set; }

        public CMS_ProfileModels()
        {
            OTPs = new List<CMS_MarketingModels>();
            Marketings = new List<CMS_MarketingModels>();
        }
    }
}
