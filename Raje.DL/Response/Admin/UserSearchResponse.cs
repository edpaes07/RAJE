using Raje.DL.Response.Base;
using Raje.DL.Services.DAL;
using System;

namespace Raje.DL.Response.Admin
{
    public class UserSearchResponse : BaseResponse, IEntity
    {
        public string FullName { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public DateTime BirthDate { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public int Grade { get; set; }

        public string Commentary { get; set; }
    }
}
