using CMS_DTO.CMSBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CMS_DTO.CMSMarketing
{
    public class CMS_MarketingModels
    {
        public string Id { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
        public int NetworkType { get; set; }
        public int SMSType { get; set; }
        public decimal RunTime { get; set; }
        public string CustomerId { get; set; }
        public decimal Cost { get; set; }
        public DateTime TimeInput { get; set; }
        public string OperatorName { get; set; }
        public int Status { get; set; }
        public HttpPostedFileBase ExcelUpload { get; set; }

        //Show
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
    }
}
