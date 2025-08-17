using Microsoft.EntityFrameworkCore;
using RandevuBackend.Models;

namespace RandevuBackend.Data;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Employee> Employee { get; set; }
}