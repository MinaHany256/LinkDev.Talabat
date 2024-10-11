using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entites.Products;
using LinkDev.Talabat.Core.Domain.Specifications.Product_Specs;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.Talabat.Infrastructure.Persistence.Repositories
{
    internal class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
        where TEntity : BaseAuditableEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly StoreContext _dbContext;

        public GenericRepository(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool AsNoTracking = false)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return (IEnumerable<TEntity>)(AsNoTracking?
                    await _dbContext.Set<Product>().Include(P => P.Brand).Include(C => C.Category).ToListAsync():
                    await _dbContext.Set<Product>().Include(P => P.Brand).Include(C => C.Category).AsNoTrackingWithIdentityResolution().ToListAsync());

            }

            return AsNoTracking? await _dbContext.Set<TEntity>().ToListAsync(): 
                    await _dbContext.Set<TEntity>().AsNoTrackingWithIdentityResolution().ToListAsync();
        }


        public async Task<TEntity?> GetAsync(TKey id)
        {
            if (typeof(TEntity) == typeof(Product))
                return await _dbContext.Set<Product>().Where(P => P.Id == 10).Include(P => P.Brand).Include(P => P.Category).FirstOrDefaultAsync() as TEntity;

            return await _dbContext.Set<TEntity>().FindAsync(id);

        }

        public async Task AddAsync(TEntity entity) => await _dbContext.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity) => _dbContext.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity) => _dbContext.Set<TEntity>().Update(entity);

        public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity, TKey> spec, bool AsNoTracking = false)
        {
            return await SpecificationsEvaluator<TEntity, TKey>.GetQuery(_dbContext.Set<TEntity>(), spec).ToListAsync();
        }

        public async Task<TEntity?> GetWithSpecAsync(ISpecifications<TEntity, TKey> spec)
        {
            return await SpecificationsEvaluator<TEntity, TKey>.GetQuery(_dbContext.Set<TEntity>(), spec).FirstOrDefaultAsync();
        }
    }
}
