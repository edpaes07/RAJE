using Raje.DL.DB.Base;
using Raje.DL.Services.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Raje.DAL.EF.Base
{
    /// <summary>
    /// Exemplo de repositório, considerando um entidade com campos padrões para auditoria.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EFRepositoryAudit<T> : EFRepository<T>
        where T : EntityAuditBase, new()
    {
        private IPrincipal _currentPrincipal;

        public EFRepositoryAudit(IPrincipal principal, DbContext db) : base(db)
        {
            _currentPrincipal = principal;
        }

        private string CurrentUserName()
        {
            return _currentPrincipal.Identity.Name ?? "Usuário não autenticado";
        }

        protected void UpdateAuditInfo(IEnumerable<T> models, EntityState state)
        {
            if (models != null && models.Any())
            {
                foreach (T model in models)
                    UpdateAuditInfo(model, state);
            }
        }

        protected void UpdateAuditInfo(T model, EntityState state)
        {
            if (model != null)
            {
                if (state == EntityState.Added)
                {
                    model.CreatedBy = CurrentUserName();
                    model.CreatedAt = DateTime.Now;
                    model.ModifiedBy = CurrentUserName();
                    model.ModifiedAt = DateTime.Now;
                }
                else if (state == EntityState.Modified || state == EntityState.Deleted)
                {
                    model.ModifiedBy = CurrentUserName();
                    model.ModifiedAt = DateTime.Now;

                    if (state == EntityState.Deleted)
                        model.FlagActive = false;
                }
            }
        }

        public override void UpdateAuditInfo(EntityAuditBase model, EnumEntityState state)
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

        public override void Insert(T model, bool autoDetectChangeEnabled = false)
        {
            UpdateAuditInfo(model, EntityState.Added);
            base.Insert(model, autoDetectChangeEnabled);
        }

        public override Task InsertAsync(T model, bool autoDetectChangeEnabled = false)
        {
            UpdateAuditInfo(model, EntityState.Added);
            return base.InsertAsync(model, autoDetectChangeEnabled);
        }

        public override void Insert(IEnumerable<T> model)
        {
            UpdateAuditInfo(model, EntityState.Added);
            base.Insert(model);
        }

        public override void Update(T model, bool autoDetectChangesEnabled = false)
        {
            UpdateAuditInfo(model, EntityState.Modified);
            base.Update(model, autoDetectChangesEnabled);
        }

        public override Task UpdateAsync(T model, bool autoDetectChangesEnabled = false)
        {
            UpdateAuditInfo(model, EntityState.Modified);
            return base.UpdateAsync(model, autoDetectChangesEnabled);
        }

        public override void Update(IEnumerable<T> model)
        {
            UpdateAuditInfo(model, EntityState.Modified);
            base.Update(model);
        }

        public override void Delete(T model)
        {
            UpdateAuditInfo(model, EntityState.Deleted);
            base.Update(model);
        }

        public override void Delete(IEnumerable<T> model)
        {
            UpdateAuditInfo(model, EntityState.Deleted);
            base.Update(model);
        }
    }
}