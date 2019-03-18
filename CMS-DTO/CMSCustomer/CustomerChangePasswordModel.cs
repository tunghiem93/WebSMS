using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSCustomer
{
    public class CustomerChangePasswordModel
    {
        public string Email { get; set; }
        public string LastPassword { get; set; }
        [Required(ErrorMessage = "Please enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter your password 2")]
        [DataType(DataType.Password)]
        public string Password2 { get; set; }
        public bool Status { get; set; }
    }
}
