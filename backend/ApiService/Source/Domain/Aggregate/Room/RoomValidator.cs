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
            LimitValidation();
            UsersValidation();
        }
        private void NameValidation() =>
            RuleFor(room => room.Name).MaximumLength(Room.NameCharLimit).WithMessage($"Maximum length is {Room.NameCharLimit}.")
                .WithName("name")
                .OverridePropertyName("name");
        private void DescriptionValidation() =>
            RuleFor(room => room.Description).MaximumLength(Room.DescriptionCharLimit).WithMessage($"Maximum length is {Room.DescriptionCharLimit}.")
                .WithName("description")
                .OverridePropertyName("description");

        private void GiftExchangeDateValidation() => RuleFor(room => room.GiftExchangeDate)
                .Must(DateIsNotPast)
                .WithMessage("Timestamp must be not before today.")
                .WithName("giftExchangeDate")
                .OverridePropertyName("giftExchangeDate");
        private void GiftMaximumBudgetValidation() =>
            RuleFor(room => room.GiftMaximumBudget).GreaterThanOrEqualTo(ulong.MinValue)
                .WithMessage("giftMaximumBudget must be greater or equal to 0.")
                .WithName("giftMaximumBudget")
                .OverridePropertyName("giftMaximumBudget");
        private void LimitValidation() =>
            RuleFor(room => room).SetValidator(new RoomLimitValidator())
            .WithName("limitsValidation")
            .OverridePropertyName("limitsValidation");
        private void UsersValidation() =>
            RuleForEach(room => room.Users).SetValidator(new UserValidator());
        private static bool DateIsNotPast(DateTime date) =>
            date >= DateTime.UtcNow.Date;
    }
}
