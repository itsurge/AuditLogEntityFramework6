using AuditLog.WebApi.DbContext;
using AuditLog.WebApi.DbContext.Models;
using AuditLog.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuditLog.WebApi.Controllers;

[Route("api/[controller]")]
public class ProductEf6TemporalController:ControllerBase
{
   
    private readonly ProductEf6TemporalDbContext _dbcontext;
    public ProductEf6TemporalController( ProductEf6TemporalDbContext dbcontext)
    {
       
        _dbcontext = dbcontext;
    }
    
    [HttpPost]
    public async  Task<IActionResult> AddProduct([FromBody] AddProductViewModel productViewModel)
    {

        var product = new Product(productViewModel.Category, productViewModel.Price, productViewModel.Name,
            Guid.NewGuid());
        
        await _dbcontext.Products.AddAsync(product);
        await _dbcontext.SaveChangesAsync();
        return Ok(product.Id);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductViewModel productViewModel)
    {
        
        var product = await _dbcontext.Products.SingleOrDefaultAsync(rec => rec.Id == productViewModel.Id);
        if (product is null) return BadRequest();
        product.Update(productViewModel.Category,productViewModel.Price,productViewModel.Name);
        await _dbcontext.SaveChangesAsync();
        return Ok();
    }
}