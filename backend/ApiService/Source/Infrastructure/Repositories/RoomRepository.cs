using System.Linq.Expressions;
using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.Aggregate.Room;

namespace Epam.ItMarathon.ApiService.Infrastructure.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        public Task<Result<Room>> AddAsync(Room Item)
        {
            throw new NotImplementedException();
        }

        public Task AddManyAsync(IEnumerable<Room> Items)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(ulong Id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteManyAsync(IEnumerable<ulong> Ids)
        {
            throw new NotImplementedException();
        }

        public Task<Room?> GetByIdAsync<TItem>(ulong Id, Expression<Func<Room, TItem>>? includeExpression = null)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Room>> GetManyAsync<TOrder, TInclude>(Expression<Func<Room, bool>>? filterExpression = null, Expression<Func<Room, TOrder>>? orderExpression = null, Expression<Func<Room, TInclude>>? includeExpression = null, int? items = null)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Room Item)
        {
            throw new NotImplementedException();
        }

        public Task UpdateManyAsync(IEnumerable<Room> Items)
        {
            throw new NotImplementedException();
        }
    }
}
