using Epam.ItMarathon.ApiService.Api.Dto.CreationDtos;
using FluentValidation;

namespace Epam.ItMarathon.ApiService.Api.Validators.CreationDtosValidators
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            #region FirstName

            RuleFor(user => user.FirstName).NotEmpty().WithMessage("This field is required.");
            RuleFor(user => user.FirstName).MaximumLength(40).WithMessage("Maximum length is 40.");

            #endregion

            #region LastName

            RuleFor(user => user.LastName).NotEmpty().WithMessage("This field is required.");
            RuleFor(user => user.LastName).MaximumLength(40).WithMessage("Maximum length is 40.");

            #endregion

            #region Phone

            RuleFor(user => user.Phone).NotEmpty().WithMessage("This field is required.");
            RuleFor(x => x.Phone)
            .Matches(@"^\+380\d{9}$")
            .WithMessage("Phone number must be a valid Ukrainian number.");

            #endregion

            #region Email

            RuleFor(user => user.Email).EmailAddress()
            .When(x => !string.IsNullOrEmpty(x.Email))
            .WithMessage("Email must be valid if provided.");

            #endregion

            #region DeliveryInfo

            RuleFor(user => user.DeliveryInfo).NotEmpty().WithMessage("This field is required.");
            RuleFor(user => user.DeliveryInfo).MaximumLength(500).WithMessage("Maximum length is 500.");

            #endregion

            #region WantSurprise

            RuleFor(user => user.WantSurprise).NotEmpty().WithMessage("This field is required.");

            #endregion

            #region Interests

            RuleFor(user => user.Interests).NotEmpty().When(user => user.WantSurprise)
                .WithMessage("Interests should be provided if user does want surprise.");
            RuleFor(user => user.Interests).Empty().When(user => !user.WantSurprise)
                .WithMessage("Interests should not be provided if user does not want surprise.");

            #endregion

            #region WishList

            RuleForEach(user => user.WishList).NotEmpty().SetValidator(new WishDtoValidator())
                .When(user => !user.WantSurprise)
                .WithMessage("Wishes should be provided if user does not want surprise.");
            RuleForEach(user => user.WishList).Empty().When(user => user.WantSurprise)
                .WithMessage("Wishes should not be provided if user want surprise.");

            #endregion
        }
    }
}
