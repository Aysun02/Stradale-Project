using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class OfferPhoto : BaseEntity
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public int OfferId { get; set; }
        public Offer Offer { get; set; }
    }
}
