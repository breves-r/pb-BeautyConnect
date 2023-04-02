using Microsoft.EntityFrameworkCore;
using RedeSocial.Domain.Entities;

namespace RedeSocial.Infra.Context
{
    public class RedeSocialDbContext : DbContext
    {
        private const string connectionStrion = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RedeSocial-Db;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Friendship> Friendships { get; set; }

        public DbSet<ProfileDetails> ProfileDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionStrion);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RedeSocialDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
