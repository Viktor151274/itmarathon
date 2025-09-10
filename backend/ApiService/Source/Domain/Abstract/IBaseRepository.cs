using System.Linq.Expressions;
using Epam.ItMarathon.ApiService.Domain.Entities;

namespace Epam.ItMarathon.ApiService.Domain.Abstract
{
    /// <summary>
    /// Basic abstract interface for repositories. Works with <see cref="BaseEntity"/> objects.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Gets item by Id, possible to include some mapped properties using expression.
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="Id">Id of required entity.</param>
        /// <param name="includeExpression">Include expression.</param>
        /// <returns>Returns generic entity of the repository.</returns>
        public Task<TEntity?> GetByIdAsync<TItem>(ulong Id, Expression<Func<TEntity, TItem>>? includeExpression = null);
        /// <summary>
        /// Returns items of repository. Could be configured using parameters. If no specified returns all.
        /// </summary>
        /// <typeparam name="TOrder">Type of order.</typeparam>
        /// <typeparam name="TInclude">Property of include.</typeparam>
        /// <param name="filterExpression">Filter entities.</param>
        /// <param name="orderExpression">Specify the order of entities to return.</param>
        /// <param name="includeExpression">Include expression</param>
        /// <param name="items">Amount of items to return.</param>
        /// <returns>Returns IQueryable amount of TEntity</returns>
        public Task<IQueryable<TEntity>> GetManyAsync<TOrder, TInclude>(Expression<Func<TEntity, bool>>? filterExpression = null,
            Expression<Func<TEntity, TOrder>>? orderExpression = null, Expression<Func<TEntity, TInclude>>? includeExpression = null, int? items = null);
        /// <summary>
        /// Add entities to repository.
        /// </summary>
        /// <param name="Item">Item to add</param>
        /// <returns></returns>
        public Task AddAsync(TEntity Item);
        /// <summary>
        /// Adds list of entities to repository.
        /// </summary>
        /// <param name="Items">Items to add.</param>
        /// <returns></returns>
        public Task AddManyAsync(IEnumerable<TEntity> Items);
        /// <summary>
        /// Update existing item in the repository.
        /// </summary>
        /// <param name="Item">Item to be updated.</param>
        /// <returns></returns>
        public Task UpdateAsync(TEntity Item);
        /// <summary>
        /// Update list existing item in the repository.
        /// </summary>
        /// <param name="Items">List of items to be updated.</param>
        /// <returns></returns>
        public Task UpdateManyAsync(IEnumerable<TEntity> Items);
        /// <summary>
        /// Deletes existing item from the repository.
        /// </summary>
        /// <param name="Id">Id of item to be deleted.</param>
        /// <returns></returns>
        public Task DeleteAsync(ulong Id);
        /// <summary>
        /// Deletes existing items from the repository.
        /// </summary>
        /// <param name="Ids">Ids of items to be deleted.</param>
        /// <returns></returns>
        public Task DeleteManyAsync(IEnumerable<ulong> Ids);
    }
}
