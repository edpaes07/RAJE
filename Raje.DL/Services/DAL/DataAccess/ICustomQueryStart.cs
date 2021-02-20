using System;
using System.Collections.Generic;
using System.Text;

namespace Raje.DL.Services.DAL.DataAccess
{
    public interface ICustomQueryStart<T> : ICustomQueryWhere<T>
        where T : IEntity
    {
        ICustomQueryExecuter<T> All();
    }
}