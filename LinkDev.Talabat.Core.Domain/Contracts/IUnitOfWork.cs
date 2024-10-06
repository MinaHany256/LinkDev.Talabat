﻿using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Entites.Products;

namespace LinkDev.Talabat.Core.Domain.Contracts
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
                    where TEntity : BaseEntity<TKey>
                    where TKey : IEquatable<TKey>;
        Task<int> CompleteAsync();

    }
}