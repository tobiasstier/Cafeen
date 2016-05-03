namespace Cafeen.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ProductContext : DbContext
    {
        public ProductContext()
            : base("name=ProductContext")
        {
        }

        public virtual DbSet<tblCategory> tblCategories { get; set; }
        public virtual DbSet<tblInventory> tblInventories { get; set; }
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

            modelBuilder.Entity<tblInventory>()
                .HasMany(e => e.tblProducts)
                .WithOptional(e => e.tblInventory)
                .HasForeignKey(e => e.InventoryId);
        }
    }
}
