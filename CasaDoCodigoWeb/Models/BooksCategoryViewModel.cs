using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RajeWeb.Models
{
    public class BooksCategoryViewModel
    {
        public string CategoryName { get; set; }
        public Dictionary<string, IEnumerable<BookViewModel>> Items { get; set; }
    }
}
