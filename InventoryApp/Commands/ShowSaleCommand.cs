using InventoryApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryApp.Commands
{
    public class ShowSaleCommand : CommandBase
    {
        public ProcessTransactionViewModel viewModel;
        public ShowSaleCommand(ProcessTransactionViewModel vm)
        {
            viewModel = vm;
        }
        public override void Execute(object parameter)
        {
            viewModel.ShowSale();
        }
    }
}
