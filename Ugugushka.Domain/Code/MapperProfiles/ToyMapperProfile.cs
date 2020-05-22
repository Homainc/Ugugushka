using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Ugugushka.Data.Models;
using Ugugushka.Domain.DtoModels;

namespace Ugugushka.Domain.Code.MapperProfiles
{
    public class ToyMapperProfile : Profile
    {

        public ToyMapperProfile()
        {
            CreateMap<Toy, ToyDto>();
            CreateMap<ToyCreateDto, Toy>()
                .ConstructUsing(x => ConstructToyFromToyCreateDto(x))
                .ForMember(x => x.Images, opt => opt.Ignore());
        }

        private Toy ConstructToyFromToyCreateDto(ToyCreateDto source)
        {
            var dest = new Toy { Images = new HashSet<ToyImage>() };
            var mainImage = source.ImageUrls.FirstOrDefault();

            if (mainImage != null)
            {
                dest.Images.Add(new ToyImage {IsMain = true, Url = source.ImageUrls.FirstOrDefault()});
                dest.Images.UnionWith(source.ImageUrls.Skip(1).Select(x => new ToyImage {Url = x}));
            }

            return dest;
        }
    }
}
