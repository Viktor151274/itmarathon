using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Domain.Aggregate.Room;
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
        /// <param name="roomToUpdate">Item to be updated.</param>
        public Task<Result> UpdateAsync(Room roomToUpdate);
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
    }
}
