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
    /// Interaction logic for ProcessTransactionWindow.xaml
    /// </summary>
    public partial class ProcessTransactionWindow : Window
    {
        public ProcessTransactionWindow()
        {
            DataContext = new ProcessTransactionViewModel();
            if (((ProcessTransactionViewModel)DataContext).CloseAction == null)
            {
                ((ProcessTransactionViewModel)DataContext).CloseAction = new Action(() => this.Close());
            }
            InitializeComponent();
        }
    }
}
