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
            var sku2 = new Sku { Id = 2, Name = "Выпечка" };
            var sku3 = new Sku { Id = 3, Name = "Молочные изделия" };
            var sku4 = new Sku { Id = 4, Name = "Фрукты" };
            var sku5 = new Sku { Id = 5, Name = "Овощи" };
            var sku6 = new Sku { Id = 6, Name = "Мясо" };
            var sku7 = new Sku { Id = 7, Name = "Рыба" };
            var sku8 = new Sku { Id = 8, Name = "Заморозка" };
            var sku9 = new Sku { Id = 9, Name = "Закуски" };
            var sku10 = new Sku { Id = 10, Name = "Конфетюр" };
            var sku11 = new Sku { Id = 11, Name = "Консервы" };
            var sku12 = new Sku { Id = 12, Name = "Соусы" };
            var sku13 = new Sku { Id = 13, Name = "Специи" };
            var sku14 = new Sku { Id = 14, Name = "Хлопья" };
            var sku15 = new Sku { Id = 15, Name = "Рис и макароны" };
            var sku16 = new Sku { Id = 16, Name = "Для завтрака" };
            var sku17 = new Sku { Id = 17, Name = "Гигиена" };
            var sku18 = new Sku { Id = 18, Name = "Домашние принадлежности" };
            var sku19 = new Sku { Id = 19, Name = "Для животных" };
            var sku20 = new Sku { Id = 20, Name = "Алкоголь" };
            
            var sub1 = new SubSku { Id = 1, Name = "Cola", SkuId = 1, Price = 1.5m, Ratio = 100 };
            var sub2 = new SubSku { Id = 2, Name = "Lemonade", SkuId = 1, Price = 1.2m, Ratio = 80 };

            // SKU2 (ids 3,4)
            var sub3 = new SubSku { Id = 3, Name = "Bread", SkuId = 2, Price = 0.9m, Ratio = 95 };
            var sub4 = new SubSku { Id = 4, Name = "Croissant", SkuId = 2, Price = 1.1m, Ratio = 85 };

            // SKU3 (ids 5,6)
            var sub5 = new SubSku { Id = 5, Name = "Milk", SkuId = 3, Price = 0.8m, Ratio = 100 };
            var sub6 = new SubSku { Id = 6, Name = "Cheese", SkuId = 3, Price = 2.5m, Ratio = 90 };

            // SKU4 (ids 7,8)
            var sub7 = new SubSku { Id = 7, Name = "Apple", SkuId = 4, Price = 0.3m, Ratio = 120 };
            var sub8 = new SubSku { Id = 8, Name = "Banana", SkuId = 4, Price = 0.25m, Ratio = 110 };

            // SKU5 (ids 9,10)
            var sub9 = new SubSku { Id = 9, Name = "Tomato", SkuId = 5, Price = 0.4m, Ratio = 105 };
            var sub10 = new SubSku { Id = 10, Name = "Cucumber", SkuId = 5, Price = 0.35m, Ratio = 100 };

            // SKU6 (ids 11,12)
            var sub11 = new SubSku { Id = 11, Name = "Beef", SkuId = 6, Price = 5.0m, Ratio = 70 };
            var sub12 = new SubSku { Id = 12, Name = "Pork", SkuId = 6, Price = 4.0m, Ratio = 75 };

            // SKU7 (ids 13,14)
            var sub13 = new SubSku { Id = 13, Name = "Salmon", SkuId = 7, Price = 8.0m, Ratio = 60 };
            var sub14 = new SubSku { Id = 14, Name = "Shrimp", SkuId = 7, Price = 6.5m, Ratio = 65 };

            // SKU8 (ids 15,16)
            var sub15 = new SubSku { Id = 15, Name = "Ice Cream", SkuId = 8, Price = 2.0m, Ratio = 90 };
            var sub16 = new SubSku { Id = 16, Name = "Frozen Pizza", SkuId = 8, Price = 3.5m, Ratio = 85 };

            // SKU9 (ids 17,18)
            var sub17 = new SubSku { Id = 17, Name = "Chips", SkuId = 9, Price = 1.3m, Ratio = 110 };
            var sub18 = new SubSku { Id = 18, Name = "Nuts", SkuId = 9, Price = 2.2m, Ratio = 95 };

            // SKU10 (ids 19,20)
            var sub19 = new SubSku { Id = 19, Name = "Chocolate Bar", SkuId = 10, Price = 1.0m, Ratio = 100 };
            var sub20 = new SubSku { Id = 20, Name = "Candy", SkuId = 10, Price = 0.6m, Ratio = 120 };

            // SKU11 (ids 21,22)
            var sub21 = new SubSku { Id = 21, Name = "Tuna Can", SkuId = 11, Price = 1.8m, Ratio = 80 };
            var sub22 = new SubSku { Id = 22, Name = "Bean Can", SkuId = 11, Price = 1.0m, Ratio = 95 };

            // SKU12 (ids 23,24)
            var sub23 = new SubSku { Id = 23, Name = "Ketchup", SkuId = 12, Price = 1.5m, Ratio = 90 };
            var sub24 = new SubSku { Id = 24, Name = "Mayonnaise", SkuId = 12, Price = 1.6m, Ratio = 85 };

            // SKU13 (ids 25,26)
            var sub25 = new SubSku { Id = 25, Name = "Black Pepper", SkuId = 13, Price = 0.7m, Ratio = 130 };
            var sub26 = new SubSku { Id = 26, Name = "Paprika", SkuId = 13, Price = 0.9m, Ratio = 120 };

            // SKU14 (ids 27,28)
            var sub27 = new SubSku { Id = 27, Name = "Oatmeal", SkuId = 14, Price = 1.2m, Ratio = 100 };
            var sub28 = new SubSku { Id = 28, Name = "Corn Flakes", SkuId = 14, Price = 1.4m, Ratio = 95 };

            // SKU15 (ids 29,30)
            var sub29 = new SubSku { Id = 29, Name = "Spaghetti", SkuId = 15, Price = 1.1m, Ratio = 110 };
            var sub30 = new SubSku { Id = 30, Name = "Rice", SkuId = 15, Price = 0.9m, Ratio = 115 };

            // SKU16 (ids 31,32)
            var sub31 = new SubSku { Id = 31, Name = "Cereal Bar", SkuId = 16, Price = 0.8m, Ratio = 125 };
            var sub32 = new SubSku { Id = 32, Name = "Pancake Mix", SkuId = 16, Price = 2.0m, Ratio = 90 };

            // SKU17 (ids 33,34)
            var sub33 = new SubSku { Id = 33, Name = "Shampoo", SkuId = 17, Price = 3.5m, Ratio = 70 };
            var sub34 = new SubSku { Id = 34, Name = "Soap", SkuId = 17, Price = 1.2m, Ratio = 95 };

            // SKU18 (ids 35,36)
            var sub35 = new SubSku { Id = 35, Name = "Detergent", SkuId = 18, Price = 4.0m, Ratio = 75 };
            var sub36 = new SubSku { Id = 36, Name = "Paper Towels", SkuId = 18, Price = 2.5m, Ratio = 85 };

            // SKU19 (ids 37,38)
            var sub37 = new SubSku { Id = 37, Name = "Dog Food", SkuId = 19, Price = 2.8m, Ratio = 80 };
            var sub38 = new SubSku { Id = 38, Name = "Cat Food", SkuId = 19, Price = 2.4m, Ratio = 85 };

            // SKU20 (ids 39,40)
            var sub39 = new SubSku { Id = 39, Name = "Beer", SkuId = 20, Price = 1.8m, Ratio = 60 };
            var sub40 = new SubSku { Id = 40, Name = "Wine", SkuId = 20, Price = 6.0m, Ratio = 50 };

            /// History (units and amount) and Planning (units and amount)
            // For brevity amounts equal units * price (rounded where necessary).
            var hist1 = new HistoryY0 { SubSkuId = 1, Units = 1000, Amount = 1500m };
            var hist2 = new HistoryY0 { SubSkuId = 2, Units = 800, Amount = 960m };

            var hist3 = new HistoryY0 { SubSkuId = 3, Units = 600, Amount = 540m };
            var hist4 = new HistoryY0 { SubSkuId = 4, Units = 350, Amount = 385m };

            var hist5 = new HistoryY0 { SubSkuId = 5, Units = 900, Amount = 720m };
            var hist6 = new HistoryY0 { SubSkuId = 6, Units = 200, Amount = 500m };

            var hist7 = new HistoryY0 { SubSkuId = 7, Units = 1500, Amount = 450m };
            var hist8 = new HistoryY0 { SubSkuId = 8, Units = 1300, Amount = 325m };

            var hist9 = new HistoryY0 { SubSkuId = 9, Units = 700, Amount = 280m };
            var hist10 = new HistoryY0 { SubSkuId = 10, Units = 500, Amount = 175m };

            var hist11 = new HistoryY0 { SubSkuId = 11, Units = 120, Amount = 600m };
            var hist12 = new HistoryY0 { SubSkuId = 12, Units = 140, Amount = 560m };

            var hist13 = new HistoryY0 { SubSkuId = 13, Units = 80, Amount = 640m };
            var hist14 = new HistoryY0 { SubSkuId = 14, Units = 100, Amount = 650m };

            var hist15 = new HistoryY0 { SubSkuId = 15, Units = 300, Amount = 600m };
            var hist16 = new HistoryY0 { SubSkuId = 16, Units = 220, Amount = 770m };

            var hist17 = new HistoryY0 { SubSkuId = 17, Units = 1100, Amount = 1430m };
            var hist18 = new HistoryY0 { SubSkuId = 18, Units = 400, Amount = 880m };

            var hist19 = new HistoryY0 { SubSkuId = 19, Units = 900, Amount = 900m };
            var hist20 = new HistoryY0 { SubSkuId = 20, Units = 2000, Amount = 1200m };

            var hist21 = new HistoryY0 { SubSkuId = 21, Units = 250, Amount = 450m };
            var hist22 = new HistoryY0 { SubSkuId = 22, Units = 300, Amount = 300m };

            var hist23 = new HistoryY0 { SubSkuId = 23, Units = 400, Amount = 600m };
            var hist24 = new HistoryY0 { SubSkuId = 24, Units = 350, Amount = 560m };

            var hist25 = new HistoryY0 { SubSkuId = 25, Units = 150, Amount = 105m };
            var hist26 = new HistoryY0 { SubSkuId = 26, Units = 120, Amount = 108m };

            var hist27 = new HistoryY0 { SubSkuId = 27, Units = 450, Amount = 540m };
            var hist28 = new HistoryY0 { SubSkuId = 28, Units = 380, Amount = 532m };

            var hist29 = new HistoryY0 { SubSkuId = 29, Units = 600, Amount = 660m };
            var hist30 = new HistoryY0 { SubSkuId = 30, Units = 800, Amount = 720m };

            var hist31 = new HistoryY0 { SubSkuId = 31, Units = 420, Amount = 336m };
            var hist32 = new HistoryY0 { SubSkuId = 32, Units = 160, Amount = 320m };

            var hist33 = new HistoryY0 { SubSkuId = 33, Units = 300, Amount = 1050m };
            var hist34 = new HistoryY0 { SubSkuId = 34, Units = 500, Amount = 600m };

            var hist35 = new HistoryY0 { SubSkuId = 35, Units = 200, Amount = 800m };
            var hist36 = new HistoryY0 { SubSkuId = 36, Units = 260, Amount = 650m };

            var hist37 = new HistoryY0 { SubSkuId = 37, Units = 220, Amount = 616m };
            var hist38 = new HistoryY0 { SubSkuId = 38, Units = 180, Amount = 432m };

            var hist39 = new HistoryY0 { SubSkuId = 39, Units = 1000, Amount = 1800m };
            var hist40 = new HistoryY0 { SubSkuId = 40, Units = 120, Amount = 720m };

            // Planning values (higher than history)
            var plan1 = new PlaningY1 { SubSkuId = 1, Units = 1200, Amount = 1800m };
            var plan2 = new PlaningY1 { SubSkuId = 2, Units = 900, Amount = 1080m };

            var plan3 = new PlaningY1 { SubSkuId = 3, Units = 720, Amount = 648m };
            var plan4 = new PlaningY1 { SubSkuId = 4, Units = 420, Amount = 462m };

            var plan5 = new PlaningY1 { SubSkuId = 5, Units = 1000, Amount = 800m };
            var plan6 = new PlaningY1 { SubSkuId = 6, Units = 240, Amount = 600m };

            var plan7 = new PlaningY1 { SubSkuId = 7, Units = 1650, Amount = 495m };
            var plan8 = new PlaningY1 { SubSkuId = 8, Units = 1500, Amount = 375m };

            var plan9 = new PlaningY1 { SubSkuId = 9, Units = 840, Amount = 336m };
            var plan10 = new PlaningY1 { SubSkuId = 10, Units = 600, Amount = 210m };

            var plan11 = new PlaningY1 { SubSkuId = 11, Units = 140, Amount = 700m };
            var plan12 = new PlaningY1 { SubSkuId = 12, Units = 160, Amount = 640m };

            var plan13 = new PlaningY1 { SubSkuId = 13, Units = 90, Amount = 720m };
            var plan14 = new PlaningY1 { SubSkuId = 14, Units = 120, Amount = 780m };

            var plan15 = new PlaningY1 { SubSkuId = 15, Units = 360, Amount = 720m };
            var plan16 = new PlaningY1 { SubSkuId = 16, Units = 260, Amount = 910m };

            var plan17 = new PlaningY1 { SubSkuId = 17, Units = 1250, Amount = 1625m };
            var plan18 = new PlaningY1 { SubSkuId = 18, Units = 480, Amount = 1056m };

            var plan19 = new PlaningY1 { SubSkuId = 19, Units = 1000, Amount = 1000m };
            var plan20 = new PlaningY1 { SubSkuId = 20, Units = 2400, Amount = 1440m };

            var plan21 = new PlaningY1 { SubSkuId = 21, Units = 300, Amount = 540m };
            var plan22 = new PlaningY1 { SubSkuId = 22, Units = 360, Amount = 360m };

            var plan23 = new PlaningY1 { SubSkuId = 23, Units = 460, Amount = 690m };
            var plan24 = new PlaningY1 { SubSkuId = 24, Units = 420, Amount = 672m };

            var plan25 = new PlaningY1 { SubSkuId = 25, Units = 170, Amount = 119m };
            var plan26 = new PlaningY1 { SubSkuId = 26, Units = 140, Amount = 126m };

            var plan27 = new PlaningY1 { SubSkuId = 27, Units = 500, Amount = 600m };
            var plan28 = new PlaningY1 { SubSkuId = 28, Units = 420, Amount = 588m };

            var plan29 = new PlaningY1 { SubSkuId = 29, Units = 660, Amount = 726m };
            var plan30 = new PlaningY1 { SubSkuId = 30, Units = 880, Amount = 792m };

            var plan31 = new PlaningY1 { SubSkuId = 31, Units = 480, Amount = 384m };
            var plan32 = new PlaningY1 { SubSkuId = 32, Units = 200, Amount = 400m };

            var plan33 = new PlaningY1 { SubSkuId = 33, Units = 360, Amount = 1260m };
            var plan34 = new PlaningY1 { SubSkuId = 34, Units = 600, Amount = 720m };

            var plan35 = new PlaningY1 { SubSkuId = 35, Units = 240, Amount = 960m };
            var plan36 = new PlaningY1 { SubSkuId = 36, Units = 310, Amount = 775m };

            var plan37 = new PlaningY1 { SubSkuId = 37, Units = 260, Amount = 728m };
            var plan38 = new PlaningY1 { SubSkuId = 38, Units = 210, Amount = 504m };

            var plan39 = new PlaningY1 { SubSkuId = 39, Units = 1200, Amount = 2160m };
            var plan40 = new PlaningY1 { SubSkuId = 40, Units = 150, Amount = 900m };

            modelBuilder.Entity<Sku>().HasData(
                sku1, sku2, sku3, sku4, sku5, sku6, sku7, sku8, sku9, sku10,
                sku11, sku12, sku13, sku14, sku15, sku16, sku17, sku18, sku19, sku20
            );

            modelBuilder.Entity<SubSku>().HasData(
                // SKU1
                new { sub1.Id, sub1.Name, sub1.SkuId, sub1.Price, sub1.Ratio },
                new { sub2.Id, sub2.Name, sub2.SkuId, sub2.Price, sub2.Ratio },

                // SKU2
                new { sub3.Id, sub3.Name, sub3.SkuId, sub3.Price, sub3.Ratio },
                new { sub4.Id, sub4.Name, sub4.SkuId, sub4.Price, sub4.Ratio },

                // SKU3
                new { sub5.Id, sub5.Name, sub5.SkuId, sub5.Price, sub5.Ratio },
                new { sub6.Id, sub6.Name, sub6.SkuId, sub6.Price, sub6.Ratio },

                // SKU4
                new { sub7.Id, sub7.Name, sub7.SkuId, sub7.Price, sub7.Ratio },
                new { sub8.Id, sub8.Name, sub8.SkuId, sub8.Price, sub8.Ratio },

                // SKU5
                new { sub9.Id, sub9.Name, sub9.SkuId, sub9.Price, sub9.Ratio },
                new { sub10.Id, sub10.Name, sub10.SkuId, sub10.Price, sub10.Ratio },

                // SKU6
                new { sub11.Id, sub11.Name, sub11.SkuId, sub11.Price, sub11.Ratio },
                new { sub12.Id, sub12.Name, sub12.SkuId, sub12.Price, sub12.Ratio },

                // SKU7
                new { sub13.Id, sub13.Name, sub13.SkuId, sub13.Price, sub13.Ratio },
                new { sub14.Id, sub14.Name, sub14.SkuId, sub14.Price, sub14.Ratio },

                // SKU8
                new { sub15.Id, sub15.Name, sub15.SkuId, sub15.Price, sub15.Ratio },
                new { sub16.Id, sub16.Name, sub16.SkuId, sub16.Price, sub16.Ratio },

                // SKU9
                new { sub17.Id, sub17.Name, sub17.SkuId, sub17.Price, sub17.Ratio },
                new { sub18.Id, sub18.Name, sub18.SkuId, sub18.Price, sub18.Ratio },

                // SKU10
                new { sub19.Id, sub19.Name, sub19.SkuId, sub19.Price, sub19.Ratio },
                new { sub20.Id, sub20.Name, sub20.SkuId, sub20.Price, sub20.Ratio },

                // SKU11
                new { sub21.Id, sub21.Name, sub21.SkuId, sub21.Price, sub21.Ratio },
                new { sub22.Id, sub22.Name, sub22.SkuId, sub22.Price, sub22.Ratio },

                // SKU12
                new { sub23.Id, sub23.Name, sub23.SkuId, sub23.Price, sub23.Ratio },
                new { sub24.Id, sub24.Name, sub24.SkuId, sub24.Price, sub24.Ratio },

                // SKU13
                new { sub25.Id, sub25.Name, sub25.SkuId, sub25.Price, sub25.Ratio },
                new { sub26.Id, sub26.Name, sub26.SkuId, sub26.Price, sub26.Ratio },

                // SKU14
                new { sub27.Id, sub27.Name, sub27.SkuId, sub27.Price, sub27.Ratio },
                new { sub28.Id, sub28.Name, sub28.SkuId, sub28.Price, sub28.Ratio },

                // SKU15
                new { sub29.Id, sub29.Name, sub29.SkuId, sub29.Price, sub29.Ratio },
                new { sub30.Id, sub30.Name, sub30.SkuId, sub30.Price, sub30.Ratio },

                // SKU16
                new { sub31.Id, sub31.Name, sub31.SkuId, sub31.Price, sub31.Ratio },
                new { sub32.Id, sub32.Name, sub32.SkuId, sub32.Price, sub32.Ratio },

                // SKU17
                new { sub33.Id, sub33.Name, sub33.SkuId, sub33.Price, sub33.Ratio },
                new { sub34.Id, sub34.Name, sub34.SkuId, sub34.Price, sub34.Ratio },

                // SKU18
                new { sub35.Id, sub35.Name, sub35.SkuId, sub35.Price, sub35.Ratio },
                new { sub36.Id, sub36.Name, sub36.SkuId, sub36.Price, sub36.Ratio },

                // SKU19
                new { sub37.Id, sub37.Name, sub37.SkuId, sub37.Price, sub37.Ratio },
                new { sub38.Id, sub38.Name, sub38.SkuId, sub38.Price, sub38.Ratio },

                // SKU20
                new { sub39.Id, sub39.Name, sub39.SkuId, sub39.Price, sub39.Ratio },
                new { sub40.Id, sub40.Name, sub40.SkuId, sub40.Price, sub40.Ratio }
            );

            modelBuilder.Entity<HistoryY0>().HasData(
                new { hist1.SubSkuId, hist1.Units, hist1.Amount },
                new { hist2.SubSkuId, hist2.Units, hist2.Amount },

                new { hist3.SubSkuId, hist3.Units, hist3.Amount },
                new { hist4.SubSkuId, hist4.Units, hist4.Amount },

                new { hist5.SubSkuId, hist5.Units, hist5.Amount },
                new { hist6.SubSkuId, hist6.Units, hist6.Amount },

                new { hist7.SubSkuId, hist7.Units, hist7.Amount },
                new { hist8.SubSkuId, hist8.Units, hist8.Amount },

                new { hist9.SubSkuId, hist9.Units, hist9.Amount },
                new { hist10.SubSkuId, hist10.Units, hist10.Amount },

                new { hist11.SubSkuId, hist11.Units, hist11.Amount },
                new { hist12.SubSkuId, hist12.Units, hist12.Amount },

                new { hist13.SubSkuId, hist13.Units, hist13.Amount },
                new { hist14.SubSkuId, hist14.Units, hist14.Amount },

                new { hist15.SubSkuId, hist15.Units, hist15.Amount },
                new { hist16.SubSkuId, hist16.Units, hist16.Amount },

                new { hist17.SubSkuId, hist17.Units, hist17.Amount },
                new { hist18.SubSkuId, hist18.Units, hist18.Amount },

                new { hist19.SubSkuId, hist19.Units, hist19.Amount },
                new { hist20.SubSkuId, hist20.Units, hist20.Amount },

                new { hist21.SubSkuId, hist21.Units, hist21.Amount },
                new { hist22.SubSkuId, hist22.Units, hist22.Amount },

                new { hist23.SubSkuId, hist23.Units, hist23.Amount },
                new { hist24.SubSkuId, hist24.Units, hist24.Amount },

                new { hist25.SubSkuId, hist25.Units, hist25.Amount },
                new { hist26.SubSkuId, hist26.Units, hist26.Amount },

                new { hist27.SubSkuId, hist27.Units, hist27.Amount },
                new { hist28.SubSkuId, hist28.Units, hist28.Amount },

                new { hist29.SubSkuId, hist29.Units, hist29.Amount },
                new { hist30.SubSkuId, hist30.Units, hist30.Amount },

                new { hist31.SubSkuId, hist31.Units, hist31.Amount },
                new { hist32.SubSkuId, hist32.Units, hist32.Amount },

                new { hist33.SubSkuId, hist33.Units, hist33.Amount },
                new { hist34.SubSkuId, hist34.Units, hist34.Amount },

                new { hist35.SubSkuId, hist35.Units, hist35.Amount },
                new { hist36.SubSkuId, hist36.Units, hist36.Amount },

                new { hist37.SubSkuId, hist37.Units, hist37.Amount },
                new { hist38.SubSkuId, hist38.Units, hist38.Amount },

                new { hist39.SubSkuId, hist39.Units, hist39.Amount },
                new { hist40.SubSkuId, hist40.Units, hist40.Amount }
            );

            modelBuilder.Entity<PlaningY1>().HasData(
                new { plan1.SubSkuId, plan1.Units, plan1.Amount },
                new { plan2.SubSkuId, plan2.Units, plan2.Amount },

                new { plan3.SubSkuId, plan3.Units, plan3.Amount },
                new { plan4.SubSkuId, plan4.Units, plan4.Amount },

                new { plan5.SubSkuId, plan5.Units, plan5.Amount },
                new { plan6.SubSkuId, plan6.Units, plan6.Amount },

                new { plan7.SubSkuId, plan7.Units, plan7.Amount },
                new { plan8.SubSkuId, plan8.Units, plan8.Amount },

                new { plan9.SubSkuId, plan9.Units, plan9.Amount },
                new { plan10.SubSkuId, plan10.Units, plan10.Amount },

                new { plan11.SubSkuId, plan11.Units, plan11.Amount },
                new { plan12.SubSkuId, plan12.Units, plan12.Amount },

                new { plan13.SubSkuId, plan13.Units, plan13.Amount },
                new { plan14.SubSkuId, plan14.Units, plan14.Amount },

                new { plan15.SubSkuId, plan15.Units, plan15.Amount },
                new { plan16.SubSkuId, plan16.Units, plan16.Amount },

                new { plan17.SubSkuId, plan17.Units, plan17.Amount },
                new { plan18.SubSkuId, plan18.Units, plan18.Amount },

                new { plan19.SubSkuId, plan19.Units, plan19.Amount },
                new { plan20.SubSkuId, plan20.Units, plan20.Amount },

                new { plan21.SubSkuId, plan21.Units, plan21.Amount },
                new { plan22.SubSkuId, plan22.Units, plan22.Amount },

                new { plan23.SubSkuId, plan23.Units, plan23.Amount },
                new { plan24.SubSkuId, plan24.Units, plan24.Amount },

                new { plan25.SubSkuId, plan25.Units, plan25.Amount },
                new { plan26.SubSkuId, plan26.Units, plan26.Amount },

                new { plan27.SubSkuId, plan27.Units, plan27.Amount },
                new { plan28.SubSkuId, plan28.Units, plan28.Amount },

                new { plan29.SubSkuId, plan29.Units, plan29.Amount },
                new { plan30.SubSkuId, plan30.Units, plan30.Amount },

                new { plan31.SubSkuId, plan31.Units, plan31.Amount },
                new { plan32.SubSkuId, plan32.Units, plan32.Amount },

                new { plan33.SubSkuId, plan33.Units, plan33.Amount },
                new { plan34.SubSkuId, plan34.Units, plan34.Amount },

                new { plan35.SubSkuId, plan35.Units, plan35.Amount },
                new { plan36.SubSkuId, plan36.Units, plan36.Amount },

                new { plan37.SubSkuId, plan37.Units, plan37.Amount },
                new { plan38.SubSkuId, plan38.Units, plan38.Amount },

                new { plan39.SubSkuId, plan39.Units, plan39.Amount },
                new { plan40.SubSkuId, plan40.Units, plan40.Amount }
            );
        }
    }
}
