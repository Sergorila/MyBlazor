using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MyBlazor.DataAccess.Entities;

namespace MyBlazor.DataAccess.Context;

public class MyContext : DbContext
{
    private readonly string _connectionString;
    public MyContext(IOptions<Connection> connectionStringOptions)
    {
        _connectionString = connectionStringOptions.Value.MyDb;
        Database.EnsureCreated();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Order> Orders { get; set; }
}