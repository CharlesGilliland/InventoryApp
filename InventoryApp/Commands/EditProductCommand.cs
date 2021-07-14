using InventoryApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryApp.Commands
{
    public class EditProductCommand : CommandBase
    {
        EditProductViewModel viewModel;
        public EditProductCommand(EditProductViewModel vm)
        {
            viewModel = vm;
        }
        public override void Execute(object parameter)
        {
            viewModel.EditProduct();
        }
    }
}
