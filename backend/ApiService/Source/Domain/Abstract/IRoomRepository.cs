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
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns <see cref="Room"/> if found, otherwise <see cref="ValidationResult"/></returns>
        public Task<Result<Room, ValidationResult>> AddAsync(Room item, CancellationToken cancellationToken);
        /// <summary>
        /// Update existing room in the repository.
        /// </summary>
        /// <param name="roomToUpdate">Item to be updated.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public Task<Result> UpdateAsync(Room roomToUpdate, CancellationToken cancellationToken);
        /// <summary>
        /// Get room by unique user code
        /// </summary>
        /// <param name="userCode">Unique user code</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns <see cref="Room"/> if found, otherwise <see cref="ValidationResult"/></returns>
        public Task<Result<Room, ValidationResult>> GetByUserCodeAsync(string userCode, CancellationToken cancellationToken);
        /// <summary>
        /// Get room by unique room code
        /// </summary>
        /// <param name="roomCode">Unique room code</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns <see cref="Room"/> if found, otherwise <see cref="ValidationResult"/></returns>
        public Task<Result<Room, ValidationResult>> GetByRoomCodeAsync(string roomCode, CancellationToken cancellationToken);
    }
}
