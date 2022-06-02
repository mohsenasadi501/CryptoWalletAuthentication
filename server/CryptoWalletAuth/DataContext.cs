using Microsoft.EntityFrameworkCore;

namespace CryptoWalletAuth
{
    public class DataContext : DbContext
    {
        //Constructor with DbContextOptions and the context class itself
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
