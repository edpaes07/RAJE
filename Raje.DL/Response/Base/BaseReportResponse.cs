using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Raje.DL.Response.Base
{
    public class BaseReportResponse : IBaseReportResponse
    {
        [DisplayName("Usuario Criacao")]
        public string CreatedBy { get; set; }

        [DisplayName("Data Criacao")]
        public DateTime CreatedAt { get; set; }

        [DisplayName("Usuario Alteracao")]
        public string ModifiedBy { get; set; }

        [DisplayName("Data Alteracao")]
        public DateTime? ModifiedAt { get; set; }

        [DisplayName("Status")]
        public string FlagActive { get; set; }
    }
}
