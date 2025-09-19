using Epam.ItMarathon.ApiService.Api.Dto.CreationDtos;
using Epam.ItMarathon.ApiService.Api.Validators.Common;
using FluentValidation;
using System.Globalization;

namespace Epam.ItMarathon.ApiService.Api.Validators.CreationDtosValidators
{
    public class RoomDtoValidator : AbstractValidator<RoomCreationDto>
    {
        public RoomDtoValidator()
        {
            #region Name

            RuleFor(room => room.Name).NotEmpty().WithMessage(ValidationConstants.RequiredMessage)
                .WithName("name")
                .OverridePropertyName("name");

            #endregion

            #region Description

            RuleFor(room => room.Description).NotEmpty().WithMessage(ValidationConstants.RequiredMessage)
                .WithName("description")
                .OverridePropertyName("description");

            #endregion

            #region GiftExchangeDate

            RuleFor(room => room.GiftExchangeDate).NotEmpty().WithMessage(ValidationConstants.RequiredMessage)
                .WithName("giftExchangeDate")
                .OverridePropertyName("giftExchangeDate");
            RuleFor(room => room.GiftExchangeDate)
                .Must(DateValidators.DateNotPastUtcIso)
                .WithMessage("Timestamp must be a valid UTC ISO 8601.")
                .WithName("giftExchangeDate")
                .OverridePropertyName("giftExchangeDate");

            #endregion

            #region GiftMaximumBudget

            RuleFor(room => room.GiftMaximumBudget).NotNull().WithMessage(ValidationConstants.RequiredMessage)
                .WithName("giftMaximumBudget")
                .OverridePropertyName("giftMaximumBudget");

            #endregion

        }
    }
}
