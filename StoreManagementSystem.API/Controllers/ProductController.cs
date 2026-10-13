using Microsoft.AspNetCore.Mvc;
using StoreManagementSystem.Core.Entities;
using StoreManagementSystem.Services.Services;

namespace StoreManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllAsync();

        return Ok(products);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product == null)
            return NotFound();

        return Ok(product);
    }
    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        var result = await _productService.AddAsync(product);

        if (!result)
        {
            return BadRequest("The selected category does not exist.");
        }

        return Ok(product);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Product product)
    {
        if (id != product.Id)
            return BadRequest();

        var existingProduct = await _productService.GetByIdAsync(id);

        if (existingProduct == null)
            return NotFound();

        await _productService.UpdateAsync(product);

        return Ok(product);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product == null)
            return NotFound();

        await _productService.DeleteAsync(id);

        return NoContent();
    }
}