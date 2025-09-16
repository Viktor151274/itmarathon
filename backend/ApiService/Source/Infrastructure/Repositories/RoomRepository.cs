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

        public Task UpdateAsync(Room item)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Room, ValidationResult>> GetByUserCodeAsync(string userCode)
        {
            var result = await GetByCodeAsync(r => r.Users.Any(u => u.AuthCode == userCode), true);
            return result;
        }

        public async Task<Result<Room, ValidationResult>> GetByRoomCodeAsync(string roomCode)
        {
            var result = await GetByCodeAsync(r => r.InvitationCode == roomCode, true);
            return result;
        }

        private async Task<Result<Room, ValidationResult>> GetByCodeAsync(Expression<Func<RoomEf, bool>> codeExpression, bool includeUsers = false)
        {
            var query = context.Rooms.AsQueryable();
            if (includeUsers)
            {
                query = query.Include(r => r.Users).ThenInclude(u => u.Wishes);
            }

            var roomEf = await query.FirstOrDefaultAsync(codeExpression);
            var result = roomEf == null
                ? Result.Failure<Room, ValidationResult>(new ValidationResult(new[]
                    { new ValidationFailure("code", "Room with such code not found") }))
                : mapper.Map<Result<Room, ValidationResult>>(roomEf);
            return result;
        }
    }
}
