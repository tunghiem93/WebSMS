using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSCentrifugo
{
    public class MainSMSModels
    {
        public object messages { get; set; }
        public string callbackURL { get; set; }
        public int delay { get; set; }
    }
}
