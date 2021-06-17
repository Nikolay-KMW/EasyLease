using System.Linq;
using EasyLease.Entities.Configuration;
using EasyLease.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyLease.Entities {
    public class RepositoryContext : DbContext {
        public RepositoryContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            //  This sets up the composite foreign key for the Region and District properties in the Location table
            modelBuilder.Entity<Location>()
                .HasKey(location => new { location.Region, location.District });

            modelBuilder.Entity<Advert>()
                .HasOne(advert => advert.Location)
                .WithMany(location => location.Adverts)
                .HasForeignKey(advert => new { advert.Region, advert.District })
                .HasPrincipalKey(location => new { location.Region, location.District });

            //  This set up many-to-many relationship for the Advert and Tag tables
            modelBuilder.Entity<AdvertTag>()
                .HasKey(advertTag => new { advertTag.AdvertId, advertTag.TagId });

            modelBuilder.Entity<AdvertTag>()
                .HasOne(advertTag => advertTag.Advert)
                .WithMany(advert => advert.AdvertTags)
                .HasForeignKey(advertTag => advertTag.AdvertId);

            modelBuilder.Entity<AdvertTag>()
                .HasOne(advertTag => advertTag.Tag)
                .WithMany(tag => tag.AdvertTags)
                .HasForeignKey(advertTag => advertTag.TagId);

            //  This set up many-to-many relationship for the Advert and Compfort tables
            modelBuilder.Entity<AdvertComfort>()
                .HasKey(advertComfort => new { advertComfort.AdvertId, advertComfort.ComfortId });

            modelBuilder.Entity<AdvertComfort>()
                .HasOne(advertComfort => advertComfort.Advert)
                .WithMany(advert => advert.AdvertComforts)
                .HasForeignKey(advertComfort => advertComfort.AdvertId);

            modelBuilder.Entity<AdvertComfort>()
                .HasOne(advertComfort => advertComfort.Comfort)
                .WithMany(comfort => comfort.AdvertComforts)
                .HasForeignKey(advertComfort => advertComfort.ComfortId);

            //  This set up many-to-many relationship for the Advert (favorites) and User tables
            modelBuilder.Entity<AdvertFavorite>()
                .HasKey(advertFavorite => new { advertFavorite.AdvertId, advertFavorite.UserId });

            modelBuilder.Entity<AdvertFavorite>()
                .HasOne(advertFavorite => advertFavorite.Advert)
                .WithMany(advert => advert.AdvertFavorites)
                .HasForeignKey(advertFavorite => advertFavorite.AdvertId);

            modelBuilder.Entity<AdvertFavorite>()
                .HasOne(advertFavorite => advertFavorite.User)
                .WithMany(user => user.AdvertFavorites)
                .HasForeignKey(advertFavorite => advertFavorite.UserId);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new AdvertConfiguration());
            modelBuilder.ApplyConfiguration(new AdvertTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new AdvertTagConfiguration());
            modelBuilder.ApplyConfiguration(new LocationConfiguration());
            modelBuilder.ApplyConfiguration(new ComfortConfiguration());
            modelBuilder.ApplyConfiguration(new AdvertComfortConfiguration());
            modelBuilder.ApplyConfiguration(new SettlementTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StreetTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AdvertFavoriteConfiguration());
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Advert> Adverts { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Comfort> Comforts { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}