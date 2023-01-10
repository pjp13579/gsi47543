using Microsoft.EntityFrameworkCore;

namespace GSI47543.DB
{
    public class DataContext : DbContext
    {
        public DataContext() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=192.168.50.240;Database=users;User Id=yo;Password=1234;TrustServerCertificate=True");
        }

        public DbSet<DB.User> users { get; set; }
    }
}
