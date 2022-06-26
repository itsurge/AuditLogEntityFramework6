using AuditLog.WebApi.DbContext;
using AuditLog.WebApi.DbContext.Models;
using AuditLog.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuditLog.WebApi.Controllers;

[Route("api/[controller]")]
public class ProductAuditNetController:ControllerBase
{
    private readonly ProductAuditNetDbContext _dbContext;
    public ProductAuditNetController(ProductAuditNetDbContext dbContext)
    {
        _dbContext = dbContext;
       
    }
    
    [HttpPost]
    public async  Task<IActionResult> AddProduct([FromBody] AddProductViewModel productViewModel)
    {

        var product = new Product(productViewModel.Category??string.Empty, productViewModel.Price, productViewModel.Name??string.Empty,
            Guid.NewGuid());
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
        
        return Ok(product.Id);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductViewModel productViewModel)
    {
        var product = await _dbContext.Products.SingleOrDefaultAsync(rec => rec.Id == productViewModel.Id);
        if (product is null) return BadRequest();
        product.Update(productViewModel.Category??string.Empty,productViewModel.Price,productViewModel.Name??string.Empty);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var product = await _dbContext.Products.SingleOrDefaultAsync(rec => rec.Id ==id);
        if (product is null) return BadRequest();
         _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync();
        return Ok();

    }


}

