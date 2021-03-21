using System.ComponentModel;

namespace Raje.Infra.Enums
{
    public enum UserRoleTypes
    {
        [Description("Admin Master")]
        AdminMaster = 1,
        [Description("Gestão")]
        Gestao = 2,
        [Description("Admin Loja")]
        AdminLoja = 3,
        [Description("Operador")]
        Operador = 4,
        [Description("Device")]
        Device = 5,
        [Description("Marketing")]
        Marketing = 6,
    }
}
