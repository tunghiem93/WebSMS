using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Entity.Entity
{
    public class CMS_ConfigRates : CMS_EntityBase
    {
        public string Id { get; set; }
        public decimal Rate { get; set; }
        public int RateType { get; set; }// otp, marketing ....
        public string Description { get; set; }
    }
}
