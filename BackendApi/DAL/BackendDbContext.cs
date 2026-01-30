using Microsoft.EntityFrameworkCore;
using Models;

namespace Intfastructure
{
    public class BackendDbContext(DbContextOptions<BackendDbContext> options) : DbContext(options)
    {
        public DbSet<Sku> Skus { get; set; } = null!;
        public DbSet<SubSku> SubSkus { get; set; } = null!;
        public DbSet<HistoryY0> HistoryY0s { get; set; } = null!;
        public DbSet<PlaningY1> PlaningY1s { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sku>(b =>
            {
                b.HasKey(s => s.Id);
                b.HasMany<SubSku>().WithOne().HasForeignKey(ss => ss.SkuId);
            });

            modelBuilder.Entity<SubSku>(b =>
            {
                b.HasKey(ss => ss.Id);

                // Explicitly map navigation properties on SubSku so EF Core knows
                // which property to populate when including related entities.
                b.HasOne(s => s.HistoryY0s)
                 .WithOne()
                 .HasForeignKey<HistoryY0>(h => h.SubSkuId)
                 .IsRequired(false);

                b.HasOne(s => s.PlaningY1s)
                 .WithOne()
                 .HasForeignKey<PlaningY1>(p => p.SubSkuId)
                 .IsRequired(false);
            });

            modelBuilder.Entity<HistoryY0>(b =>
            {
                b.HasKey(h => h.SubSkuId);
            });

            modelBuilder.Entity<PlaningY1>(b =>
            {
                b.HasKey(p => p.SubSkuId);
            });

            // Seed data
            var sku1 = new Sku { Id = 1, Name = "Напитки" };

            var sub1 = new SubSku { Id = 1, Name = "Cola", SkuId = 1, Price = 1.5m, Ratio = 100 };
            var sub2 = new SubSku { Id = 2, Name = "Lemonade", SkuId = 1, Price = 1.2m, Ratio = 80 };

            var hist1 = new HistoryY0 { SubSkuId = 1, Units = 1000, Amount = 1500m };
            var hist2 = new HistoryY0 { SubSkuId = 2, Units = 800, Amount = 960m };

            var plan1 = new PlaningY1 { SubSkuId = 1, Units = 1200, Amount = 1800m };
            var plan2 = new PlaningY1 { SubSkuId = 2, Units = 900, Amount = 1080m };

            modelBuilder.Entity<Sku>().HasData(sku1);

            modelBuilder.Entity<SubSku>().HasData(
                new { sub1.Id, sub1.Name, sub1.SkuId, sub1.Price, sub1.Ratio },
                new { sub2.Id, sub2.Name, sub2.SkuId, sub2.Price, sub2.Ratio }
            );

            modelBuilder.Entity<HistoryY0>().HasData(
                new { hist1.SubSkuId, hist1.Units, hist1.Amount },
                new { hist2.SubSkuId, hist2.Units, hist2.Amount }
            );

            modelBuilder.Entity<PlaningY1>().HasData(
                new { plan1.SubSkuId, plan1.Units, plan1.Amount },
                new { plan2.SubSkuId, plan2.Units, plan2.Amount }
            );
        }
    }
}
