using Raje.DL.DB.Base;
using Raje.DL.Services.DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Raje.DL.Services.DAL.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepositoryCRUD<T>
        where T : IEntity
    {
        void Insert(T model, bool autoDetectChangesEnabled = false);

        Task InsertAsync(T model, bool autoDetectChangesEnabled = false);

        void Insert(IEnumerable<T> models);

        void Update(T model, bool autoDetectChangesEnabled = false);

        Task UpdateAsync(T model, bool autoDetectChangesEnabled = false);

        void Update(IEnumerable<T> models);

        void Delete(T model);

        void Delete(IEnumerable<T> models);

        T FindById<TProp>(TProp key);

        IEnumerable<T> FindByIds<TProp>(IEnumerable<TProp> keys);

        void UpdateAuditInfo(EntityAuditBase model, EnumEntityState state);
    }
}