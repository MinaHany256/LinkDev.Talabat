using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Entites.Products;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data
{
    public class StoreContext : DbContext
    {

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductBrand> Brands { get; set; }
        public DbSet<ProductCategory> Categories { get; set; }

        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
            

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyInformation).Assembly);
        }

        //public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        //{

        //    foreach (var entry in this.ChangeTracker.Entries<BaseAuditableEntity<int>>()
        //        .Where(entity => entity.State is EntityState.Added or EntityState.Modified))
        //    {
        //        if (entry.State is EntityState.Added or EntityState.Modified)
        //        {
        //            entry.Entity.CreatedBy = "";
        //            entry.Entity.CreatedOn = DateTime.UtcNow;   
        //        }
        //            entry.Entity.LastModifiedBy = "";
        //            entry.Entity.LastModifiedOn = DateTime.UtcNow;

        //        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        //    }
        //}

    }
}
