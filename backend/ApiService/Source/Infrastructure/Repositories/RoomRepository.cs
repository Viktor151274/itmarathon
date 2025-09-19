using System.Linq.Expressions;
using AutoMapper;
using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.Aggregate.Room;
using Epam.ItMarathon.ApiService.Domain.Entities.User;
using Epam.ItMarathon.ApiService.Domain.Shared.ValidationErrors;
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
            var result = await GetByCodeAsync(room => room.Users.Any(user => user.AuthCode == userCode), true);
            return result;
        }

        public async Task<Result<Room, ValidationResult>> GetByRoomCodeAsync(string roomCode)
        {
            var result = await GetByCodeAsync(room => room.InvitationCode == roomCode, true);
            return result;
        }

        public async Task<Result<User, ValidationResult>> GetUserByUserCode(string userCode, bool includeRoom = false, bool includeWishes = false)
        {
            var userQuery = context.Users.AsQueryable();
            if (includeRoom)
            {
                userQuery = userQuery.Include(user => user.Room);
            }
            if (includeWishes)
            {
                userQuery = userQuery.Include(user => user.Wishes);
            }

            var userEf = await userQuery.FirstOrDefaultAsync(user => user.AuthCode.Equals(userCode));
            var result = userEf == null
                ? Result.Failure<User, ValidationResult>(new NotFoundError([
                    new ValidationFailure(nameof(userCode), "User with such code not found")
                ]))
                : mapper.Map<User>(userEf);
            return result;
        }

        public async Task<Result<User, ValidationResult>> GetUserById(ulong id, bool includeRoom = false, bool includeWishes = false)
        {
            var userQuery = context.Users.AsQueryable();
            if (includeRoom)
            {
                userQuery = userQuery.Include(user => user.Room);
            }
            if (includeWishes)
            {
                userQuery = userQuery.Include(user => user.Wishes);
            }

            var userEf = await userQuery.FirstOrDefaultAsync(user => user.Id.Equals(id));
            var result = userEf == null
                ? Result.Failure<User, ValidationResult>(new NotFoundError([
                    new ValidationFailure(nameof(id), "User with such id not found")
                ]))
                : mapper.Map<User>(userEf);
            return result;
        }

        public async Task<Result<List<User>, ValidationResult>> GetRoomUsersByRoomId(ulong roomId)
        {
            var usersEf = await context.Users
                .Include(user => user.Room)
                .Include(user => user.Wishes)
                .Where(user => user.RoomId == roomId)
                .ToListAsync();
            var result = usersEf.Count == 0
                ? Result.Failure<List<User>, ValidationResult>(new NotFoundError([
                    new ValidationFailure("roomId", "Room with such id not found0")
                ]))
                : mapper.Map<List<User>>(usersEf);
            return result;
        }

        private async Task<Result<Room, ValidationResult>> GetByCodeAsync(Expression<Func<RoomEf, bool>> codeExpression, bool includeUsers = false)
        {
            var roomQuery = context.Rooms.AsQueryable();
            if (includeUsers)
            {
                roomQuery = roomQuery.Include(room => room.Users).ThenInclude(user => user.Wishes);
            }

            var roomEf = await roomQuery.FirstOrDefaultAsync(codeExpression);
            var result = roomEf == null
                ? Result.Failure<Room, ValidationResult>(new NotFoundError([
                    new ValidationFailure("code", "Room with such code not found")
                ]))
                : mapper.Map<Result<Room, ValidationResult>>(roomEf);
            return result;
        }
    }
}
