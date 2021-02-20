using Raje.DL.Response.Base;
using System.Collections.Generic;

namespace Raje.DL.Request.Admin.Base
{
    public class BaseSearchResponse<T> : IBaseSearchResponse<T>
        where T : IBaseResponse
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<T> Data { get; set; }
        public int? TotalItens { get; set; }
        public int? TotalPages { get; set; }
        public bool LastPage { get; set; }
    }
}
