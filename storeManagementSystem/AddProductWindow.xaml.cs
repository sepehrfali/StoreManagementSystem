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
using StoreManagementSystem.Core.Entities;
using storeManagementSystem.Services;

namespace storeManagementSystem
{
    public partial class AddProductWindow : Window
    {
        private readonly ApiService _apiService;

        public AddProductWindow()
        {
            InitializeComponent();

            _apiService = new ApiService();
            Loaded += AddProductWindow_Loaded;
        }

        private async void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Please enter product name.");
                return;
            }

            if (!decimal.TryParse(PriceTextBox.Text, out decimal price))
            {
                MessageBox.Show("Please enter a valid price.");
                return;
            }

            if (!int.TryParse(QuantityTextBox.Text, out int quantity))
            {
                MessageBox.Show("Please enter a valid quantity.");
                return;
            }

            if (CategoryComboBox.SelectedItem is not Category selectedCategory)
            {
                MessageBox.Show("Please select a category.");
                return;
            }
            var product = new Product
            {
                Name = NameTextBox.Text,
                Price = price,
                Quantity = quantity,
                CategoryId = selectedCategory.Id
            };

            await _apiService.AddProductAsync(product);

            MessageBox.Show("Product added successfully!");

            DialogResult = true;
        }
        private async void AddProductWindow_Loaded(object sender,RoutedEventArgs e)
        {
            var categories = await _apiService.GetCategoriesAsync();

            CategoryComboBox.ItemsSource = categories;
        }

    }
}