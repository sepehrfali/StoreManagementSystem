using Microsoft.EntityFrameworkCore;
using StoreManagementSystem.Core.Entities;
using StoreManagementSystem.Data.Context;

namespace StoreManagementSystem.Data.Repositories
{
    public class CategoryRepository
    {
        private readonly StoreDbContext _context;

        public CategoryRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories
                .ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            var existingCategory = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == category.Id);

            if (existingCategory is null)
                return;

            existingCategory.Name = category.Name;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category is null)
                return;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}