using Microsoft.EntityFrameworkCore;
using RJA3.Modules.Items.FoundItems.Domain;

namespace RJA3.Modules.Items.FoundItems.Persistence
{
    public class FoundItemDbContext : DbContext
    {
        public FoundItemDbContext(DbContextOptions<FoundItemDbContext> options) : base(options)
        {
            
        }

        public FoundItemDbContext() : base(GetOptions())
        {
        }

        private static DbContextOptions<FoundItemDbContext> GetOptions()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<FoundItemDbContext>();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("PostgreSQL"));

            return optionsBuilder.Options;
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<FoundItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FinderId).IsRequired();
                entity.Property(e => e.FoundAt).IsRequired();
                entity.Property(e => e.Latitude).IsRequired();
                entity.Property(e => e.Longitude).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.ItemType).IsRequired();
                entity.OwnsMany(e => e.SecurityQuestions, sq =>
                {
                    sq.Property(s => s.Question).IsRequired().HasMaxLength(500);
                    sq.Property(s => s.ExpectedAnswer).IsRequired().HasMaxLength(500);
                });
            });


            modelBuilder.Entity<FoundItem>()
                .HasDiscriminator<FoundItemType>("ItemType")
                .HasValue<PhoneFoundItem>(FoundItemType.Phone);
        }


        public DbSet<FoundItem> FoundItems => Set<FoundItem>();
    }
}