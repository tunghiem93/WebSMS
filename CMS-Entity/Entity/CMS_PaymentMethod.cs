using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Entity.Entity
{
    public class CMS_PaymentMethod : CMS_EntityBase
    {
        public string Id { get; set; }
        public string PaymentName { get; set; }
        public int PaymentType { get; set; }
    }
}
