using BackendPractice.Beginner.TodoListAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendPractice.Beginner.TodoListAPI.Data;

public class AppDbContext : DbContext
{
    private readonly IConfiguration? _config;

    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration config)
        : base(options)
    {
        _config = config;
    }

    public AppDbContext()
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<ToDoItem> ToDoItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=ToDoList;User Id=sa;Password=MoamenSQL@123;TrustServerCertificate=True;");
        }
        else if (_config is not null)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}