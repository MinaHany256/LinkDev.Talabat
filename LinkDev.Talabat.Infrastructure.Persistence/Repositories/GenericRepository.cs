using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entites.Products;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Repositories
{
    internal class GenericRepository<TEntity, TKey>(StoreContext dbContext) : IGenericRepository<TEntity, TKey>
        where TEntity : BaseAuditableEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool AsNoTracking = false)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                if (typeof(TEntity) == typeof(Product))
                {
                    AsNoTracking? await dbContext.Set<Product>().Include(P => P.Brand).Include(C => C.Category).ToListAsync() : 
                              await dbContext.Set<Product>().AsNoTracking().ToListAsync();
                }

                AsNoTracking? await dbContext.Set<TEntity>().ToListAsync() : 
                              await dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
            }
        }



        public async Task<TEntity?> GetAsync(TKey id) => await dbContext.Set<TEntity>().FindAsync(id);


        public async Task AddAsync(TEntity entity) => await dbContext.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity) => dbContext.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity) => dbContext.Set<TEntity>().Update(entity);

    }
}
