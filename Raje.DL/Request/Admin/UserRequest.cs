using Raje.DL.Request.Admin.Base;
using System;

namespace Raje.DL.Request.Admin
{
    public class UserRequest : BaseRequest
    {
        public string FullName { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public DateTime BirthDate { get; set; }

        public string City { get; set; }

        public string State { get; set; }
    }
}
