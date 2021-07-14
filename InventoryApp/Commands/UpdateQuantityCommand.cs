using InventoryApp.Model;
using InventoryApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace InventoryApp.Commands
{
    public class UpdateQuantityCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public ProcessTransactionViewModel ViewModel { get; set; }

        public UpdateQuantityCommand(ProcessTransactionViewModel vm)
        {
            ViewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ViewModel.GetQuantities(parameter as Product);
        }
    }
}
