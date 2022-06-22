namespace AuditLog.WebApi.DbContext.Models;

public class Product
{
    public Product(string category, decimal price, string name, Guid id)
    {
        Category = category;
        Price = price;
        Name = name;
        Id = id;
    }

    public Product()
    {
        
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public string Category { get; private set; }

    public void Update(string category, decimal price, string name)
    {
        Name = name;
        Price = price;
        Category = category;

    }
    
}