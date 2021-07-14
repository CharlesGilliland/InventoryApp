using InventoryApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryApp.Commands
{
    public class DisplayWarehouseValuesCommand : CommandBase
    {
        EditWarehouseViewModel viewModel;
        public DisplayWarehouseValuesCommand(EditWarehouseViewModel vm)
        {
            viewModel = vm;
        }
        public override void Execute(object parameter)
        {
            viewModel.DisplayValues();
        }
    }
}
