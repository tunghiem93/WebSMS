using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSCustomer
{
    public class CustomerActiveCode
    {
        [Required(ErrorMessage ="Please enter coe")]
        public string Code { get; set; }
    }
}
