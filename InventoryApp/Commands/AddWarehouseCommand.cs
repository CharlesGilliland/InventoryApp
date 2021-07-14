using InventoryApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace InventoryApp.Commands
{
    public class AddWarehouseCommand : CommandBase
    {
        public AddWarehouseViewModel ViewModel { get; set; }
        public AddWarehouseCommand(AddWarehouseViewModel vm)
        {
            ViewModel = vm;
        }
        public override void Execute(object parameter)
        {
            ViewModel.CreateWarehouse();
        }
    }
}
