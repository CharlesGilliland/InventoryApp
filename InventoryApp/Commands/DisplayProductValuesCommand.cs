using InventoryApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryApp.Commands
{
    public class DisplayProductValuesCommand : CommandBase
    {
        EditProductViewModel viewModel;
        public DisplayProductValuesCommand(EditProductViewModel vm)
        {
            viewModel = vm;
        }
        public override void Execute(object parameter)
        {
            viewModel.DisplayValues();
        }
    }
}
