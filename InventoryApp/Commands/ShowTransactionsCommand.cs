using InventoryApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryApp.Commands
{
    public class ShowTransactionsCommand : CommandBase
    {
        public MainViewModel ViewModel;
        public ShowTransactionsCommand(MainViewModel vm)
        {
            ViewModel = vm;
        }
        public override void Execute(object parameter)
        {
            ViewModel.ShowTransactions();
        }
    }
}
