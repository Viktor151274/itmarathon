using AutoMapper;
using Epam.ItMarathon.ApiService.Domain.ValueObjects.Wish;
using Epam.ItMarathon.ApiService.Infrastructure.Database.Models.Gift;

namespace Epam.ItMarathon.ApiService.Infrastructure.Database.Models.AutoMapper
{
    internal class GiftMappingProfile : Profile
    {
        public GiftMappingProfile()
        {
            CreateMap<Wish, GiftEf>()
                .ForMember(giftEf => giftEf.Name, opt => opt.MapFrom(wish => wish.Name))
                .ForMember(giftEf => giftEf.InfoLink, opt => opt.MapFrom(wish => wish.InfoLink))
                .ForMember(giftEf => giftEf.CreatedOn, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(giftEf => giftEf.ModifiedOn, opt => opt.MapFrom(_ => DateTime.UtcNow));
            CreateMap<GiftEf, Wish>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(user => user.Name))
                .ForMember(dest => dest.InfoLink, opt => opt.MapFrom(user => user.InfoLink));
        }
    }
}
