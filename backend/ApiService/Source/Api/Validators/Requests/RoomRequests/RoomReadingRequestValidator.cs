using Epam.ItMarathon.ApiService.Api.Dto.Requests.RoomRequests;

using FluentValidation;

namespace Epam.ItMarathon.ApiService.Api.Validators.Requests.RoomRequests
{
    public class RoomReadingRequestValidator : AbstractValidator<RoomReadingRequest>
    {
        public RoomReadingRequestValidator()
        {
            RuleFor(request => request)
                .Must(request => string.IsNullOrEmpty(request.UserCode) != string.IsNullOrEmpty(request.RoomCode))
                .WithMessage("Only one code (either userCode or roomCode) should be provided")
                .WithName("code")
                .OverridePropertyName("code");
        }
    }
}
