using Raje.DL.Services.DAL.Model;
using System;
using System.Linq.Expressions;

namespace Raje.DL.Services.DAL.DataAccess
{
    public interface ICustomQueryWhere<T>
    where T : IEntity
    {

        ICustomQueryExecuter<T> Where(Expression<Func<T, bool>> filterExpression);
    }
}
