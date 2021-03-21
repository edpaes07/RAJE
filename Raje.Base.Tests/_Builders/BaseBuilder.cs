using Raje.DL.Services.DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Raje.Base.Tests._Builders
{
    public abstract class BaseBuilder<T, Q> where T : IEntity
    {
        protected long Id;
        protected bool FlagActive;

        public abstract Q GetThis();

        public Q WithId(long id)
        {
            Id = id;
            return GetThis();
        }

        public Q WithFlagActive(bool flagActive)
        {
            FlagActive = flagActive;
            return GetThis();
        }

        public T Build(T model)
        {
            if (Id <= 0) return model;

            //TODO: Solução alternativa para deixar ainda private a propriedade (pensar em outra forma)
            var propertyInfo = model.GetType().GetProperty("Id");
            propertyInfo.SetValue(model, Convert.ChangeType(Id, propertyInfo.PropertyType), null);

            return model;
        }
    }
}
