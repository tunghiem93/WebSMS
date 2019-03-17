using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSRate
{
    public class CMS_RateModels
    {
        public string Id { get; set; }
        public decimal Rate { get; set; }
        public int RateType { get; set; }// otp, marketing ....
        public int WaitingTime { get; set; }

        public CMS_RateModels()
        {
        }
    }
}
