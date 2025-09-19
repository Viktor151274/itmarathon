using AutoMapper;
using Epam.ItMarathon.ApiService.Infrastructure.Database.Models.User;

namespace Epam.ItMarathon.ApiService.Infrastructure.Database.Models.AutoMapper
{
    internal class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<Domain.Entities.User.User, UserEf>()
                .ForMember(userEf => userEf.Id, opt => opt.MapFrom(user => user.Id))
                .ForMember(userEf => userEf.CreatedOn, opt => opt.MapFrom(user => user.CreatedOn))
                .ForMember(userEf => userEf.ModifiedOn, opt => opt.MapFrom(user => user.ModifiedOn))
                .ForMember(userEf => userEf.RoomId, opt => opt.MapFrom(user => user.RoomId))
                .ForMember(userEf => userEf.AuthCode, opt => opt.MapFrom(user => user.AuthCode))
                .ForMember(userEf => userEf.FirstName, opt => opt.MapFrom(user => user.FirstName))
                .ForMember(userEf => userEf.Phone, opt => opt.MapFrom(user => user.Phone))
                .ForMember(userEf => userEf.Email, opt => opt.MapFrom(user => user.Email))
                .ForMember(userEf => userEf.DeliveryInfo, opt => opt.MapFrom(user => user.DeliveryInfo))
                .ForMember(userEf => userEf.WantSurprise, opt => opt.MapFrom(user => user.WantSurprise))
                .ForMember(userEf => userEf.Interests, opt => opt.MapFrom(user => user.Interests))
                .ForMember(userEf => userEf.TargetGift, opt => opt.MapFrom(user => user.Gift))
                .ForMember(userEf => userEf.Wishes, opt => opt.MapFrom(user => user.Wishes));

            CreateMap<UserEf, Domain.Entities.User.User>()
                .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(user => user.IsAdminForRoom!.AdminId == user.Id))
                .ForMember(userEf => userEf.Gift, opt => opt.MapFrom(user => user.TargetGift));
        }
    }
}
