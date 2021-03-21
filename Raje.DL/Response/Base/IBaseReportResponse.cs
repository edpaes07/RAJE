using System;

namespace Raje.DL.Response.Base
{
    public interface IBaseReportResponse
    {
        string CreatedBy { get; set; }
        DateTime CreatedAt { get; set; }
        string ModifiedBy { get; set; }
        DateTime? ModifiedAt { get; set; }
        string FlagActive { get; set; }
    }
}