namespace Epam.ItMarathon.ApiService.Domain.Abstract
{
    /// <summary>
    /// Base class for implementing a factory for domain entities.
    /// </summary>
    /// <typeparam name="TFactory">Factory type for fluent chaining.</typeparam>
    public abstract class BaseEntityBuilder<TFactory> where TFactory : BaseEntityBuilder<TFactory>
    {
        protected ulong _id;
        protected DateTime _createdOn;
        protected DateTime _modifiedOn;
        /// <summary>
        /// (USED ONLY BY MAPPERS) Set an Id for entity.
        /// </summary>
        /// <param name="id">Unique identifier of entity.</param>
        /// <returns>Ref to current factory</returns>
        public TFactory WithId(ulong id)
        { 
            _id = id;
            return (TFactory)this;
        }
        /// <summary>
        /// (USED ONLY BY MAPPERS) Set a creation date for entity.
        /// </summary>
        /// <param name="createdOn">Creation date of entity.</param>
        /// <returns>Ref to current factory</returns>
        public TFactory WithCreatedOn(DateTime createdOn)
        {
            _createdOn = createdOn;
            return (TFactory)this;
        }
        /// <summary>
        /// Set a modification date.
        /// </summary>
        /// <param name="updatedOn">Date when entity was updated.</param>
        /// <returns>Ref to current factory</returns>
        public TFactory WithModifiedOn(DateTime updatedOn)
        {
            _modifiedOn = updatedOn;
            return (TFactory)this;
        }
    }
}
