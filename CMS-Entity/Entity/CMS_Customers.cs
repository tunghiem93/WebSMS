using CMS_Entity.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Entity.Entity
{
    public class CMS_Customers : CMS_EntityBase
    {
        public string Id { get; set; }
        public string CustomerName { get { return this.FirstName + " " + this.LastName; } }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Password2 { get; set; }
        public string Phone { get; set; }
        public int Status { get; set; }
        public string CreditNumber { get; set; } // ví nhận tiền
        public decimal TotalCredit { get; set; }
        public int SMSBalances { get; set; }
        public string APIKey { get; set; }
        public string APIPass { get; set; }
        public bool IsVerifiedEmail { get; set; }
        public bool IsVerifiedPhone { get; set; }
        public virtual List<CMS_CustomerActiveCode> CustomerActiveCode { get; set; }
        public virtual List<CMS_Marketing> Marketing { get; set; }
        public string MemberID { get; set; }
        public CMS_Customers()
        {
            CustomerActiveCode = new List<CMS_CustomerActiveCode>();
        }
    }
}
