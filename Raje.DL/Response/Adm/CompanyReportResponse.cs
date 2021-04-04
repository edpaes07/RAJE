using Raje.DL.Response.Base;
using System.ComponentModel;

namespace Raje.DL.Response.Adm
{
    public class CompanyReportResponse : BaseReportResponse
    {

        [DisplayName("Id da Empresa")]
        public long Id { get; set; }

        [DisplayName("Nome da Empresa")]
        public string Name { get; set; }

        [DisplayName("Nome do Logo")]
        public string FileName { get; set; }

        [DisplayName("URL do Logo")]
        public string FilePath { get; set; }

    }
}
