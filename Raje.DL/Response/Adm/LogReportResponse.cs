using Raje.DL.DB.Admin;
using Raje.DL.Response.Base;
using System;
using System.ComponentModel;

namespace Raje.DL.Response.Adm
{
    public class LogReportResponse : BaseReportResponse
    {
        [DisplayName("ID")]
        public long Id { get; set; }

        [DisplayName("Método")]
        public string Method { get; set; }

        [DisplayName("Api")]
        public string Api { get; set; }

        [DisplayName("Url")]
        public string UrlQuery { get; set; }

        [DisplayName("Input")]
        public bool Input { get; set; }

        [DisplayName("Code")]
        public short Code { get; set; }
    }
}
