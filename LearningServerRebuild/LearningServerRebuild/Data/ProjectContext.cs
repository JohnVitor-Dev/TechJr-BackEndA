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
            .AddJsonFile("appsettings.json", false, true)
            .Build();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("ProjectContext"));
        }

        public DbSet<Users> User { get; set; }
        public DbSet<Products> Product { get; set; }

    }
}
