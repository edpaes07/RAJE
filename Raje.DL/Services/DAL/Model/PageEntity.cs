using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Raje.DL.Services.DAL.Model
{
    public class PageEntity<T> : IPageEntity<T>
        where T : IEntity
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<T> Data { get; set; }
        public int? TotalItens { get; set; }
        public int? TotalPages { get; set; }
        public bool LastPage { get; set; }

        public PageEntity()
        {

        }

        public PageEntity(int pageIndex, int pageSize, IEnumerable<T> result, int? count = null)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;

            int length = result.Count();

            if (!count.HasValue)
            {
                if (length == pageSize + 1)
                {
                    this.Data = result.Take(pageSize);
                    this.LastPage = false;
                }
                else
                {
                    this.Data = result;
                    this.LastPage = true;
                }
            }
            else
            {
                this.Data = result;
                this.TotalItens = count;
                this.TotalPages = pageSize == 0 ? pageSize : (int)Math.Ceiling((double)count.Value / pageSize);
                this.LastPage = pageIndex + 1 == this.TotalPages;
            }
        }
    }
}
