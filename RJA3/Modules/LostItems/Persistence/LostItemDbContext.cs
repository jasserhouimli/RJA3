using Microsoft.EntityFrameworkCore;
using RJA3.Modules.LostAndFound.Domain;
using RJA3.Modules.LostItems.Domain;

namespace RJA3.Modules.LostItems.Persistence
{
    public class LostItemDbContext : DbContext
    {
        public LostItemDbContext(DbContextOptions<LostItemDbContext> options) : base(options)
        {
            
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<LostItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.OwnerId).IsRequired();
                entity.Property(e => e.LostAt).IsRequired();
                entity.Property(e => e.Location).IsRequired().HasMaxLength(500);
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
