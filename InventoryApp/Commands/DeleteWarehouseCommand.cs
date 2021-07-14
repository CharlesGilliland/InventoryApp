using InventoryApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryApp.Commands
{
    public class DeleteWarehouseCommand : CommandBase
    {
        EditWarehouseViewModel viewModel;
        public DeleteWarehouseCommand(EditWarehouseViewModel vm)
        {
            viewModel = vm;
        }
        public override void Execute(object parameter)
        {
            viewModel.DeleteWarehouse();
        }
    }
}
