using System.Linq.Expressions;
using AutoMapper;
using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.Aggregate.Room;
using Epam.ItMarathon.ApiService.Infrastructure.Database;
using Epam.ItMarathon.ApiService.Infrastructure.Database.Models.Room;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace Epam.ItMarathon.ApiService.Infrastructure.Repositories
{
    internal class RoomRepository(AppDbContext context, IMapper mapper) : IRoomRepository
    {

        public async Task<Result<Room, ValidationResult>> AddAsync(Room item)
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                var adminAuthCode = item.Users.Where(user => user.IsAdmin).First().AuthCode;
                var roomEf = mapper.Map<RoomEf>(item);
                var adminEf = roomEf.Users.Where(user => user.AuthCode == adminAuthCode).FirstOrDefault();
                roomEf.Admin = null;
                await context.Rooms.AddAsync(roomEf);
                await context.SaveChangesAsync();
                roomEf.Admin = adminEf;
                roomEf.AdminId = adminEf.Id;
                await context.SaveChangesAsync();

                await transaction.CommitAsync();

                return mapper.Map<Result<Room, ValidationResult>>(roomEf);
            }
            catch (DbUpdateException e)
            {
                await transaction.RollbackAsync();
                throw;
            }
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
