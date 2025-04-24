using Microsoft.EntityFrameworkCore;

using ASP.NET.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ASP.NET.Data;

public class ApplicationDbContext : DbContext
{
    
    private readonly IConfiguration configuration;

    public ApplicationDbContext(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
    }

    public DbSet<Joke> jokes { get; set; }
    
    
}