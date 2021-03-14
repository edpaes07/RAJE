using System;

namespace Raje.DL.Request.Admin.Base
{

    public abstract class BaseAuditRequest : BaseRequest
    {
        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime ModifiedAt { get; set; }
    }
}