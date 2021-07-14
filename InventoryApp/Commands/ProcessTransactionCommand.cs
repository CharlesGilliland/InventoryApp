using InventoryApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace InventoryApp.Commands
{
    public class ProcessTransactionCommand : CommandBase
    {
        public ProcessTransactionViewModel ViewModel { get; set; }

        public ProcessTransactionCommand(ProcessTransactionViewModel vm)
        {
            ViewModel = vm;
        }

        public override void Execute(object parameter)
        {
            ViewModel.ProcessTransaction();
        }
    }
}
