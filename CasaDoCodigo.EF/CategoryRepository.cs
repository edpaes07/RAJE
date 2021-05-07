using Raje.Core.Repository;
using Raje.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Raje.EF
{
    public class CategoryRepository : RepositoryBase<Category, int>, ICategoryRepository
    {
        public CategoryRepository(RajeDbContext dbContext)
            : base(dbContext, e => e.Id)
        {
        }

        public override IQueryable<Category> Query
            => Set()
                .Include(b => b.Parent);
    }
}
