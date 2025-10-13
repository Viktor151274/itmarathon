using AutoMapper;
using Epam.ItMarathon.ApiService.Infrastructure.Database.Models.User;

namespace Epam.ItMarathon.ApiService.Infrastructure.Database.Models.AutoMapper
{
    internal class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<Domain.Entities.User.User, UserEf>()
                .ForMember(userEf => userEf.TargetGift, opt => opt.MapFrom(user => user.Gift));
            CreateMap<UserEf, Domain.Entities.User.User>()
                .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(user => user.IsAdminForRoom!.AdminId == user.Id))
                .ForMember(userEf => userEf.Gift, opt => opt.MapFrom(user => user.TargetGift));
        }
    }
}
