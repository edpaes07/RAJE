using Raje.DL.DB.Base;
using Raje.DL.Services.DAL.DataAccess;
using Raje.DL.Services.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raje.DAL.EF.Base
{
    public class EFRepository<T> : IRepository<T>
    where T : EntityBase, new()
    {
        protected readonly DbContext _dbContext;

        public EFRepository(DbContext db)
        {
            _dbContext = db;
            //TODO: Melhorar isso - timeout em upload de arquivos grandes.
            if (_dbContext.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
            {
                _dbContext.Database.SetCommandTimeout(300);
            }
        }

        protected virtual void Set(T model, EntityState state)
        {
            if (state == EntityState.Added)
                _dbContext.Set<T>().Add(model);
            else if (state == EntityState.Modified)
                _dbContext.Set<T>().Update(model);
            else if (state == EntityState.Deleted)
                _dbContext.Set<T>().Remove(model);
        }

        protected virtual void Set(IEnumerable<T> models, EntityState state)
        {
            if (state == EntityState.Added)
                foreach (var model in models)
                {
                    if (model != null)
                        _dbContext.Set<T>().Add(model);
                }
            else if (state == EntityState.Modified)
                foreach (var model in models)
                {
                    if (model != null)
                        _dbContext.Set<T>().Update(model);
                }
            else if (state == EntityState.Deleted)
                foreach (var model in models)
                {
                    if (model != null)
                        _dbContext.Set<T>().Remove(model);
                }
        }

        protected virtual void SingleOperation(T model, EntityState state, bool autoDetectChangesEnabled = false)
        {
            //Verifica se existe algum item
            if (model == null)
                return;

            _dbContext.ChangeTracker.AutoDetectChangesEnabled = autoDetectChangesEnabled;

            //Define os valores padrões do item
            Set(model, state);

            _dbContext.SaveChanges();
        }

        protected virtual async Task SingleOperationAsync(T model, EntityState state, bool autoDetectChangesEnabled = false)
        {
            //Verifica se existe algum item
            if (model == null)
                return;

            _dbContext.ChangeTracker.AutoDetectChangesEnabled = autoDetectChangesEnabled;

            //Define os valores padrões do item
            Set(model, state);

            await _dbContext.SaveChangesAsync();
        }

        protected virtual void MultipleOperations(IEnumerable<T> models, EntityState state)
        {
            //Verifica se existe algum item
            if (models == null || !models.Any())
                return;

            _dbContext.ChangeTracker.AutoDetectChangesEnabled = false;

            Set(models, state);

            _dbContext.SaveChanges();
        }

        public virtual void Insert(T model, bool autoDetectChangesEnabled = false) => SingleOperation(model, EntityState.Added, autoDetectChangesEnabled);

        public virtual Task InsertAsync(T model, bool autoDetectChangesEnabled = false) => SingleOperationAsync(model, EntityState.Added, autoDetectChangesEnabled);

        public virtual void Insert(IEnumerable<T> models) => MultipleOperations(models, EntityState.Added);

        public virtual void Update(T model, bool autoDetectChangesEnabled = false) => SingleOperation(model, EntityState.Modified, autoDetectChangesEnabled);

        public virtual Task UpdateAsync(T model, bool autoDetectChangesEnabled = false) => SingleOperationAsync(model, EntityState.Modified, autoDetectChangesEnabled);

        public virtual void Update(IEnumerable<T> models) => MultipleOperations(models, EntityState.Modified);

        public virtual void Delete(T model) => SingleOperation(model, EntityState.Deleted);

        public virtual void Delete(IEnumerable<T> models) => MultipleOperations(models, EntityState.Deleted);

        public virtual T FindById<TKeyProp>(TKeyProp key)
        {
            return _dbContext.Set<T>().Find(key);
        }

        public virtual IEnumerable<T> FindByIds<TKeyProp>(IEnumerable<TKeyProp> keys)
        {
            var models = new List<T>();

            if (keys != null && keys.Any())
            {
                foreach (TKeyProp key in keys)
                {
                    var tempModel = _dbContext.Set<T>().Find(key);
                    if (tempModel != null)
                        models.Add(tempModel);
                }
            }

            return models;
        }

        public virtual ICustomQueryStart<T> Query()
        {
            return new EFCustomQuery<T>(this._dbContext);
        }

        private string CurrentUserName()
        {
            //return _currentPrincipal.Identity.Name;//TODO: Descomentar depois de implementar Identity
            return "usuario TESTE";
        }

        public virtual void UpdateAuditInfo(EntityAuditBase model, EnumEntityState state)
        {
            if (model == null)
                return;

            String usuarioAtual = CurrentUserName();
            switch (state)
            {
                case EnumEntityState.Inserir:

                    model.CreatedBy = usuarioAtual;
                    model.CreatedAt = DateTime.Now;
                    model.ModifiedBy = usuarioAtual;
                    model.ModifiedAt = DateTime.Now;
                    model.FlagActive = true;
                    break;

                case EnumEntityState.Atualizar:
                    model.ModifiedBy = usuarioAtual;
                    model.ModifiedAt = DateTime.Now;
                    break;

                case EnumEntityState.ExcluirLogico:
                    model.ModifiedBy = usuarioAtual;
                    model.ModifiedAt = DateTime.Now;
                    model.FlagActive = false;
                    break;
            }
        }
    }
}