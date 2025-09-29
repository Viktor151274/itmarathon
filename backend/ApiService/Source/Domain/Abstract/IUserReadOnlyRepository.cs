using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Domain.Entities.User;
using FluentValidation.Results;

namespace Epam.ItMarathon.ApiService.Domain.Abstract
{
    /// <summary>
    /// Readonly repository for <see cref="User"/> aggregate.
    /// </summary>
    public interface IUserReadOnlyRepository
    {
        /// <summary>
        /// Get user by unique user code
        /// </summary>
        /// <param name="userCode">Unique user code</param>
        /// <param name="includeRoom">Include dependent room to response</param>
        /// <param name="includeWishes">Include list of dependent wishes to response</param>
        /// <returns>Returns <see cref="User"/> if found, otherwise <see cref="ValidationResult"/></returns>
        public Task<Result<User, ValidationResult>> GetByCodeAsync(string userCode, bool includeRoom = false, bool includeWishes = false);
        /// <summary>
        /// Get user by unique user id
        /// </summary>
        /// <param name="id">Unique user id</param>
        /// <param name="includeRoom">Include dependent room to response</param>
        /// <param name="includeWishes">Include list of dependent wishes to response</param>
        /// <returns>Returns <see cref="User"/> if found, otherwise <see cref="ValidationResult"/></returns>
        public Task<Result<User, ValidationResult>> GetByIdAsync(ulong id, bool includeRoom = false, bool includeWishes = false);
        /// <summary>
        /// Get all users in room by room id
        /// </summary>
        /// <param name="roomId">Unique room id</param>
        /// <returns>Returns list of <see cref="User"/> if found, otherwise <see cref="ValidationResult"/></returns>
        public Task<Result<List<User>, ValidationResult>> GetManyByRoomIdAsync(ulong roomId);
    }
}
