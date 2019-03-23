using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSMarketing
{
    public class CMS_TestServiceModels
    {
        [Required(ErrorMessage = "Please enter your phone !")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Your phone not invalid !")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Please enter your SMS content !")]
        public string Content { get; set; }
    }
}
