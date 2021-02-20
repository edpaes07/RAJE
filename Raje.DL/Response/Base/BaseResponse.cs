using System;

namespace Raje.DL.Response.Base
{
    public class BaseResponse : IBaseResponse
    {
        public long Id { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public bool FlagActive { get; set; }
    }
}
