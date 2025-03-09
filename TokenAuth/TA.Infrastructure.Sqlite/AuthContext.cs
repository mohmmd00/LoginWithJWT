using Microsoft.EntityFrameworkCore;
using TA.Domain.Entities;

namespace TA.Infrastructure.Sqlite
{
    public class AuthContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public AuthContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(key => key.PrimaryId);
            modelBuilder.Entity<Session>().HasKey(key => key.UserId);


            modelBuilder.Entity<Session>()
                .HasOne(s => s.User)
                .WithOne(u => u.Session)  
                .HasForeignKey<Session>(s => s.UserId); 



            base.OnModelCreating(modelBuilder);
        }
    }
}
