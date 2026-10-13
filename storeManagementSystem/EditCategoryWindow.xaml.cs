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
    public partial class EditCategoryWindow : Window
    {
        private readonly ApiService _apiService;
        private readonly Category _category;

        public EditCategoryWindow(Category category)
        {
            InitializeComponent();

            _apiService = new ApiService();
            _category = category;

            NameTextBox.Text = category.Name;
        }

        private async void SaveCategory_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Please enter category name.");
                return;
            }

            var updatedCategory = new Category
            {
                Id = _category.Id,
                Name = NameTextBox.Text
            };

            await _apiService.UpdateCategoryAsync(updatedCategory);

            MessageBox.Show("Category updated successfully!");

            DialogResult = true;
        }
    }
}