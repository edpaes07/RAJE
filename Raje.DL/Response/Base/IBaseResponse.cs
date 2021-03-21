using System;

namespace Raje.DL.Response.Base
{
    public interface IBaseResponse
    {
        long Id { get; set; }
        string CreatedBy { get; set; }
        DateTime CreatedAt { get; set; }
        string ModifiedBy { get; set; }
        DateTime? ModifiedAt { get; set; }
        bool FlagActive { get; set; }
    }
}