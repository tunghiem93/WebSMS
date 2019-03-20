using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Entity.Entity
{
    public class CMS_API : CMS_EntityBase
    {
        public string Id { get; set; }
        public string LinkAPI { get; set; }
        public string APIName { get; set; }
        public int APIType { get; set; }
        public string Description { get; set; }
    }
}
