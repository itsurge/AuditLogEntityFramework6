using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auditable.Entity.EntityTypeConfigurations;

internal class AuditConfiguration:IEntityTypeConfiguration<Audit>
{
    private const string TableName = "Audit";
    private const string Schema = "dbo";
    public void Configure(EntityTypeBuilder<Audit> builder)
    {
        builder.HasKey(x => x.AuditId);
        builder.ToTable(TableName, Schema);
    }
}