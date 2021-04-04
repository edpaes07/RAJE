using Raje.DL.DB.Admin;
using Raje.DL.Request.Admin;
using Raje.DL.Request.Admin.Base;
using Raje.DL.Services.DAL.Model;
using AutoMapper;
using Raje.DL.Response.Adm;

namespace Raje.BLL.Mapper
{
    public class ContentsProfile : Profile
    {
        public ContentsProfile()
        {
            CreateMap<Contents, ContentsSearchResponse>()
            .ForMember(dest => dest.Media, opts => opts.MapFrom(src => src.Media.FilePath));

            CreateMap<ContentsRequest, Contents>();

            CreateMap<Contents, ContentsResponse>()
            .ForMember(dest => dest.Media, opts => opts.MapFrom(src => src.Media.FilePath));

            CreateMap<IPageEntity<ContentsSearchResponse>, BaseSearchResponse<ContentsSearchResponse>>();

            CreateMap<IPageEntity<Contents>, BaseSearchResponse<ContentsSearchResponse>>();
        }
    }
}