using Raje.Core.Repository;
using Raje.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Raje.EF
{
    public class BookRepository : RepositoryBase<Book, int>, IBookRepository
    {
        public BookRepository(RajeDbContext dbContext)
            : base(dbContext, e => e.Id)
        {
        }

        public override IQueryable<Book> Query
            => Set()
                .Include(b => b.Category)
                .Include(b => b.BookAuthors)
                .ThenInclude(a => a.Author);
    }
}
