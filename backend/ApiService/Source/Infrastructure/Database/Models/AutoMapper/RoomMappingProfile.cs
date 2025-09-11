using AutoMapper;
using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Infrastructure.Database.Models.Room;
using FluentValidation.Results;

namespace Epam.ItMarathon.ApiService.Infrastructure.Database.Models.AutoMapper
{
    internal class RoomMappingProfile : Profile
    {
        public RoomMappingProfile()
        {
            CreateMap<Domain.Aggregate.Room.Room, RoomEf>()
                .ForMember(roomEf => roomEf.Id, opt => opt.MapFrom(room => room.Id))
                .ForMember(roomEf => roomEf.CreatedOn, opt => opt.MapFrom(room => room.CreatedOn))
                .ForMember(roomEf => roomEf.ModifiedOn, opt => opt.MapFrom(room => room.ModifiedOn))
                .ForMember(roomEf => roomEf.ClosedOn, opt => opt.MapFrom(room => room.ClosedOn))
                .ForMember(roomEf => roomEf.InvitationCode, opt => opt.MapFrom(room => room.InvitationCode))
                .ForMember(roomEf => roomEf.MinUsersLimit, opt => opt.MapFrom(room => room.MinUsersLimit))
                .ForMember(roomEf => roomEf.MaxUsersLimit, opt => opt.MapFrom(room => room.MaxUsersLimit))
                .ForMember(roomEf => roomEf.MaxWishesLimit, opt => opt.MapFrom(room => room.MaxWishesLimit))
                .ForMember(roomEf => roomEf.Name, opt => opt.MapFrom(room => room.Name))
                .ForMember(roomEf => roomEf.Description, opt => opt.MapFrom(room => room.Description))
                .ForMember(roomEf => roomEf.InvitationNote, opt => opt.MapFrom(room => room.InvitationNote))
                .ForMember(roomEf => roomEf.GiftExchangeDate, opt => opt.MapFrom(room => room.GiftExchangeDate))
                .ForMember(roomEf => roomEf.GiftMaximumBudget, opt => opt.MapFrom(room => room.GiftMaximumBudget))
                .ForMember(roomEf => roomEf.Users, opt => opt.MapFrom(room => room.Users));

            CreateMap<RoomEf, Result<Domain.Aggregate.Room.Room, ValidationResult>> ()
                .ConvertUsing<RoomConverter>();
        }
    }
}
