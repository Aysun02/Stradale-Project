using Core.Constants;
using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Drink : BaseEntity
    {
        public string DrinkName { get; set; }
        public double MediumPrice { get; set; }
        public double LargePrice { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ProductStatus Status { get; set; }
    }
}
