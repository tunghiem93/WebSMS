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
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }     
        public string SMSContent { get; set; }
        public string SendTo { get; set; }
        public string SendFrom { get; set; }
        public string OperatorName { get; set; }
        public decimal SMSPrice { get; set; }
        public int SMSType { get; set; }
        public decimal RunTime { get; set; }
        public DateTime TimeInput{ get; set; }
        public int Status { get; set; }
        public decimal SMSRate { get; set; }
    }
}
