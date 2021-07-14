using InventoryApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace InventoryApp.Commands
{
    public class AddProductCommand : CommandBase
    {
        public AddProductViewModel ViewModel { get; set; }
        public AddProductCommand(AddProductViewModel vm)
        {
            ViewModel = vm;
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            ViewModel.CreateProduct();
            
        }
    }
}
