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
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string SMSContent { get; set; }
        public string SendTo { get; set; }
        public string SendFrom { get; set; }
        public string OperatorName { get; set; }
        public decimal SMSPrice { get; set; }
        public int SMSType { get; set; }
        public decimal? RunTime { get; set; }
        public DateTime TimeInput { get; set; }
        public int Status { get; set; }
        public HttpPostedFileBase ExcelUpload { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public CMS_MarketingModels()
        {
            this.IsActive = true;
        }
    }
}
