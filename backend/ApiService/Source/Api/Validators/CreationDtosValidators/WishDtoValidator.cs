using Epam.ItMarathon.ApiService.Api.Dto.CreationDtos;
using Epam.ItMarathon.ApiService.Api.Validators.Common;
using FluentValidation;

namespace Epam.ItMarathon.ApiService.Api.Validators.CreationDtosValidators
{
    public class WishDtoValidator : AbstractValidator<WishDto>
    {
        public WishDtoValidator()
        {
            #region Name

            RuleFor(wish => wish.Name).NotEmpty().WithMessage(ValidationConstants.RequiredMessage)
                .WithName("name")
                .OverridePropertyName("name");

            #endregion
        }
    }
}
