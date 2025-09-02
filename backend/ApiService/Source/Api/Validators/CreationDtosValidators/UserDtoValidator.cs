using Epam.ItMarathon.ApiService.Api.Dto.CreationDtos;
using Epam.ItMarathon.ApiService.Api.Validators.Common;
using FluentValidation;

namespace Epam.ItMarathon.ApiService.Api.Validators.CreationDtosValidators
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            #region FirstName

            RuleFor(user => user.FirstName).NotEmpty().WithMessage(ValidationConstants.RequiredMessage)
                .WithName("firstName")
                .OverridePropertyName("firstName");
            RuleFor(user => user.FirstName).MaximumLength(40).WithMessage("Maximum length is 40.")
                .WithName("firstName")
                .OverridePropertyName("firstName");

            #endregion

            #region LastName

            RuleFor(user => user.LastName).NotEmpty().WithMessage(ValidationConstants.RequiredMessage)
                .WithName("lastName")
                .OverridePropertyName("lastName");
            RuleFor(user => user.LastName).MaximumLength(40).WithMessage("Maximum length is 40.")
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
            RuleFor(user => user.DeliveryInfo).MaximumLength(500).WithMessage("Maximum length is 500.")
                .WithName("deliveryInfo")
                .OverridePropertyName("deliveryInfo");

            #endregion

            #region WantSurprise

            RuleFor(user => user.WantSurprise).NotEmpty().WithMessage(ValidationConstants.RequiredMessage)
                .WithName("wantSurprise")
                .OverridePropertyName("wantSurprise");

            #endregion

            #region Interests

            RuleFor(user => user.Interests).NotEmpty().When(user => user.WantSurprise)
                .WithMessage("Interests should be provided if user does want surprise.")
                .WithName("interests")
                .OverridePropertyName("interests");
            RuleFor(user => user.Interests).Empty().When(user => !user.WantSurprise)
                .WithMessage("Interests should not be provided if user does not want surprise.")
                .WithName("interests")
                .OverridePropertyName("interests");

            #endregion

            #region WishList

            RuleForEach(user => user.WishList).NotEmpty().SetValidator(new WishDtoValidator())
                .When(user => !user.WantSurprise)
                .WithMessage("Wishes should be provided if user does not want surprise.")
                .WithName("wishList")
                .OverridePropertyName("wishList");
            RuleForEach(user => user.WishList).Empty().When(user => user.WantSurprise)
                .WithMessage("Wishes should not be provided if user want surprise.")
                .WithName("wishList")
                .OverridePropertyName("wishList");

            #endregion
        }
    }
}
