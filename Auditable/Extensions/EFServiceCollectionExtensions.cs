using System.Text.Json;
using Auditable.Entity;
using Auditable.Entity.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Auditable.Extensions;

public static class EfServiceCollectionExtensions
{
    public static void AddAuditModel(this ModelBuilder modelBuilder)
    {
        if (modelBuilder == null)
        {
            throw new ArgumentNullException(nameof(modelBuilder));
        }

        modelBuilder.ApplyConfiguration(new AuditConfiguration());
    }
    public static async Task<int> SaveChangesWithAudiLogging(this DbContext context)
    {

        ChangeTrackerAudit(context);
        return await context.SaveChangesAsync();
    }
    
    private static void ChangeTrackerAudit(DbContext context)
    {
        context.ChangeTracker.Entries().Where(p=>p.State!=EntityState.Unchanged ).ToList().ForEach((entity) =>
        {
            Audit(entity,context);
        });
    }
    private static void Audit(EntityEntry entry,DbContext context)
    {
        if (!string.Equals(entry.Entity.GetType().Name, nameof(Audit), StringComparison.OrdinalIgnoreCase))
        {
            var auditEntry = new Audit
            {
                TableName = entry.Entity.GetType().Name,
                Date = DateTime.Now,
                Action = entry.State.ToString(),
                PrimaryKey = entry.Properties.SingleOrDefault(rec=>rec.Metadata.IsPrimaryKey())?.CurrentValue?.ToString(),
                //az koja por konim 
                UserId = String.Empty,
                ChangesFiled = JsonSerializer.Serialize(ChangesList(entry.Properties))
            };
            context.Add(auditEntry);
        }

    }

    private static IEnumerable<Changes> ChangesList(IEnumerable<PropertyEntry> propertyEntries)
    {
        return propertyEntries.Select(rec => new Changes
        {
            FiledName = rec.Metadata.Name,
            NewValue = rec.CurrentValue?.ToString(),
            OldValue = rec.OriginalValue?.ToString()
        });
    }
}