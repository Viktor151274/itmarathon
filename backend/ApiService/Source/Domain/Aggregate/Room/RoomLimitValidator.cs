using FluentValidation;

namespace Epam.ItMarathon.ApiService.Domain.Aggregate.Room
{
    internal class RoomLimitValidator : AbstractValidator<Room>
    {
        public RoomLimitValidator()
        {
            MaxWishesLimitValidation();
            MaxUsersLimitValidation();
        }
        private void MaxWishesLimitValidation() =>
        RuleForEach(room => room.Users)
            .Must((room, user) => (ulong)user.Wishes.Count() <= room.MaxWishesLimit)
            .WithMessage(user => $"User {user.Name} exceeds the max wishes limit.");
        private void MaxUsersLimitValidation() =>
            RuleFor(room => room.Users)
            .Must((room, user) => (ulong)user.Count() <= room.MaxUsersLimit)
            .WithMessage(user => $"User {user.Name} exceeds the max wishes limit.");
    }
}