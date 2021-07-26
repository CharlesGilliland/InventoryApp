using InventoryApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryApp.Commands
{
    public class ShowLoginRegisterFocusCommand : CommandBase
    {
        LoginViewModel viewModel;
        public ShowLoginRegisterFocusCommand(LoginViewModel vm)
        {
            viewModel = vm;
        }
        public override void Execute(object parameter)
        {
            viewModel.ChangeFocus();
        }
    }
}
