using Microsoft.EntityFrameworkCore;

class TaskDb : DbContext
{
    public TaskDb(DbContextOptions<TaskDb> options)
        : base(options) { }

    public DbSet<Task> Tasks => Set<Task>();
}