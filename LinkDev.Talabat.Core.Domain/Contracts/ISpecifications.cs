﻿using LinkDev.Talabat.Core.Domain.Common;
using System.Linq.Expressions;

namespace LinkDev.Talabat.Core.Domain.Contracts
{
    public interface ISpecifications<TEntity, TKey>
        where TEntity : BaseAuditableEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public Expression<Func<TEntity, bool>>? Criteria { get; set; }  // P => P.Id == 10 

        public List<Expression<Func<TEntity,object>>> Includes { get; set; }
    }
}