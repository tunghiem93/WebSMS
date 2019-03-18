using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSPaymentMethod
{
    public class CMS_PaymentMethodModels
    {
        public string Id { get; set; }
        [Required]
        public string PaymentName { get; set; }
        [Required]
        public int PaymentType { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
