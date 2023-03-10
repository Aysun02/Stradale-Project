using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositories.Abstarct
{
    public interface IDrinkRepository : IRepository<Drink>
    {
        Task<List<Drink>> GetAllWithCategoryAsync();
    }
}
