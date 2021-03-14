using Raje.DL.DB.Admin;
using Raje.DL.Request.Admin;
using Raje.DL.Request.Admin.Base;
using Raje.DL.Response.Admin;
using Raje.DL.Services.DAL.Model;
using AutoMapper;
using Raje.DL.Request.Base;

namespace Raje.BLL.Mapper
{
    public class MediaProfile : Profile
	{
		public MediaProfile()
		{
			CreateMap<MediaRequest, Media>()
					.ForMember(dest => dest.CreatedBy, opts => opts.Ignore())
					.ForMember(dest => dest.CreatedAt, opts => opts.Ignore())
					.ForMember(dest => dest.ModifiedBy, opts => opts.Ignore())
					.ForMember(dest => dest.ModifiedAt, opts => opts.Ignore())
					.ForMember(dest => dest.FilePath, opts => opts.Ignore());

			CreateMap<Media, MediaResponse>();

			CreateMap<IPageEntity<MediaSearchResponse>, BaseSearchResponse<MediaSearchResponse>>();

			CreateMap<IPageEntity<Media>, BaseSearchResponse<MediaResponse>>();
		}
	}
}
