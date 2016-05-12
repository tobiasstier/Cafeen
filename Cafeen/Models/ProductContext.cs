namespace Cafeen.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ProductContext : DbContext
    {
        public ProductContext()
            : base("name=ProductContext1")
        {
        }

        public virtual DbSet<tblCategory> tblCategories { get; set; }
        public virtual DbSet<tblProduct> tblProducts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblCategory>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tblCategory>()
                .HasMany(e => e.tblProducts)
                .WithOptional(e => e.tblCategory)
                .HasForeignKey(e => e.CategoryId);
        }
    }
}
