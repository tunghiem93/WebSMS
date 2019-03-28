using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSDepositPackage
{
    public class PerfectMoneyModel
    {
        public string Id { get; set; }
        [Required]
        public string PAYEE_ACCOUNT { get; set; }
        [Required]
        public string PAYEE_NAME { get; set; }
        [Required]
        public decimal PAYMENT_AMOUNT { get; set; }
        [Required]
        public int PAYMENT_UNITS { get; set; }
        public string PAYMENT_ID { get; set; }
        public bool STATUS_URL { get; set; }
        public string PAYMENT_URL { get; set; }
        public string PAYMENT_URL_METHOD { get; set; }
        public string NOPAYMENT_URL { get; set; }
        public string NOPAYMENT_URL_METHOD { get; set; }
        public string BAGGAGE_FIELDS { get; set; }
        public string SUGGESTED_MEMO { get; set; }
        public string SUGGESTED_MEMO_NOCHANGE { get; set; }
        public string FORCED_PAYER_ACCOUNT { get; set; }
        public string AVAILABLE_PAYMENT_METHODS { get; set; }
        public string DEFAULT_PAYMENT_METHOD { get; set; }
        public string FORCED_PAYMENT_METHOD { get; set; }
        public string INTERFACE_LANGUAGE { get; set; }
    }
}
