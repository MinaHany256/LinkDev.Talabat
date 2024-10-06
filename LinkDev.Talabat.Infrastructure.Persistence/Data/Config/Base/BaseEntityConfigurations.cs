﻿using LinkDev.Talabat.Core.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data.Config.Base
{
    internal class BaseEntityConfigurations<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(E => E.Id)
                  .ValueGeneratedOnAdd();

            builder.Property(E => E.CreatedBy)
                .IsRequired();

            builder.Property(E => E.CreatedOn)
                .IsRequired();

            builder.Property(E => E.LastModifiedBy)
                .IsRequired();

            builder.Property(E => E.LastModifiedOn)
                .IsRequired();
        }
    }
}