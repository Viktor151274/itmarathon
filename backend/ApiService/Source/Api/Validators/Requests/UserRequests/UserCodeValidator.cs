using FluentValidation;

namespace Epam.ItMarathon.ApiService.Api.Validators.Requests.UserRequests
{
    public class UserCodeValidator : AbstractValidator<string>
    {
        public UserCodeValidator()
        {
            RuleFor(code => code)
                .NotEmpty()
                .WithName("userCode")
                .WithMessage("Query Parameter 'userCode' is required.");
        }
    }
}