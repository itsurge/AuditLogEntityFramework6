using AuditLog.WebApi.DbContext.Models;
using Microsoft.EntityFrameworkCore;

namespace AuditLog.WebApi.DbContext;

public class ProductEf6TemporalDbContext:Microsoft.EntityFrameworkCore.DbContext
{

    public virtual DbSet<Product> Products => Set<Product>();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.;Database=ProductEf6Temporal;User Id=sa;Password=12345678");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().ToTable(builder => builder.IsTemporal());
    }
}