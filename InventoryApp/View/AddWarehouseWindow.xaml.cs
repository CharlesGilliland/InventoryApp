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
    /// Interaction logic for AddWarehouseWindow.xaml
    /// </summary>
    public partial class AddWarehouseWindow : Window
    {
        public AddWarehouseWindow()
        {
            AddWarehouseViewModel viewModel = new AddWarehouseViewModel();
            DataContext = viewModel;
            if (((AddWarehouseViewModel)DataContext).CloseAction == null)
            {
                ((AddWarehouseViewModel)DataContext).CloseAction = new Action(() => this.Close());
            }
            InitializeComponent();
        }
    }
}
