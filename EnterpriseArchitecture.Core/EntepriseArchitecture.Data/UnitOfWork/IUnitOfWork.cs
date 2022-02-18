using EntepriseArchitecture.Data.GenericRepository;
using System;

namespace EntepriseArchitecture.Data.UnitOfWork
{
    /// <summary>
    /// Unit Of Work design pattern allows to perform a batch operation on the database and
    /// to undo the operation in case of a possible error.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        /// <summary>
        /// Undo changes if there is a problem during the save.
        /// </summary>
        void Rollback();

        /// <summary>
        /// Save the changes.
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// Start the transaction.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Stop Transaction if there is no problem during registration.
        /// </summary>
        void Commit();
    }
}