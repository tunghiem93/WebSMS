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
        public decimal Value { get; set; }
        public int ValueType { get; set; }// otp, marketing ....
        public string SiteContent { get; set; }
        public string Description { get; set; }
    }
}
