using EntepriseArchitecture.Data.Context;
using EntepriseArchitecture.Data.GenericRepository;
using System;
using System.Data.Entity;

namespace EntepriseArchitecture.Data.UnitOfWork
{
    /// <summary>
    /// Unit Of Work design pattern allows to perform a batch operation on the database 
    /// and to undo the operation in case of a possible error.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EnterpriseArchitectureContext _context;
        DbContextTransaction _transaction;
        private bool _disposed = false;

        public UnitOfWork(EnterpriseArchitectureContext context)
        {
            Database.SetInitializer<EnterpriseArchitectureContext>(null);

            if (context == null)
            {
                throw new ArgumentException("Context is null!");
            }
            else
            {
                _context = context;
            }
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new GenericRepository<TEntity>(_context);
        }

        public int SaveChanges()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
    }
}