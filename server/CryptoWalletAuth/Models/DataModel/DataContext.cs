using CryptoWalletAuth.Models.DataModel;
using Microsoft.EntityFrameworkCore;

namespace CryptoWalletAuth
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Nonce = 123456987654,
                    PublicAddress = "0x174A639d18a2EE6590ed1A201F8CCC76A52FFB13",
                    RefreshToken = "",
                    RefreshTokenExpiration = DateTime.Now
                }
            );
            modelBuilder.Entity<UserRole>().HasData(
               new UserRole
               {
                   Id = 1,
                   UserId = 1,
                   Name = "Administrator"
               },
               new UserRole
               {
                   Id = 2,
                   UserId = 1,
                   Name = "View"
               }
           );
        }
    }
}
