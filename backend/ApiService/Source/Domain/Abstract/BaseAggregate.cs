namespace Epam.ItMarathon.ApiService.Domain.Abstract
{
    public abstract class BaseAggregate : BaseEntity
    {
        protected BaseAggregate() : base() { }
        protected BaseAggregate(ulong id) : base(id) { }
    }
}
