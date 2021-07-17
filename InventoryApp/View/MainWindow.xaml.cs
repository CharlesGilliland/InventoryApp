using InventoryApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InventoryApp.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = Resources["vm"] as MainViewModel;
            viewModel.GetProducts();
            viewModel.GetTransactions();
            viewModel.GetWarehouses();
        }

        

        private void ProcessTransactionButton_Click(object sender, RoutedEventArgs e)
        {
            ProcessTransactionWindow processTransactionWindow = new ProcessTransactionWindow();
            processTransactionWindow.Owner = this;
            processTransactionWindow.ShowDialog();

            viewModel.GetProducts();
            viewModel.GetTransactions();
        }

        private void AddWarehouseButton_Click(object sender, RoutedEventArgs e)
        {
            AddWarehouseWindow addWarehouseWindow = new AddWarehouseWindow();
            addWarehouseWindow.Owner = this;
            addWarehouseWindow.ShowDialog();

            viewModel.GetWarehouses();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void FullScreenButton_Click(object sender, RoutedEventArgs e)
        {
            if(WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AboutLink_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Owner = this;
            aboutWindow.Show();
        }

        private void EditProductButton_Click(object sender, RoutedEventArgs e)
        {
            EditProductWindow editProductWindow = new EditProductWindow();
            editProductWindow.Owner = this;
            editProductWindow.ShowDialog();

            viewModel.GetProducts();
        }

        private void EditWarehouseButton_Click(object sender, RoutedEventArgs e)
        {
            EditWarehouseWindow editWarehouseWindow = new EditWarehouseWindow();
            editWarehouseWindow.Owner = this;
            editWarehouseWindow.ShowDialog();

            viewModel.GetWarehouses();
        }
    }
}
