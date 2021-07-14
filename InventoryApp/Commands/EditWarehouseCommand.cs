using InventoryApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryApp.Commands
{
    public class EditWarehouseCommand : CommandBase
    {
        EditWarehouseViewModel viewModel;
        public EditWarehouseCommand(EditWarehouseViewModel vm)
        {
            viewModel = vm;
        }
        public override void Execute(object parameter)
        {
            viewModel.EditWarehouse();
        }
    }
}
