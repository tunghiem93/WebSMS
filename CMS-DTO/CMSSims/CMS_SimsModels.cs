using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSSims
{
    public class CMS_SimsModels
    {
        public string Id { get; set; }
        [Required]
        public string SimName { get; set; }
        public string SimNumber { get; set; }
        public string OperatorName { get; set; }
        [Required]
        public int Status { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public CMS_SimsModels()
        {
            IsActive = true;
        }
    }
}
