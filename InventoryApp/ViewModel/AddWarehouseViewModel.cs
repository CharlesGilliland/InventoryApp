using InventoryApp.Commands;
using InventoryApp.Helper;
using InventoryApp.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace InventoryApp.ViewModel
{
    public class AddWarehouseViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private string warehouseName;
        private readonly ErrorsViewModel errorsViewModel;

        public string WarehouseName
        {
            get { return warehouseName; }
            set
            {
                warehouseName = value;
                errorsViewModel.ClearErrors(nameof(WarehouseName));
                if (string.IsNullOrWhiteSpace(warehouseName))
                {
                    errorsViewModel.AddError(nameof(WarehouseName), "Warehouse name cannot be null");
                }
                OnPropertyChanged("WarehouseName");
            }
        }

        public CloseCommand CloseCommand { get; set; }
        public AddWarehouseCommand AddWarehouseCommand { get; set; }

        #region ErrorsViewModel Code
        public bool HasErrors => errorsViewModel.HasErrors;
        public bool CanCreate => !HasErrors;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public IEnumerable GetErrors(string propertyName)
        {
            return errorsViewModel.GetErrors(propertyName);
        }
        private void ErrorsViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
            OnPropertyChanged(nameof(CanCreate));
        }
        #endregion

        public AddWarehouseViewModel()
        {
            CloseCommand = new CloseCommand(this);
            AddWarehouseCommand = new AddWarehouseCommand(this);

            errorsViewModel = new ErrorsViewModel();
            errorsViewModel.ErrorsChanged += ErrorsViewModel_ErrorsChanged;
        }

        public void CreateWarehouse()
        {
            Warehouse warehouse = new Warehouse()
            {
                WarehouseName = WarehouseName
            };
            DatabaseAccessHelper.Insert(warehouse);
            CloseAction();
        }
    }
}
