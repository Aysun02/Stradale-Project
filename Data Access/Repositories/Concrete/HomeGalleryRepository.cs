using Core.Entities;
using Data_Access.Contexts;
using Data_Access.Repositories.Abstarct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositories.Concrete
{
    public class HomeGalleryRepository : Repository<HomeGallery>, IHomeGalleryRepository
    {
        public HomeGalleryRepository(AppDbContext context) : base(context)
        {
        }
    }
}