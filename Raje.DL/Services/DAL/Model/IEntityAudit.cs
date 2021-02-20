using System;

namespace Raje.DL.Services.DAL
{
    public interface IEntityAudit : IEntity
    {
        string CreatedBy { get; set; }

        DateTime CreatedAt { get; set; }

        string ModifiedBy { get; set; }

        DateTime ModifiedAt { get; set; }

    }
}