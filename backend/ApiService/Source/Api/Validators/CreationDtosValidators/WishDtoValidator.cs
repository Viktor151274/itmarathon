using Epam.ItMarathon.ApiService.Api.Dto.CreationDtos;
using FluentValidation;

namespace Epam.ItMarathon.ApiService.Api.Validators.CreationDtosValidators
{
    public class WishDtoValidator : AbstractValidator<WishDto>
    {
        public WishDtoValidator()
        {
            #region Name

            RuleFor(wish => wish.Name).NotEmpty().WithMessage("This field is required.");
            RuleFor(wish => wish.Name).MaximumLength(40).WithMessage("Maximum length is 40.");

            #endregion
        }
    }
}
