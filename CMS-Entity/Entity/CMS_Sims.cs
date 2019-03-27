using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Entity.Entity
{
    public class CMS_Sims : CMS_EntityBase
    {
        public string Id { get; set; }
        public string SimName { get; set; }
        public string SimNumber { get; set; }
        public string OperatorName { get; set; }
        public int Status { get; set; }

    }
}
