using Raje.DL.Services.DAL.Model;

namespace Raje.DL.Services.DAL.DataAccess
{
    public interface ICustomQuery<T> : ICustomQueryStart<T>, ICustomQueryExecuter<T>
        where T : IEntity
    {
    }
}