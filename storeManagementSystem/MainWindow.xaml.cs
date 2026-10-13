using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using StoreManagementSystem.Core;
using storeManagementSystem.Services;
using StoreManagementSystem.Core.Entities;
using ProductEntity = StoreManagementSystem.Core.Entities.Product;

namespace storeManagementSystem
{
    public partial class MainWindow : Window
    {
        private readonly ApiService _apiService;

        public MainWindow()
        {
            InitializeComponent();

            _apiService = new ApiService();

            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var products = await _apiService.GetProductsAsync();

            ProductsDataGrid.ItemsSource = products;

            var categories = await _apiService.GetCategoriesAsync();

            CategoriesListBox.ItemsSource = categories;
        }
        private async void RefreshProducts_Click(object sender, RoutedEventArgs e)
        {
            var products = await _apiService.GetProductsAsync();

            ProductsDataGrid.ItemsSource = products;
        }
        private async void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductEntity? product = ProductsDataGrid.SelectedItem as ProductEntity;

            if (product == null)
            {
                MessageBox.Show("Please select a product.");
                return;
            }

            await _apiService.DeleteProductAsync(product.Id);

            MessageBox.Show("Product deleted successfully!");

            var products = await _apiService.GetProductsAsync();
            ProductsDataGrid.ItemsSource = products;
        }
        private async void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductEntity? product =
                ProductsDataGrid.SelectedItem as ProductEntity;

            if (product == null)
            {
                MessageBox.Show("Please select a product.");
                return;
            }

            EditProductWindow window = new EditProductWindow(product);

            window.Owner = this;

            if (window.ShowDialog() == true)
            {
                var products = await _apiService.GetProductsAsync();

                ProductsDataGrid.ItemsSource = products;
            }
        }
        private async void RefreshCategories_Click(object sender, RoutedEventArgs e)
        {
            var categories = await _apiService.GetCategoriesAsync();

            CategoriesListBox.ItemsSource = categories;
        }
        private async void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            AddCategoryWindow window = new AddCategoryWindow();
            window.Owner = this;

            if (window.ShowDialog() == true)
            {
                var categories = await _apiService.GetCategoriesAsync();
                CategoriesListBox.ItemsSource = categories;
            }
        }
        private async void EditCategory_Click(object sender, RoutedEventArgs e)
        {
            Category? category =
                CategoriesListBox.SelectedItem as Category;

            if (category == null)
            {
                MessageBox.Show("Please select a category.");
                return;
            }

            EditCategoryWindow window =
                new EditCategoryWindow(category);

            window.Owner = this;

            if (window.ShowDialog() == true)
            {
                var categories = await _apiService.GetCategoriesAsync();

                CategoriesListBox.ItemsSource = categories;
            }
        }
        private async void DeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            Category? category =
                CategoriesListBox.SelectedItem as Category;

            if (category == null)
            {
                MessageBox.Show("Please select a category.");
                return;
            }

            var result = MessageBox.Show(
                $"Are you sure you want to delete '{category.Name}'?",
                "Delete Category",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes)
                return;

            await _apiService.DeleteCategoryAsync(category.Id);

            MessageBox.Show("Category deleted successfully!");

            var categories = await _apiService.GetCategoriesAsync();

            CategoriesListBox.ItemsSource = categories;
        }
    }
}