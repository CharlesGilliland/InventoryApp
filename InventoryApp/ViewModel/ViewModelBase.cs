using InventoryApp.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace InventoryApp.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public Action CloseAction;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public void Close()
        {
            CloseAction();
        }
    }
}
