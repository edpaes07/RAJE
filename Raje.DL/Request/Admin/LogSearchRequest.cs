using Raje.DL.Request.Admin.Base;
using System;

namespace Raje.DL.Request.Admin
{
    public class LogSearchRequest : BaseSearchRequest
    {
        public string Author { get; set; }

        public bool? Input { get; set; }

        public short? Code { get; set; }

        public string Api { get; set; }

        public string Request { get; set; }

        public string Response { get; set; }

        public string UrlQuery { get; set; }

        public string Method { get; set; }

        public DateTime? DateBegin { get; set; }

        public DateTime? DateEnd { get; set; }

    }
}
