using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSDepositPackage
{
    public class PriceObjects
    {
        public string symbol { get; set; }
        public decimal? priceChange { get; set; }
        public decimal? priceChangePercent { get; set; }
        public decimal? weightedAvgPrice { get; set; }
        public decimal? prevClosePrice { get; set; }
        public decimal? lastPrice { get; set; }
        public decimal? last { get; set; }
    }
}
