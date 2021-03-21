using Raje.DAL.EF;
using DocumentFormat.OpenXml.Presentation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Raje.Base.Tests._Builders
{
    public class EntityContextBuilder
    {
        protected string DataBaseName;

        public static EntityContextBuilder New()
        {
            return new EntityContextBuilder() { DataBaseName = Guid.NewGuid().ToString() };
        }

        public EntityContext Build()
        {
            var builder = new DbContextOptionsBuilder<EntityContext>();
            builder.UseInMemoryDatabase(databaseName: DataBaseName);
            var dbContextOptions = builder.Options;

            EntityContext entityContext = new EntityContext(dbContextOptions);

            entityContext.Database.EnsureDeleted();
            entityContext.Database.EnsureCreated();

            return entityContext;
        }
    }
}
