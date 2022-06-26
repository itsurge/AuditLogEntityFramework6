using System.Text.Json;
using System.Text.Json.Serialization;
using Auditable.Extensions;
using AuditLog.WebApi.DbContext.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AuditLog.WebApi.DbContext;

public class ProductDbContext:Microsoft.EntityFrameworkCore.DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.;Database=Product;User Id=sa;Password=12345678");
    }
    public virtual DbSet<Product> Products => Set<Product>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AddAuditModel();
        base.OnModelCreating(modelBuilder);
    }
}