using System.Linq;

namespace EntepriseArchitecture.Data.GenericRepository
{
    /// <summary>
    /// It is necessary to apply the Generic Repository Design Pattern in order not to 
    /// define different classes for the CRUD operations that occur in the same way on every table in the database.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// It is the method to be used for query operations.
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// It is used to query data from database by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Find(int id);

        /// <summary>
        /// It is used to add a new record to the database.
        /// </summary>
        /// <param name="entity"></param>
        void Insert(TEntity entity);

        /// <summary>
        /// It is used to update a record in the database.
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// It is used to delete a record in the database.
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);
    }
}