namespace Epam.ItMarathon.ApiService.Domain.ValueObjects.Wish
{
    public sealed record Wish
    {
        internal const int NameCharLimit = 40;

        public string? Name { get; init; }
        public string? InfoLink { get; init; }

        private Wish(string? name, string? infoLink)
        {
            Name = name;
            InfoLink = infoLink;
        }

        internal static Wish Create(string? name, string? infoLink)
            => new(name, infoLink);
    }
}