using InventoryApp.Model;
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
    /// Interaction logic for AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        public AddProductWindow()
        {
            AddProductViewModel viewModel = new AddProductViewModel();
            DataContext = viewModel;
            if (((AddProductViewModel)DataContext).CloseAction == null)
            {
                ((AddProductViewModel)DataContext).CloseAction = new Action(() => this.Close());
            }
            viewModel.GetWarehouses();
            InitializeComponent();
        }
    }
}
