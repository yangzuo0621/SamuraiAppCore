using Microsoft.EntityFrameworkCore;
using SamuraiAppCore.Domain;
using System;
using System.Linq;

namespace SamuraiAppCore.Data
{
    public class SamuraiContext : DbContext
    {
        public SamuraiContext(DbContextOptions<SamuraiContext> options) : base(options)
        {
        }

        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Battle> Battles { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<SamuraiBattle> SamuraiBattles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SamuraiBattle>()
                .HasKey(s => new { s.BattleId, s.SamuraiId });

            //modelBuilder.Entity<Samurai>().Property(s => s.SecretIdentity).IsRequired();
            //modelBuilder.Entity<Samurai>().Property<DateTime>("LastModified");

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entityType.Name).Property<DateTime>("LastModified");
                modelBuilder.Entity(entityType.Name).Ignore("IsDirty");
            }

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder
            //    .UseLoggerFactory(MyLoggerFactory)
            //    .UseSqlServer(
            //    "Server = (localdb)\\mssqllocaldb; Database = SamuraiWpfData; Trusted_Connection = True; "
            //);
            //optionsBuilder.EnableSensitiveDataLogging();
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                entry.Property("LastModified").CurrentValue = DateTime.Now;
            }

            return base.SaveChanges();
        }

        //public static readonly LoggerFactory MyLoggerFactory = new LoggerFactory(new[] { new ConsoleLoggerProvider((_, __) => true, true) });
        //public static readonly LoggerFactory MyLoggerFactory = new LoggerFactory(new[]
        //{
        //    new ConsoleLoggerProvider((category, level) =>
        //        level == LogLevel.Information, true)
        //});
    }
}