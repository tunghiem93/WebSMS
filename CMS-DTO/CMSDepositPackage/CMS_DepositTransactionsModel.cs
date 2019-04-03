using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO
{
    public class CMS_DepositTransactionsModel
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
        public int Status { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string sStatus { get; set; }
        public string DepositNo { get; set; }
        public bool IsClose { get; set; }
        public decimal Price { get; set; }
        public CMS_DepositTransactionsModel()
        {
            this.IsActive = true;
        }
    }
}
