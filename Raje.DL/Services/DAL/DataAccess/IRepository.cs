using System;
using System.Collections.Generic;
using System.Text;

namespace Raje.DL.Services.DAL.DataAccess
{
    public interface IRepository<T> : IRepositoryCRUD<T>, ICustomQueryRepository<T>
        where T : IEntity
    {
    }
}
