using System.Collections.Generic;
using System.Linq;
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
            CreateMap<ToyUpdateDto, Toy>()
                .ConstructUsing(x => ConstructToyFromToyCreateDto(x))
                .ForMember(x => x.Category, opt => opt.Ignore())
                .ForMember(x => x.Images, opt => opt.Ignore());

            CreateMap<ToyImage, ToyImageDto>()
                .ReverseMap();
        }

        private static Toy ConstructToyFromToyCreateDto(ToyUpdateDto source)
        {
            var dest = new Toy {Images = new HashSet<ToyImage>()};
            var mainImage = source.Images.FirstOrDefault();

            if (mainImage != null)
            {
                dest.Images.Add(new ToyImage {IsMain = true, PublicId = mainImage.PublicId, Format = mainImage.Format});
                dest.Images.UnionWith(source.Images.Skip(1)
                    .Select(x => new ToyImage {Format = x.Format, PublicId = x.PublicId}));
            }

            return dest;
        }
    }
}
