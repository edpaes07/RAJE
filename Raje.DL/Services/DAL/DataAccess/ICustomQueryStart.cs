using Raje.DL.Services.DAL.Model;

namespace Raje.DL.Services.DAL.DataAccess
{
    public interface ICustomQueryStart<T> : ICustomQueryWhere<T>
        where T : IEntity
    {
        ICustomQueryExecuter<T> All();
    }
}