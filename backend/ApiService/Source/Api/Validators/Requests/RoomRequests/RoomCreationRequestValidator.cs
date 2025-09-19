using Epam.ItMarathon.ApiService.Api.Dto.Requests.RoomRequests;
using Epam.ItMarathon.ApiService.Api.Validators.CreationDtosValidators;
using FluentValidation;

namespace Epam.ItMarathon.ApiService.Api.Validators.Requests.RoomRequests
{
    public class RoomCreationRequestValidator : AbstractValidator<RoomCreationRequest>
    {
        public RoomCreationRequestValidator()
        {
            RuleFor(roomRequest => roomRequest.Room).SetValidator(new RoomDtoValidator())
                .WithName("room")
                .OverridePropertyName("room");
            RuleFor(roomRequest => roomRequest.AdminUser).SetValidator(new UserDtoValidator())
                .WithName("adminUser")
                .OverridePropertyName("adminUser");
        }
    }
}