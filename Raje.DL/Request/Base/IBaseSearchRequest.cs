using System.Collections.Generic;

namespace Raje.DL.Request.Admin.Base
{
    public interface IBaseSearchRequests
    {
        public IEnumerable<long> Ids { get; set; }

        public bool? FlagActive { get; set; }

        int PageIndex { get; set; }
        int PageSize { get; set; }
    }
}