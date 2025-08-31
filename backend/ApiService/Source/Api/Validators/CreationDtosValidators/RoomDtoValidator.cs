using Epam.ItMarathon.ApiService.Api.Dto.CreationDtos;
using FluentValidation;
using System.Globalization;

namespace Epam.ItMarathon.ApiService.Api.Validators.CreationDtosValidators
{
    public class RoomDtoValidator : AbstractValidator<RoomDto>
    {
        public RoomDtoValidator()
        {
            #region Name

            RuleFor(room => room.Name).NotEmpty().WithMessage("This field is required.");
            RuleFor(room => room.Name).MaximumLength(40).WithMessage("Maximum length is 40.");

            #endregion

            #region Description

            RuleFor(room => room.Description).NotEmpty().WithMessage("This field is required.");
            RuleFor(room => room.Description).MaximumLength(200).WithMessage("Maximum length is 200.");

            #endregion

            #region GiftExchangeDate

            RuleFor(room => room.GiftExchangeDate).NotEmpty().WithMessage("This field is required.");
            RuleFor(room => room.GiftExchangeDate)
                .Must(date => DateTimeOffset.TryParseExact(
                    date,
                    "yyyy-MM-dd'T'HH:mm:ss'z'",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
                    out var dateTime
                    ) && dateTime.Date >= DateTimeOffset.UtcNow.Date)
                .WithMessage("Timestamp must be a valid UTC ISO 8601 date and not before today.");

            #endregion

            #region GiftMaximumBudget

            RuleFor(room => room.GiftMaximumBudget).NotNull().WithMessage("This field is required.");
            RuleFor(room => room.GiftMaximumBudget).GreaterThanOrEqualTo(ulong.MinValue);

            #endregion

            #region Admin

            RuleFor(room => room.AdminUser).NotNull().WithMessage("This field is required.");
            RuleFor(room => room.AdminUser).SetValidator(new UserDtoValidator());

            #endregion
        }
    }
}
