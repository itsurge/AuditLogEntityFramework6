using Audit.EntityFramework;
using AuditLog.WebApi.DbContext.Models;
using Microsoft.EntityFrameworkCore;

namespace AuditLog.WebApi.DbContext;

public class ProductAuditNetDbContext:Microsoft.EntityFrameworkCore.DbContext
{
  

   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   {
      optionsBuilder.UseSqlServer("Server=.;Database=ProductAuditLog;User Id=sa;Password=12345678");
      optionsBuilder.AddInterceptors(new AuditSaveChangesInterceptor());
   }

 

   public virtual DbSet<Product> Products => Set<Product>();
   public virtual DbSet<ProductAudit> ProductAudits => Set<ProductAudit>();
}