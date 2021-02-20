using System.Collections.Generic;

namespace Raje.DL.Response.Base
{
    public interface IBaseSearchResponse<T>
         where T : IBaseResponse
    {
        int PageIndex { get; set; }

        int PageSize { get; set; }

        IEnumerable<T> Data { get; set; }

        int? TotalItens { get; set; }

        int? TotalPages { get; set; }

        bool LastPage { get; set; }
    }
}