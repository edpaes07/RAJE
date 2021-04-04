using Raje.DL.Response.Base;
using Raje.DL.Services.DAL.Model;

namespace Raje.DL.Response.Adm
{
    public class UserSearchResponse : BaseResponse, IEntity
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }

        /// <summary>
        /// User Role Name
        /// </summary>
        public string NameUserRole { get; set; }

        /// <summary>
        /// Company Name
        /// </summary>
        public string NameCompany { get; set; }

        /// <summary>
        /// Agregate Name Stores separeted by ,  
        /// </summary>
        public string NameStores { get; set; }

        /// <summary>
        /// Agregate Name FranchiseeGroup separeted by ,  
        /// </summary>
        public string NameFranchiseeGroups { get; set; }

    }
}
