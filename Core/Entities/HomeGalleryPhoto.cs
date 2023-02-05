using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class HomeGalleryPhoto : BaseEntity
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public int HomeGalleryId { get; set; }
        public HomeGallery HomeGallery { get; set; }
    }
}