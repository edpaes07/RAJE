using Raje.DL.Services.DAL;
using Raje.DL.Services.DAL.Model;
using System.Collections.Generic;

namespace Raje.DAL.EF.Base
{
    public class EFPageEntity<T> : IPageEntity<T>
    where T : IEntity
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public IEnumerable<T> Data { get; set; }

        public int? TotalItens { get; set; }

        public int? TotalPages { get; set; }

        public bool LastPage { get; set; }
    }
}