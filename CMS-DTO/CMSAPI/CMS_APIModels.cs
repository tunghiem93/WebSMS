using CMS_DTO.CMSBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CMS_DTO.CMSAPI
{
    public class CMS_APIModels
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Please entering filed")]
        public string LinkAPI { get; set; }
        [Required(ErrorMessage = "Please entering filed")]
        public string APIName { get; set; }
        public int APIType { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string sStatus { get; set; }
        public string sTypeName { get; set; }

        public CMS_APIModels()
        {
            IsActive = true;
        }
    }
}
