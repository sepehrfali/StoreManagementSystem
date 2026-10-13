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
    public partial class AddCategoryWindow : Window
    {
        private readonly ApiService _apiService;

        public AddCategoryWindow()
        {
            InitializeComponent();

            _apiService = new ApiService();
        }

        private async void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Please enter category name.");
                return;
            }

            var category = new Category
            {
                Name = NameTextBox.Text
            };

            await _apiService.AddCategoryAsync(category);

            MessageBox.Show("Category added successfully!");

            DialogResult = true;
        }
    }
}