using Raje.DL.DB.Admin;
using Raje.DL.Request.Admin;
using Raje.DL.Request.Admin.Base;
using Raje.DL.Services.DAL.Model;
using AutoMapper;
using Raje.DL.Response.Adm;

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

            CreateMap<UserSearchResponse, UserReportResponse>();

            CreateMap<User, UserReportResponse>()
                .ForMember(dest => dest.PerfilName, opts => opts.MapFrom(src => src.UserRole.Name))
                .ForMember(dest => dest.FlagActive, opts => opts.MapFrom(src => src.FlagActive ? "Ativo" : "Inativo"));
        }
    }
}