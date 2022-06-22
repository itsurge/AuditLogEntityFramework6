using Audit.EntityFramework;
using AuditLog.WebApi.DbContext.Models;
using Microsoft.EntityFrameworkCore;

namespace AuditLog.WebApi.DbContext;

public class ProductDbContext:Microsoft.EntityFrameworkCore.DbContext
{
   // public ProductDbContext(DbContextOptions<ProductDbContext> options):base(options)
   // {
   //    
   // }

   public ProductDbContext()
   {
   }
  

   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   {
      optionsBuilder.UseSqlServer("Server=.;Database=product;User Id=sa;Password=12345678");
      optionsBuilder.AddInterceptors(new AuditSaveChangesInterceptor());
   }
   public virtual DbSet<Product> Products { get; set; }
   public virtual DbSet<ProductAudit> ProductAudits { get; set; }
}