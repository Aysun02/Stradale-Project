using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class InfoPhoto : BaseEntity
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public int InfoId { get; set; }
        public Information Information { get; set; }
    }
}
