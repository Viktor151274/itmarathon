namespace Epam.ItMarathon.ApiService.Domain.Entities
{
    public class BaseEntity
    {
        public ulong Id { get; init; }

        protected BaseEntity()
        {

        }

        protected BaseEntity(ulong id)
        {
            Id = id;
        }
    }
}