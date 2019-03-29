using System;
using System.ComponentModel.DataAnnotations;

namespace CMS_DTO.CMSGSM
{
    public class CMS_GMSModels
    {
        public string Id { get; set; }
        [Required]
        public string GSMName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public CMS_GMSModels()
        {
            IsActive = true;
        }
    }
}
