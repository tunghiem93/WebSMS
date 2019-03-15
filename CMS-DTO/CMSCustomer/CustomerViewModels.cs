using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSCustomer
{
    public class CustomerViewModels
    {
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter your lastname")]
        public string LastName { get; set; }
        [Required(ErrorMessage ="Please enter your email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail not invalid")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Please enter your phone")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Your phone not invalid !")]
        public string Phone { get; set; }
        [Required(ErrorMessage ="Please enter your password")]
        public string Password { get; set; }
        [Required(ErrorMessage ="Please enter your password1")]
        public string Password1 { get; set; }
        public int Status { get; set; }
    }
}
