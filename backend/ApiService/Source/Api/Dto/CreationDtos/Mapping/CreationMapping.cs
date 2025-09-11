using System.Globalization;
using AutoMapper;
using Epam.ItMarathon.ApiService.Application.Models.Creation;
using Epam.ItMarathon.ApiService.Domain.Aggregate.Room;

namespace Epam.ItMarathon.ApiService.Api.Dto.CreationDtos.Mapping
{
    public class CreationMapping : Profile
    {
        public CreationMapping()
        {
            CreateMap<RoomShortDto, RoomApplication>()
                .ForMember(roomApplication => roomApplication.GiftExchangeDate, opt => opt 
                .MapFrom(roomDto => DateTime.Parse(
                    roomDto.GiftExchangeDate,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal
                )));
            CreateMap<UserDto, UserApplication>()
                .ForMember(userApplication => userApplication.Wishes, opt => opt
                .MapFrom(userDto => userDto.WishList.ToDictionary(wish => wish.Name, wish => wish.InfoLink)));

            CreateMap<Room, RoomDto>();
        }
    }
}
