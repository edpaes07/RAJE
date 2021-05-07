using AutoMapper;
using Raje.Model;
using RajeWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RajeWeb
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            MapBookViewModel();
        }

        private void MapBookViewModel()
        {
            CreateMap<Book, BookViewModel>()
                .ForMember(
                    dest => dest.Authors,
                    opt => opt.ResolveUsing(
                        src => string.Join(", ", src.BookAuthors.Select(b => b.Author.Name))
                    )
                ).ForMember(
                    dest => dest.Category,
                    opt => opt.ResolveUsing(
                        src => src.Category.Parent?.Name
                    )
                ).ForMember(
                    dest => dest.SubCategory,
                    opt => opt.ResolveUsing(
                        src => src.Category.Name
                    )
                );
        }
    }
}
