using EntepriseArchitecture.Data.Context;
using System.Data.Entity;
using System.Linq;

namespace EntepriseArchitecture.Data.GenericRepository
{
    /// <summary>
    /// It is necessary to apply the Generic Repository Design Pattern in order not to define 
    /// different classes for the CRUD operations that occur in the same way on every table in the database.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly EnterpriseArchitectureContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(EnterpriseArchitectureContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }

        public TEntity Find(int id)
        {
            return _dbSet.Find(id);
        }

        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);
        }
    }
}