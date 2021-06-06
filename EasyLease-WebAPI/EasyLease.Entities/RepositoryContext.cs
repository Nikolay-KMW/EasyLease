using EasyLease.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyLease.Entities {
    public class RepositoryContext : DbContext {
        public RepositoryContext(DbContextOptions options) : base(options) { }

        public DbSet<Advert> Adverts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
    }
}