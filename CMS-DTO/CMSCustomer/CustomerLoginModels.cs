using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSCustomer
{
    public class CustomerLoginModels
    {
        [Required(ErrorMessage ="Please enter your phone/E-mail")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Please enter your password")]
        public string Password { get; set; }
    }
}
