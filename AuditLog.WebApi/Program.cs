using Audit.EntityFramework.Providers;
using AuditLog.WebApi.DbContext;
using AuditLog.WebApi.DbContext.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console());

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ProductEf6TemporalDbContext>();
builder.Services.AddDbContext<ProductAuditNetDbContext>();
builder.Services.AddDbContext<ProductDbContext>();

Audit.Core.Configuration.DataProvider = new EntityFrameworkDataProvider()
{
    DbContextBuilder = ev => new ProductAuditNetDbContext(),
    AuditTypeMapper = (t, ee) => t == typeof(Product) ? typeof(ProductAudit)  : null,
    AuditEntityAction = (evt, entry, auditEntity) =>
    {
        var a = (dynamic)auditEntity;
        a.AuditDate = DateTime.UtcNow;
        a.UserName = evt.Environment.UserName;
        a.AuditAction = entry.Action;
       
        // Insert, Update, Delete
        return Task.FromResult(true); // return false to ignore the audit
    }
};
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
app.UseAuthorization();

app.MapControllers();

app.Run();