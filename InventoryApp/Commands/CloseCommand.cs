using InventoryApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace InventoryApp.Commands
{
    public class CloseCommand : CommandBase
    {
        public ViewModelBase ViewModel;
        public CloseCommand(ViewModelBase vm)
        {
            ViewModel = vm;
        }

        public override void Execute(object parameter)
        {
            ViewModel.Close();
        }
    }
}
