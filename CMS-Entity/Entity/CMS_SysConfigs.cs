using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Entity.Entity
{
    public class CMS_SysConfigs : CMS_EntityBase
    {
        public string Id { get; set; }
        public decimal Rate { get; set; }
        public int RateType { get; set; }// otp, marketing ....
        public int WaitingTime { get; set; }
        public decimal TotalCredit { get; set; }
        public string SiteContent { get; set; }
        public string Description { get; set; }
    }
}
