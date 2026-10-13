using Microsoft.AspNetCore.Mvc;
using StoreManagementSystem.Core.Entities;
using StoreManagementSystem.Services.Services;

namespace StoreManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly CategoryService _categoryService;

    public CategoryController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryService.GetAllAsync();

        return Ok(categories);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);

        if (category == null)
            return NotFound();

        return Ok(category);
    }
    [HttpPost]
    public async Task<IActionResult> Create(Category category)
    {
        await _categoryService.AddAsync(category);

        return Ok(category);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Category category)
    {
        if (id != category.Id)
            return BadRequest();

        var existingCategory = await _categoryService.GetByIdAsync(id);

        if (existingCategory == null)
            return NotFound();

        await _categoryService.UpdateAsync(category);

        return Ok(category);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);

        if (category == null)
            return NotFound();

        await _categoryService.DeleteAsync(id);

        return NoContent();
    }
}