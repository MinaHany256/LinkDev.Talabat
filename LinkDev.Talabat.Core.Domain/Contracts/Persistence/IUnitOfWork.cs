using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Entites.Products;

namespace LinkDev.Talabat.Core.Domain.Contracts.Persistence
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
                    where TEntity : BaseAuditableEntity<TKey>
                    where TKey : IEquatable<TKey>;
        Task<int> CompleteAsync();

    }
}
