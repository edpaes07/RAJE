using Raje.Core.Repository;
using Raje.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Raje.EF
{
    public class AuthorRepository: RepositoryBase<Author, int>, IAuthorRepository
    {
        public AuthorRepository(RajeDbContext dbContext)
            : base(dbContext, e => e.Id)
        {
        }
    }
}
