using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Entity.Entity
{
    public class CMS_Marketing : CMS_EntityBase
    {
        public string Id { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
        public int NetworkType { get; set; }
        public int SMSType { get; set; }
        public decimal RunTime { get; set; }
        public string CustomerId { get; set; }
        public decimal Cost { get; set; }
        public DateTime TimeInput{ get; set; }
        public string OperatorName { get; set; }
        public int Status { get; set; }

        public virtual CMS_Customers Customer { get; set; }
    }
}
