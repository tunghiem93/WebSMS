using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Entity.Entity
{
    public class CMS_CustomerActiveCode : CMS_EntityBase
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string Code { get; set; }
        public virtual CMS_Customers Customer { get; set; }
    }
}
