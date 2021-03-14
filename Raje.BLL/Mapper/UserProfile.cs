using Raje.DL.DB.Admin;
using Raje.DL.Request.Admin;
using Raje.DL.Request.Admin.Base;
using Raje.DL.Response.Admin;
using Raje.DL.Services.DAL.Model;
using AutoMapper;
using System.Linq;
using Raje.DL.Request.Base;

namespace Raje.BLL.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserSearchResponse>();

            CreateMap<UserRequest, User>();

            CreateMap<User, UserResponse>();

            CreateMap<IPageEntity<UserSearchResponse>, BaseSearchResponse<UserSearchResponse>>();

            CreateMap<IPageEntity<User>, BaseSearchResponse<UserSearchResponse>>();
        }
    }
}
