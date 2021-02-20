using System.Collections.Generic;

namespace Raje.DL.Request.Admin.Base
{
    public class BaseSearchRequest : IBaseSearchRequests
    {
        public IEnumerable<long> Ids { get; set; }

        public bool? FlagActive { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
