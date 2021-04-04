using Raje.DL.Services.DAL.Model;
namespace Raje.DL.Services.DAL.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICustomQueryRepository<T>
        where T : IEntity
    {
        ICustomQueryStart<T> Query();
    }
}