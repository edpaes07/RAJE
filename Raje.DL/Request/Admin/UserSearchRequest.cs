using Raje.DL.Request.Admin.Base;
using System.Collections.Generic;

namespace Raje.DL.Request.Admin
{
    public class UserSearchRequest : BaseSearchRequest
    {
        public IEnumerable<long> StoreIds { get; set; }

        public IEnumerable<long> FranchiseeGroupIds { get; set; }

        public long? UserRoleId { get; set; }

        public string Cpf { get; set; }
    }
}
