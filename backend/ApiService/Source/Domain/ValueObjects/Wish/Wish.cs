using System.Text.Json;
using CSharpFunctionalExtensions;

namespace Epam.ItMarathon.ApiService.Domain.ValueObjects.Wish
{
    public class Wish
    {
        public const int NameCharLimit = 40;
        public required string Name { get; init; }
        public required string InfoLink { get; init; }
        private Wish() { }
        public Result<Wish> Create(string name, string infoLink)
        {
            var wish = new Wish() { Name = name, InfoLink = infoLink };
            var validator = new WishValidator();
            var validationResult = validator.Validate(wish);
            if (!validationResult.IsValid)
            {
                var errorDict = validationResult.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToList()
                    );

                var errorJson = JsonSerializer.Serialize(errorDict);
                return Result.Failure<Wish>(errorJson);
            }
            return Result.Success(wish);
        }
    }
}
