using LearningServerRebuild.Models;
using Microsoft.EntityFrameworkCore;

namespace Project.Data
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddEnvironmentVariables()
            .AddJsonFile("appsettings.json", false, true)
            .Build();

            string connectionString = Environment.GetEnvironmentVariable("CONNECTIONSTRINGS_PROJECTCONTEXT");
            optionsBuilder.UseNpgsql(connectionString);
        }

        public DbSet<Users> User { get; set; }
        public DbSet<Products> Product { get; set; }

    }
}
