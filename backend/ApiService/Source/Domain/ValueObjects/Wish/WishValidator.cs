using FluentValidation;

namespace Epam.ItMarathon.ApiService.Domain.ValueObjects.Wish
{
    internal class WishValidator : AbstractValidator<Wish>
    {
        public WishValidator()
        {
            ValidationForFields();
            NameRules();
            LinkRule();
        }
        private void ValidationForFields()
        {
            RuleFor(wish => wish)
                .Must(wish => !string.IsNullOrWhiteSpace(wish.Name)
                        || !string.IsNullOrWhiteSpace(wish.InfoLink))
                .WithMessage("At least one of Name, InfoLink, must be provided.");
        }
        private void NameRules() =>
            RuleFor(wish => wish.Name).MaximumLength(Wish.NameCharLimit).WithMessage($"Maximum length is {Wish.NameCharLimit}.")
               .WithName("name")
               .OverridePropertyName("name");

        private void LinkRule() =>
            RuleFor(wish => wish.InfoLink)
            .NotEmpty()
            .Must(url => url.StartsWith("https://"))
            .When(wish => !string.IsNullOrEmpty(wish.InfoLink))
            .WithMessage("URL must start with https://")
            .WithName("infoLink")
            .OverridePropertyName("infoLink");
    }
}
