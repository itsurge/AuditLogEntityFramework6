namespace Auditable.Entity;

public class Audit
{
    
    public Guid AuditId { get; set; }
    public string? Action { get; set; }
    public string? TableName { get; set; }
    public string? ChangesFiled { get; set; }
    public string? PrimaryKey { get; set; }
    public DateTime Date { get; set; }
    public string? UserId { get; set; }
}
public  class Changes
{
    public string? FiledName { get; set; }
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
}