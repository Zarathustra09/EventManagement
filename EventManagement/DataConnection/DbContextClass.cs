using Microsoft.EntityFrameworkCore;
using EventManagement.Model;


namespace EventManagement.DataConnection
{
    public class DbContextClass : DbContext
    {
        protected readonly IConfiguration Configuration;
        public DbContextClass(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = Configuration.GetConnectionString("WebApiDatabase");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public DbSet<Login> Logins { get; set; }

    }
}