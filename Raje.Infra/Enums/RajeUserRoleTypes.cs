using System.ComponentModel;

namespace Raje.Infra.Enums
{
    public enum UserRoleTypes
    {
        [Description("User")]
        User = 1,
        [Description("Admin Master")]
        AdminMaster = 2
    }
}
