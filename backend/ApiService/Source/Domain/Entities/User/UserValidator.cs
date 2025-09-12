using Epam.ItMarathon.ApiService.Domain.ValueObjects.Wish;
using FluentValidation;

namespace Epam.ItMarathon.ApiService.Domain.Entities.User
{
    internal class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            FirstNameValidation();
            LastNameValidation();
            EmailValidation();
            DeliveryInfoValidation();
            InterestsValidation();
            WishListValidation();
        }
        private void FirstNameValidation() =>
            RuleFor(user => user.FirstName).MaximumLength(User.FirstNameCharLimit).WithMessage($"Maximum length is {User.FirstNameCharLimit}.")
                .WithName("firstName")
                .OverridePropertyName("firstName");
        private void LastNameValidation() =>
            RuleFor(user => user.LastName).MaximumLength(User.LastNameCharLimit).WithMessage($"Maximum length is {User.LastNameCharLimit}.")
                .WithName("lastName")
                .OverridePropertyName("lastName");
        public void EmailValidation() =>
            RuleFor(user => user.Email).EmailAddress()
            .When(x => !string.IsNullOrEmpty(x.Email))
            .WithMessage("Email must be valid if provided.")
            .WithName("email")
            .OverridePropertyName("email");

        private void DeliveryInfoValidation() =>
            RuleFor(user => user.DeliveryInfo).MaximumLength(User.DeliveryInfoCharLimit).WithMessage($"Maximum length is {User.DeliveryInfoCharLimit}.")
                .WithName("deliveryInfo")
                .OverridePropertyName("deliveryInfo");

        private void InterestsValidation()
        {
            RuleFor(user => user.Interests).NotEmpty().When(user => user.WantSurprise)
                .WithMessage("Interests should be provided if user does want surprise.")
                .WithName("interests")
                .OverridePropertyName("interests");
            RuleFor(user => user.Interests).Empty().When(user => !user.WantSurprise)
                .WithMessage("Interests should not be provided if user does not want surprise.")
                .WithName("interests")
                .OverridePropertyName("interests");
        }

        private void WishListValidation()
        {
            RuleForEach(user => user.Wishes).NotEmpty().SetValidator(new WishValidator())
                .When(user => !user.WantSurprise)
                .WithMessage("Wishes should be provided if user does not want surprise.")
                .WithName("wishList")
                .OverridePropertyName("wishList");
            RuleForEach(user => user.Wishes).Empty().When(user => user.WantSurprise)
                .WithMessage("Wishes should not be provided if user want surprise.")
                .WithName("wishList")
                .OverridePropertyName("wishList");
        }
    }
}
