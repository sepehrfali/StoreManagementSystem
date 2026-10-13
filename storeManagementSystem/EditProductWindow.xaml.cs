using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using storeManagementSystem.Services;
using StoreManagementSystem.Core.Entities;
using ProductEntity = StoreManagementSystem.Core.Entities.Product;

namespace storeManagementSystem
{
    public partial class EditProductWindow : Window
    {
        private readonly ApiService _apiService;
        private readonly Product _product;

        public EditProductWindow(Product product)
        {
            InitializeComponent();

            _apiService = new ApiService();
            _product = product;

            NameTextBox.Text = product.Name;
            PriceTextBox.Text = product.Price.ToString();
            QuantityTextBox.Text = product.Quantity.ToString();

            Loaded += EditProductWindow_Loaded;
        }

        private async void EditProductWindow_Loaded(
            object sender,
            RoutedEventArgs e)
        {
            var categories = await _apiService.GetCategoriesAsync();

            CategoryComboBox.ItemsSource = categories;

            foreach (var category in categories!)
            {
                if (category.Id == _product.CategoryId)
                {
                    CategoryComboBox.SelectedItem = category;
                    break;
                }
            }
        }

        private async void SaveProduct_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Please enter product name.");
                return;
            }

            if (!decimal.TryParse(
                    PriceTextBox.Text,
                    out decimal price))
            {
                MessageBox.Show("Please enter a valid price.");
                return;
            }

            if (price < 0)
            {
                MessageBox.Show("Price cannot be negative.");
                return;
            }

            if (!int.TryParse(
                    QuantityTextBox.Text,
                    out int quantity))
            {
                MessageBox.Show("Please enter a valid quantity.");
                return;
            }

            if (quantity < 0)
            {
                MessageBox.Show("Quantity cannot be negative.");
                return;
            }

            if (CategoryComboBox.SelectedItem is not Category selectedCategory)
            {
                MessageBox.Show("Please select a category.");
                return;
            }

            var updatedProduct = new Product
            {
                Id = _product.Id,
                Name = NameTextBox.Text,
                Price = price,
                Quantity = quantity,
                CategoryId = selectedCategory.Id
            };

            try
            {
                await _apiService.UpdateProductAsync(updatedProduct);

                MessageBox.Show(
                    "Product updated successfully!",
                    "Success",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                DialogResult = true;
            }
            catch
            {
                MessageBox.Show(
                    "The selected category does not exist.",
                    "Update Failed",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }
    }
}