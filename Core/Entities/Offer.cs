using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Offer : BaseEntity
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string CoffeeType1 { get; set; }
        public string Offer1 { get; set; }
        public string Photo1 { get; set; }
        public string CoffeeType2 { get; set; }
        public string Offer2 { get; set; }
        public string Photo2 { get; set; }
    }
}
