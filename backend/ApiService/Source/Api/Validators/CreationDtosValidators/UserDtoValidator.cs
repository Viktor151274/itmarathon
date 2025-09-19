using Epam.ItMarathon.ApiService.Api.Dto.CreationDtos;
using Epam.ItMarathon.ApiService.Api.Validators.Common;
using FluentValidation;

namespace Epam.ItMarathon.ApiService.Api.Validators.CreationDtosValidators
{
    public class UserDtoValidator : AbstractValidator<UserCreationDto>
    {
        public UserDtoValidator()
        {
            #region FirstName

            RuleFor(user => user.FirstName).NotEmpty().WithMessage(ValidationConstants.RequiredMessage)
                .WithName("firstName")
                .OverridePropertyName("firstName");

            #endregion

            #region LastName

            RuleFor(user => user.LastName).NotEmpty().WithMessage(ValidationConstants.RequiredMessage)
                .WithName("lastName")
                .OverridePropertyName("lastName");

            #endregion

            #region Phone

            RuleFor(user => user.Phone).NotEmpty().WithMessage(ValidationConstants.RequiredMessage)
                .WithName("phone")
                .OverridePropertyName("phone");

            #endregion

            #region Email

            RuleFor(user => user.Email).EmailAddress()
            .When(x => !string.IsNullOrEmpty(x.Email))
            .WithMessage("Email must be valid if provided.")
            .WithName("email")
            .OverridePropertyName("email");

            #endregion

            #region DeliveryInfo

            RuleFor(user => user.DeliveryInfo).NotEmpty().WithMessage(ValidationConstants.RequiredMessage)
                .WithName("deliveryInfo")
                .OverridePropertyName("deliveryInfo");

            #endregion
        }
    }
}
