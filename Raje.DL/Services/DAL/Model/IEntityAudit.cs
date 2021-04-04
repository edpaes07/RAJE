using System;

namespace Raje.DL.Services.DAL.Model
{
    public interface IEntityAudit : IEntity
    {
        string CreatedBy { get; set; }

        DateTime CreatedAt { get; set; }

        string ModifiedBy { get; set; }

        DateTime ModifiedAt { get; set; }

    }
}