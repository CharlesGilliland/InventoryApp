using InventoryApp.Commands;
using InventoryApp.Helper;
using InventoryApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace InventoryApp.ViewModel
{
    public class EditWarehouseViewModel : ViewModelBase
    {
        private ObservableCollection<Warehouse> warehouses;
        private Warehouse selectedWarehouse;
        private string warehouseName;

        public ObservableCollection<Warehouse> Warehouses
        {
            get { return warehouses; }
            set
            {
                warehouses = value;
                OnPropertyChanged("Warehouses");
            }
        }
        public Warehouse SelectedWarehouse
        {
            get { return selectedWarehouse; }
            set
            {
                selectedWarehouse = value;
                OnPropertyChanged("SelectedWarehouse");
            }
        }
        public string WarehouseName
        {
            get { return warehouseName; }
            set
            {
                warehouseName = value;
                OnPropertyChanged("WarehouseName");
            }
        }


        public DisplayWarehouseValuesCommand DisplayValuesCommand { get; set; }
        public EditWarehouseCommand EditWarehouseCommand { get; set; }
        public DeleteWarehouseCommand DeleteWarehouseCommand { get; set; }
        public CloseCommand CloseCommand { get; set; }

        public EditWarehouseViewModel()
        {
            Warehouses = new ObservableCollection<Warehouse>();

            DisplayValuesCommand = new DisplayWarehouseValuesCommand(this);
            EditWarehouseCommand = new EditWarehouseCommand(this);
            DeleteWarehouseCommand = new DeleteWarehouseCommand(this);
            CloseCommand = new CloseCommand(this);
        }

        public void GetWarehouses()
        {
            List<Warehouse> warehouses = DatabaseAccessHelper.Read<Warehouse>();
            Warehouses.Clear();
            foreach (Warehouse warehouse in warehouses)
            {
                Warehouses.Add(warehouse);
            }
        }
        public void DisplayValues()
        {
            WarehouseName = SelectedWarehouse.WarehouseName;
        }

        public void EditWarehouse()
        {
            SelectedWarehouse.WarehouseName = WarehouseName;
            DatabaseAccessHelper.Update(SelectedWarehouse);
            CloseAction();
        }

        public void DeleteWarehouse()
        {
            List<Product> currentStock = DatabaseAccessHelper.Read<Product>().Where(x => x.WarehouseNo == SelectedWarehouse.ID).ToList();

            if(currentStock.Count > 0)
            {
                MessageBox.Show("Cannot delete a warehouse if it still contains products.\nPlease remove all products before deleting.");
            }
            else
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete {SelectedWarehouse}?", "Warning", MessageBoxButton.YesNo);
                if(result == MessageBoxResult.Yes)
                {
                    DatabaseAccessHelper.Delete(SelectedWarehouse);
                }
                else
                {
                    MessageBox.Show("No change made.");
                }
            }
            CloseAction();
        }
    }
}
