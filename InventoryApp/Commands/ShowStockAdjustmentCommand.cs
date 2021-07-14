using InventoryApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryApp.Commands
{
    public class ShowStockAdjustmentCommand : CommandBase
    {
        public ProcessTransactionViewModel viewModel;
        public ShowStockAdjustmentCommand(ProcessTransactionViewModel vm)
        {
            viewModel = vm;
        }
        public override void Execute(object parameter)
        {
            viewModel.ShowStockAdjustment();
        }
    }
}
