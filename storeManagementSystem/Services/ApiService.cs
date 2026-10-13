using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using StoreManagementSystem.Core.Entities;

namespace storeManagementSystem.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7154/")
            };
        }

        public async Task<List<Product>?> GetProductsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Product>>(
                "api/Product");
        }

        public async Task<List<Category>?> GetCategoriesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Category>>(
                "api/Category");
        }
        public async Task<Product?> AddProductAsync(Product product)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "api/Product",
                product);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Product>();
        }
        public async Task DeleteProductAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Product/{id}");

            response.EnsureSuccessStatusCode();
        }
        public async Task UpdateProductAsync(Product product)
        {
            var response = await _httpClient.PutAsJsonAsync(
                $"api/Product/{product.Id}",
                product);

            response.EnsureSuccessStatusCode();
        }
        public async Task<Category?> AddCategoryAsync(Category category)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "api/Category",
                category);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Category>();
        }
        public async Task UpdateCategoryAsync(Category category)
        {
            var response = await _httpClient.PutAsJsonAsync(
                $"api/Category/{category.Id}",
                category);

            response.EnsureSuccessStatusCode();
        }
        public async Task DeleteCategoryAsync(int id)
        {
            var response = await _httpClient.DeleteAsync(
                $"api/Category/{id}");

            response.EnsureSuccessStatusCode();
        }
        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Category>(
                $"api/Category/{id}");
        }
    }
}