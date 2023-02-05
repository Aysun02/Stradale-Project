using Core.Entities;
using Data_Access.Contexts;
using Data_Access.Repositories.Abstarct;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositories.Concrete
{
    public class DrinkRepository : Repository<Drink>, IDrinkRepository
    {
        private readonly AppDbContext _context;

        public DrinkRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Drink>> GetAllWithCategoryAsync()
        {
            return await _context.Drinks.Include(p => p.Category).ToListAsync();
        }
    }
}
