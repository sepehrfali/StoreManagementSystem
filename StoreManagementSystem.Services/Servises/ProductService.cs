
using StoreManagementSystem.Core.Entities;
using StoreManagementSystem.Data.Repositories;

namespace StoreManagementSystem.Services.Services
{
    public class ProductService
    
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly ProductRepository _productRepository;

        public ProductService(
        ProductRepository productRepository,
        CategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<List<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

      public async Task<bool> AddAsync(Product product)
{
    var category = await _categoryRepository.GetByIdAsync(product.CategoryId);

    if (category == null)
        return false;

    await _productRepository.AddAsync(product);

    return true;
}

        public async Task UpdateAsync(Product product)
        {
            await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }
    }
}