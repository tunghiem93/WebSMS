using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Entity.Entity
{
    public class CMS_DepositTransactions : CMS_EntityBase
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string WalletMoney { get; set; }
        public string PackageId { get; set; }
        public string PackageName { get; set; }
        public decimal PackageSMS { get; set; }
        public decimal SMSPrice { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal PackagePrice { get; set; }
        public string PaymentMethodId { get; set; }
        public string PaymentMethodName { get; set; }
        public decimal PayCoin { get; set; }
        public int Status { get; set; } //enum DepositStatus (in Commons)
        public string DepositNo { get; set; }
    }
}
