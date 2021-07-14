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
    /// Interaction logic for EditWarehouseWindow.xaml
    /// </summary>
    public partial class EditWarehouseWindow : Window
    {
        public EditWarehouseWindow()
        {
            EditWarehouseViewModel viewModel = new EditWarehouseViewModel();
            DataContext = viewModel;
            if (((EditWarehouseViewModel)DataContext).CloseAction == null)
            {
                ((EditWarehouseViewModel)DataContext).CloseAction = new Action(() => this.Close());
            }
            viewModel.GetWarehouses();
            InitializeComponent();
        }
    }
}
