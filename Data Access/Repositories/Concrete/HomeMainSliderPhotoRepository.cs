﻿using Core.Entities;
using Data_Access.Contexts;
using Data_Access.Repositories.Concrete;
using DataAccess.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
    public class HomeMainSliderPhotoRepository : Repository<HomeMainSliderPhoto>,IHomeMainSliderPhotoRepsository
    {
        public HomeMainSliderPhotoRepository(AppDbContext context) : base(context)
        {
        }
    }
}
