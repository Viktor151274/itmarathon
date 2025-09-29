using AutoMapper;
using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.Entities.User;
using Epam.ItMarathon.ApiService.Domain.Shared.ValidationErrors;
using Epam.ItMarathon.ApiService.Infrastructure.Database;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace Epam.ItMarathon.ApiService.Infrastructure.Repositories
{
    internal class UserReadOnlyRepository(AppDbContext context, IMapper mapper) : IUserReadOnlyRepository
    {
        /// <inheritdoc/>
        public async Task<Result<User, ValidationResult>> GetByCodeAsync(string userCode, bool includeRoom = false, bool includeWishes = false)
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
        /// <inheritdoc/>
        public async Task<Result<User, ValidationResult>> GetByIdAsync(ulong id, bool includeRoom = false, bool includeWishes = false)
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
        /// <inheritdoc/>
        public async Task<Result<List<User>, ValidationResult>> GetManyByRoomIdAsync(ulong roomId)
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
    }
}