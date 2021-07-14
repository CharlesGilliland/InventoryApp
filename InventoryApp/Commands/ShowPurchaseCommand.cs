using InventoryApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryApp.Commands
{
    public class ShowPurchaseCommand : CommandBase
    {
        public ProcessTransactionViewModel viewModel;
        public ShowPurchaseCommand(ProcessTransactionViewModel vm)
        {
            viewModel = vm;
        }
        public override void Execute(object parameter)
        {
            viewModel.ShowPurchase();
        }
    }
}
