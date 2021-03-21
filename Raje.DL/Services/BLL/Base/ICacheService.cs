using System;
using System.Threading.Tasks;

namespace Raje.DL.Services.BLL.Base
{
    public interface ICacheService
    {
        void Set<T>(string key, T obj);
        ICacheItem<T> Get<T>(string key);
        void Clean(string key);
        T GetOrSet<T>(string key, Func<T> GetData);
        Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> GetData);
    }
}
