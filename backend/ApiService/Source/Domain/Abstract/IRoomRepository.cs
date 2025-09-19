using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Domain.Aggregate.Room;
using Epam.ItMarathon.ApiService.Domain.Entities.User;
using FluentValidation.Results;

namespace Epam.ItMarathon.ApiService.Domain.Abstract
{
    /// <summary>
    /// Repository for <see cref="Room"/> aggregate.
    /// </summary>
    public interface IRoomRepository
    {
        /// <summary>
        /// Add new room to the repository.
        /// </summary>
        /// <param name="item">Item to add</param>
        /// <returns>Returns <see cref="Room"/> if found, otherwise <see cref="ValidationResult"/></returns>
        public Task<Result<Room, ValidationResult>> AddAsync(Room item);
        /// <summary>
        /// Update existing room in the repository.
        /// </summary>
        /// <param name="item">Item to be updated.</param>
        public Task UpdateAsync(Room item);
        /// <summary>
        /// Get room by unique user code
        /// </summary>
        /// <param name="userCode">Unique user code</param>
        /// <returns>Returns <see cref="Room"/> if found, otherwise <see cref="ValidationResult"/></returns>
        public Task<Result<Room, ValidationResult>> GetByUserCodeAsync(string userCode);
        /// <summary>
        /// Get room by unique room code
        /// </summary>
        /// <param name="roomCode">Unique room code</param>
        /// <returns>Returns <see cref="Room"/> if found, otherwise <see cref="ValidationResult"/></returns>
        public Task<Result<Room, ValidationResult>> GetByRoomCodeAsync(string roomCode);
        /// <summary>
        /// Get user by unique user code
        /// </summary>
        /// <param name="userCode">Unique user code</param>
        /// <param name="includeRoom">Include dependent room to response</param>
        /// <param name="includeWishes">Include list of dependent wishes to response</param>
        /// <returns>Returns <see cref="User"/> if found, otherwise <see cref="ValidationResult"/></returns>
        public Task<Result<User, ValidationResult>> GetUserByUserCode(string userCode, bool includeRoom = false, bool includeWishes = false);
        /// <summary>
        /// Get user by unique user id
        /// </summary>
        /// <param name="id">Unique user id</param>
        /// <param name="includeRoom">Include dependent room to response</param>
        /// <param name="includeWishes">Include list of dependent wishes to response</param>
        /// <returns>Returns <see cref="User"/> if found, otherwise <see cref="ValidationResult"/></returns>
        public Task<Result<User, ValidationResult>> GetUserById(ulong id, bool includeRoom = false, bool includeWishes = false);
        /// <summary>
        /// Get all users in room by room id
        /// </summary>
        /// <param name="roomId">Unique room id</param>
        /// <returns>Returns list of <see cref="User"/> if found, otherwise <see cref="ValidationResult"/></returns>
        public Task<Result<List<User>, ValidationResult>> GetRoomUsersByRoomId(ulong roomId);
    }
}
