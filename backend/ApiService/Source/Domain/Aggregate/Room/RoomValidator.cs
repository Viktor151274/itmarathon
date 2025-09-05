using Epam.ItMarathon.ApiService.Domain.Entities.User;
using FluentValidation;

namespace Epam.ItMarathon.ApiService.Domain.Aggregate.Room
{
    internal class RoomValidator : AbstractValidator<Room>
    {
        public RoomValidator()
        {
            NameValidation();
            DescriptionValidation();
            GiftExchangeDateValidation();
            GiftMaximumBudgetValidation();
            UsersValidation();
        }
        private void NameValidation() =>
            RuleFor(room => room.Name).MaximumLength(40).WithMessage("Maximum length is 40.")
                .WithName("name")
                .OverridePropertyName("name");
        private void DescriptionValidation() =>
            RuleFor(room => room.Description).MaximumLength(200).WithMessage("Maximum length is 200.")
                .WithName("description")
                .OverridePropertyName("description");

        private void GiftExchangeDateValidation() => RuleFor(room => room.GiftExchangeDate)
                .Must(DateIsNotPast)
                .WithMessage("Timestamp must be not before today.")
                .WithName("giftExchangeDate")
                .OverridePropertyName("giftExchangeDate");
        private void GiftMaximumBudgetValidation() =>
            RuleFor(room => room.GiftMaximumBudget).GreaterThanOrEqualTo(ulong.MinValue)
                .WithName("giftMaximumBudget")
                .OverridePropertyName("giftMaximumBudget");
        private void UsersValidation() =>
            RuleForEach(room => room.Users).SetValidator(new UserValidator());

        private static bool DateIsNotPast(DateTime date) =>
            date >= DateTime.UtcNow.Date;
    }
}
