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
    /// Interaction logic for EditProductWindow.xaml
    /// </summary>
    public partial class EditProductWindow : Window
    {
        public EditProductWindow()
        {
            EditProductViewModel viewModel = new EditProductViewModel();
            DataContext = viewModel;
            if (((EditProductViewModel)DataContext).CloseAction == null)
            {
                ((EditProductViewModel)DataContext).CloseAction = new Action(() => this.Close());
            }
            viewModel.GetProducts();
            viewModel.GetWarehouses();
            InitializeComponent();
        }
    }
}
