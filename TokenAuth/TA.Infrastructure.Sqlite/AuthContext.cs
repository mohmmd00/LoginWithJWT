using Microsoft.EntityFrameworkCore;
using TA.Domain.Entities;

namespace TA.Infrastructure.Sqlite
{
    public class AuthContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<SecretMessage> SecretMessages { get; set; }
        public AuthContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(key => key.PrimaryId);
            modelBuilder.Entity<Session>().HasKey(key => key.UserId);
            modelBuilder.Entity<SecretMessage>().HasKey(x => x.PrimaryId);


            modelBuilder.Entity<Session>().HasOne<User>().WithOne().HasForeignKey<Session>(x => x.UserId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
