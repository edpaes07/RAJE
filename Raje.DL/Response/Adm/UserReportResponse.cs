using Raje.DL.Response.Base;
using System.ComponentModel;

namespace Raje.DL.Response.Adm
{
    public class UserReportResponse : BaseReportResponse
    {

        [DisplayName("Id do Usuário")]
        public long Id { get; set; }

        [DisplayName("Nome")]
        public string Name { get; set; }

        [DisplayName("CPF")]
        public string Cpf { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Perfil")]
        public string PerfilName { get; set; }
    }
}
