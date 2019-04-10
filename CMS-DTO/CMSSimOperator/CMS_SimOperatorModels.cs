using System;
using System.ComponentModel.DataAnnotations;

namespace CMS_DTO.CMSSimOperator
{
    public class CMS_SimOperatorModels
    {
        public string Id { get; set; }
        [Required]
        [MaxLength(5)]
        public string HeaderPhone { get; set; }
        [Required]
        [MaxLength(50)]
        public string OperaterName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public CMS_SimOperatorModels()
        {
            IsActive = true;
        }
    }
}
