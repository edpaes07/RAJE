using System.Collections.Generic;

namespace Raje.DL.Services.DAL.Model
{
    public interface IPageEntity<T>
        where T : IEntity
    {
        int PageIndex { get; set; }
        int PageSize { get; set; }

        IEnumerable<T> Data { get; set; }

        int? TotalItens { get; set; }

        int? TotalPages { get; set; }

        bool LastPage { get; set; }
    }
}
