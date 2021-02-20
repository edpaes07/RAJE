using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Raje.DL.Services.DAL.DataAccess
{
    public interface ICustomQueryWhere<T>
    where T : IEntity
    {

        ICustomQueryExecuter<T> Where(Expression<Func<T, bool>> filterExpression);
    }
}
