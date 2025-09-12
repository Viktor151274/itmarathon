using FluentValidation;

namespace Epam.ItMarathon.ApiService.Domain.ValueObjects.Wish
{
    internal class WishValidator : AbstractValidator<Wish>
    {
        public WishValidator()
        {
            NameRules();
        }

        private void NameRules() =>
            RuleFor(wish => wish.Name).MaximumLength(Wish.NameCharLimit).WithMessage($"Maximum length is {Wish.NameCharLimit}.")
               .WithName("name")
               .OverridePropertyName("name");
    }
}
