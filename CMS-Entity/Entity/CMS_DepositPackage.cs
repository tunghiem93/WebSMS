using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Entity.Entity
{
    public class CMS_DepositPackage : CMS_EntityBase
    {
        public string Id { get; set; }
        public string PackageName { get; set; }
        public decimal PackageSMS { get; set; }
        //public decimal PackagePrice { get; set; }
        public decimal Discount { get; set; }
        public decimal SMSPrice { get; set; }
    }
}
