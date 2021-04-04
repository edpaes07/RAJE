using Raje.DL.DB.Admin;
using Raje.DL.Request.Admin;
using Raje.DL.Request.Admin.Base;
using Raje.DL.Services.DAL.Model;
using AutoMapper;
using Raje.DL.Response.Adm;

namespace Raje.BLL.Mapper
{
    public class LogProfile : Profile
    {
        public LogProfile()
        {
            CreateMap<Log, LogResponse>();
            CreateMap<Log, LogReportResponse>()
                .ForMember(dest => dest.FlagActive, opts => opts.MapFrom(src => src.FlagActive ? "Ativo" : "Inativo"));

            CreateMap<LogRequest, Log>()
                .ForMember(dest => dest.CreatedBy, opts => opts.Ignore())
                .ForMember(dest => dest.CreatedAt, opts => opts.Ignore())
                .ForMember(dest => dest.ModifiedBy, opts => opts.Ignore())
                .ForMember(dest => dest.ModifiedAt, opts => opts.Ignore());

            CreateMap<IPageEntity<LogResponse>, BaseSearchResponse<LogResponse>>();

            CreateMap<IPageEntity<LogSearchResponse>, BaseSearchResponse<LogSearchResponse>>();
        }
    }
}