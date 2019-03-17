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
        public string Wallet_Receiving_Money { get; set; } // ví nhận tiền
        public virtual List<CMS_CustomerActiveCode> CustomerActiveCode { get; set; }

        public CMS_Customers()
        {
            CustomerActiveCode = new List<CMS_CustomerActiveCode>();
        }
    }
}
