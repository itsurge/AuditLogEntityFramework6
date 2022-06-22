using AuditLog.WebApi.DbContext;
using AuditLog.WebApi.DbContext.Models;
using AuditLog.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuditLog.WebApi.Controllers;

[Route("api/[controller]")]
public class ProductEf6Controller:ControllerBase
{
   
    private readonly ProductEf6Dbcontext _ef6Dbcontext;
    public ProductEf6Controller( ProductEf6Dbcontext ef6Dbcontext)
    {
       
        _ef6Dbcontext = ef6Dbcontext;
    }
    
    [HttpPost]
    public async  Task<IActionResult> AddProduct([FromBody] AddProductViewModel productViewModel)
    {

        var product = new Product(productViewModel.Category, productViewModel.Price, productViewModel.Name,
            Guid.NewGuid());
        
        await _ef6Dbcontext.Products.AddAsync(product);
        await _ef6Dbcontext.SaveChangesAsync();
        return Ok(product.Id);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductViewModel productViewModel)
    {
        
        var product = await _ef6Dbcontext.Products.SingleOrDefaultAsync(rec => rec.Id == productViewModel.Id);
        if (product is null) return BadRequest();
        product.Update(productViewModel.Category,productViewModel.Price,productViewModel.Name);
        await _ef6Dbcontext.SaveChangesAsync();
        return Ok();
    }
}