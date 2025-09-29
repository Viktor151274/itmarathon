using AutoMapper;
using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.Aggregate.Room;
using Epam.ItMarathon.ApiService.Domain.Shared.ValidationErrors;
using Epam.ItMarathon.ApiService.Infrastructure.Database;
using Epam.ItMarathon.ApiService.Infrastructure.Database.Models.Room;
using Epam.ItMarathon.ApiService.Infrastructure.Database.Models.Room.Extensions;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Epam.ItMarathon.ApiService.Infrastructure.Repositories
{
    internal class RoomRepository(AppDbContext context, IMapper mapper, ILogger<RoomRepository> logger) : IRoomRepository
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
                roomEf.AdminId = null;
                await context.Rooms.AddAsync(roomEf);
                await context.SaveChangesAsync();
                roomEf.Admin = adminEf;
                roomEf.AdminId = adminEf.Id;
                await context.SaveChangesAsync();

                await transaction.CommitAsync();

                return mapper.Map<Result<Room, ValidationResult>>(roomEf);
            }
            catch (DbUpdateException exception)
            {
                await transaction.RollbackAsync();
                logger.LogError(exception.ToString());
                throw;
            }
        }

        public async Task<Result> UpdateAsync(Room roomToUpdate)
        {
            var existingRoom = await context.Rooms
                .Include(room => room.Users)
                .ThenInclude(user => user.Wishes)
                .FirstOrDefaultAsync(room => room.Id == roomToUpdate.Id);

            if (existingRoom == null)
                return Result.Failure($"Room with Id={roomToUpdate.Id} not found.");

            var updatedRoomEf = mapper.Map<RoomEf>(roomToUpdate);

            try
            {
                context.Rooms.Update(existingRoom.SyncRoom(updatedRoomEf));
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                logger.LogError(exception.ToString());
                throw;
            }
            return Result.Success();
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
