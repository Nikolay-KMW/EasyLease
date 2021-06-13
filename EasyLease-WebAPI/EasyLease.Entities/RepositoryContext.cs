using EasyLease.Entities.Configuration;
using EasyLease.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyLease.Entities {
    public class RepositoryContext : DbContext {
        public RepositoryContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {


            modelBuilder.Entity<AdvertTag>()
                .HasKey(t => new { t.AdvertId, t.TagId });

            modelBuilder.Entity<AdvertTag>()
                .HasOne(advertTag => advertTag.Advert)
                .WithMany(advert => advert.AdvertTags)
                .HasForeignKey(advertTag => advertTag.AdvertId);

            modelBuilder.Entity<AdvertTag>()
                .HasOne(advertTag => advertTag.Tag)
                .WithMany(tag => tag.AdvertTags)
                .HasForeignKey(advertTag => advertTag.TagId);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new AdvertConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new AdvertTagConfiguration());
            modelBuilder.ApplyConfiguration(new RegionConfiguration());
            modelBuilder.ApplyConfiguration(new DistrictConfiguration());
            modelBuilder.ApplyConfiguration(new SettlementTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StreetTypeConfiguration());
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Advert> Adverts { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}