using System;
using System.Linq;
using EasyLease.Entities.Configuration;
using EasyLease.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EasyLease.Entities {
    public class RepositoryContext : IdentityDbContext<User, IdentityRole<Guid>, Guid> {
        public RepositoryContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

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
            modelBuilder.Entity<FavoriteAdvert>()
                .HasKey(favoriteAdvert => new { favoriteAdvert.AdvertId, favoriteAdvert.UserId });

            modelBuilder.Entity<FavoriteAdvert>()
                .HasOne(favoriteAdvert => favoriteAdvert.Advert)
                .WithMany(advert => advert.Subscribers)
                .HasForeignKey(favoriteAdvert => favoriteAdvert.AdvertId);

            modelBuilder.Entity<FavoriteAdvert>()
                .HasOne(favoriteAdvert => favoriteAdvert.User)
                .WithMany(user => user.FavoriteAdverts)
                .HasForeignKey(favoriteAdvert => favoriteAdvert.UserId);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new AdvertConfiguration());
            modelBuilder.ApplyConfiguration(new RealtyTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new AdvertTagConfiguration());
            modelBuilder.ApplyConfiguration(new LocationConfiguration());
            modelBuilder.ApplyConfiguration(new ComfortConfiguration());
            modelBuilder.ApplyConfiguration(new AdvertComfortConfiguration());
            modelBuilder.ApplyConfiguration(new SettlementTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StreetTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FavoriteAdvertConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        }
        // public DbSet<User> Users { get; set; }
        public DbSet<Advert> Adverts { get; set; }
        public DbSet<Comfort> Comforts { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}