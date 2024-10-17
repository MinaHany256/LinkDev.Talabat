using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entites.Products;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using LinkDev.Talabat.Infrastructure.Persistence.Repositories;
using System.Collections.Concurrent;

namespace LinkDev.Talabat.Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _dbContext;
        private readonly ConcurrentDictionary<string, object> _repositories;

        public UnitOfWork(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new ();
        }

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : BaseAuditableEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            //return new GenericRepository<TEntity, TKey>(_dbContext);

            ///var TypeName = typeof(TEntity).Name;
            ///if(_repositories.ContainsKey(TypeName)) return (IGenericRepository<TEntity, TKey>) _repositories[TypeName];
            ///var repository = new GenericRepository<TEntity, TKey>(_dbContext);
            ///_repositories.Add(TypeName, repository);
            ///return repository;

            return (IGenericRepository<TEntity, TKey>) _repositories.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TEntity, TKey>(_dbContext));

        }

        public async Task<int> CompleteAsync() => await _dbContext.SaveChangesAsync();
       

        public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();
        

    }
}
