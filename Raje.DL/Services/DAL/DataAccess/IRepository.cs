using Raje.DL.Services.DAL.Model;

namespace Raje.DL.Services.DAL.DataAccess
{
    public interface IRepository<T> : IRepositoryCRUD<T>, ICustomQueryRepository<T>
        where T : IEntity
    {
    }
}
