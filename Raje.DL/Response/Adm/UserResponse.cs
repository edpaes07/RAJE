using Raje.DL.DB.Admin;
using Raje.DL.Response.Base;
using System.Collections.Generic;

namespace Raje.DL.Response.Adm
{
    public class UserResponse : BaseResponse
    {
        public string Name { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Cpf { get; set; }

        public long UserRoleId { get; set; }

        public long CompanyId { get; set; }


        public UserRole UserRole { get; set; }
    }
}
