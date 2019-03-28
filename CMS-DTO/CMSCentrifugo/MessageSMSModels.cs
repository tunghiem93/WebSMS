using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSCentrifugo
{
    public class MessageSMSModels
    {
        public string id { get; set; }
        public string phone { get; set; }
        public string text { get; set; }
        public string operatorName { get; set; }
    }
}
