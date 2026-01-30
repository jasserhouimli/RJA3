using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RJA3.Modules.LostItems.Domain;
using RJA3.Modules.LostItems.Domain;

namespace RJA3.Modules.LostItems.Persistence
{
    public class LostItemDbContext : DbContext
    {
        public LostItemDbContext(DbContextOptions<LostItemDbContext> options) : base(options)
        {
            
        }

        public LostItemDbContext() : base(GetOptions())
        {
        }

        private static DbContextOptions<LostItemDbContext> GetOptions()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<LostItemDbContext>();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("PostgreSQL"));

            return optionsBuilder.Options;
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<LostItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.OwnerId).IsRequired();
                entity.Property(e => e.LostAt).IsRequired();
                entity.Property(e => e.Latitude).IsRequired();
                entity.Property(e => e.Longitude).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.ItemType).IsRequired();
            });


            modelBuilder.Entity<LostItem>()
                .HasDiscriminator<LostItemType>("ItemType")
                .HasValue<PhoneLostItem>(LostItemType.Phone);
        }


        public DbSet<LostItem> LostItems => Set<LostItem>();
    }
}
