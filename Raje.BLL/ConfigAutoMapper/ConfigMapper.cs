using Raje.DL.DB.Admin;
using Raje.DL.Request.Admin;
using Raje.DL.Request.Admin.Base;
using Raje.DL.Response.Adm;
using Raje.DL.Services.DAL.Model;
using AutoMapper;

namespace Raje.BLL.ConfigAutoMapper
{
    public static class ConfigMapper
    {
        public static IMapper Configure()
        {
            return new MapperConfiguration(cfg =>
            {

                #region [User]

                cfg.CreateMap<UserRequest, User>();
                cfg.CreateMap<User, UserResponse>();
                cfg.CreateMap<IPageEntity<UserSearchResponse>, BaseSearchResponse<UserSearchResponse>>();

                cfg.CreateMap<UserSearchResponse, UserReportResponse>();

                cfg.CreateMap<User, UserReportResponse>()
                    .ForMember(dest => dest.PerfilName, opts => opts.MapFrom(src => src.UserRole.Name))
                    .ForMember(dest => dest.FlagActive, opts => opts.MapFrom(src => src.FlagActive ? "Ativo" : "Inativo"));

                #endregion

                #region [Log]

                cfg.CreateMap<Log, LogResponse>();
                cfg.CreateMap<Log, LogReportResponse>()
                    .ForMember(dest => dest.FlagActive, opts => opts.MapFrom(src => src.FlagActive ? "Ativo" : "Inativo"));

                cfg.CreateMap<LogRequest, Log>()
                    .ForMember(dest => dest.CreatedBy, opts => opts.Ignore())
                    .ForMember(dest => dest.CreatedAt, opts => opts.Ignore())
                    .ForMember(dest => dest.ModifiedBy, opts => opts.Ignore())
                    .ForMember(dest => dest.ModifiedAt, opts => opts.Ignore());

                cfg.CreateMap<IPageEntity<LogResponse>, BaseSearchResponse<LogResponse>>();

                cfg.CreateMap<IPageEntity<LogSearchResponse>, BaseSearchResponse<LogSearchResponse>>();

                #endregion
               
            }).CreateMapper();
        }
    }
}
