namespace Epam.ItMarathon.ApiService.Domain.ValueObjects.Wish
{
    public sealed class Wish : IEquatable<Wish>
    {
        internal const int NameCharLimit = 40;
        public required string Name { get; init; }
        public string? InfoLink { get; init; }
        public Wish() { }
        public static Wish Create(string name, string? infoLink)
        {
            return new Wish() { Name = name, InfoLink = infoLink };
        }
        public override bool Equals(object obj) => Equals(obj as Wish);
        public bool Equals(Wish? other)
        {
            if (other is null)
                return false;

            return string.Equals(Name, other.Name, StringComparison.Ordinal) &&
                   string.Equals(InfoLink, other.InfoLink, StringComparison.Ordinal);
        }
        public override int GetHashCode() => HashCode.Combine(Name, InfoLink);
        public static bool operator ==(Wish left, Wish right)
        {
            if (left is null && right is null) return true;
            if (left is null || right is null) return false;
            return left.Equals(right);
        }
        public static bool operator !=(Wish left, Wish right) => !(left == right);
    }
}
