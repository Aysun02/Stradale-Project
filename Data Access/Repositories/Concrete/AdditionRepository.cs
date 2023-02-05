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
    public class AdditionRepository : Repository<Addition>, IAdditionRepository
    {
        private readonly AppDbContext _context;

        public AdditionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Addition>> GetAllWithCategoryAsync()
        {
            return await _context.Additions.Include(a => a.Category).ToListAsync();
        }
    }
}
