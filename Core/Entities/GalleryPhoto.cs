using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class GalleryPhoto : BaseEntity
    {
        public string Name { get; set; }
        public int Order { get; set; }
        //public int GalleryElementId { get; set; }
        public GalleryElement GalleryElement { get; set; }
    }
}
