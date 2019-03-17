using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSTime
{
    public class CMS_TimeModels
    {
        public string Id { get; set; }
        public int WaitingTime { get; set; }

        public CMS_TimeModels()
        {
            WaitingTime = 15; //value default
        }
    }
}
