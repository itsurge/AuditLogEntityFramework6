using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuditLog.WebApi.DbContext.Models;

[Table("Audit_Product")]
public class ProductAudit
{
    [Key]
    public Guid BaseId { get; set; }
    public Guid Id { get; set; }
    public string? Name { get;  set; }
    public decimal Price { get;  set; }
    public string? Category { get;  set; }
    public DateTime AuditDate  { get; set; }
    public string? UserName { get; set; }
    public string? AuditAction { get; set; }
   
    
}