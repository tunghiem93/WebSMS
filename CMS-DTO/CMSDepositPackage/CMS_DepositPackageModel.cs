using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO
{
    public class CMS_DepositPackageModel
    {
        public string Id { get; set; }
        [Required]
        public string PackageName { get; set; }
        [Required]
        public decimal PackageSMS { get; set; }
        public decimal RateSMS {
            get;set;
        }
        public decimal PackagePrice { get; set; }
        [Required]
        public decimal Discount { get; set; }
        [Required]
        public decimal SMSPrice { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
