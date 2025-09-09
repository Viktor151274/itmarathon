using System.Linq.Expressions;
using AutoMapper;
using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.Aggregate.Room;
using Epam.ItMarathon.ApiService.Infrastructure.Database;
using Epam.ItMarathon.ApiService.Infrastructure.Database.Models.Room;

namespace Epam.ItMarathon.ApiService.Infrastructure.Repositories
{
    internal class RoomRepository(AppDbContext context, IMapper mapper) : IRoomRepository
    {

        public async Task<Result<Room>> AddAsync(Room Item)
        {
            var adminAuthCode = Item.Users.Where(user => user.IsAdmin).First().AuthCode;
            var roomEf = mapper.Map<RoomEf>(Item);
            var adminEf = roomEf.Users.Where(user => user.AuthCode == adminAuthCode).First();
            adminEf.Room = roomEf;
            adminEf.IsAdminForRoom = roomEf;
            await context.Rooms.AddAsync(roomEf);
            return mapper.Map<Room>(roomEf);
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
