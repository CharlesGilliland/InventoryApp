using InventoryApp.Commands;
using InventoryApp.Helper;
using InventoryApp.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace InventoryApp.ViewModel
{
    public class AddProductViewModel : ViewModelBase, INotifyDataErrorInfo 
    {
        #region Fields
        private string productName;
        private string productCode;
        private string productDescription;
        private int quantity;
        private Warehouse selectedWarehouse;
        private readonly ErrorsViewModel errorsViewModel;
        private ObservableCollection<Warehouse> warehouses;

        
        #endregion

        #region Properties
        public string ProductName
        {
            get { return productName; }
            set 
            {
                productName = value;
                errorsViewModel.ClearErrors(nameof(ProductName));
                if (string.IsNullOrWhiteSpace(productName))
                {
                    errorsViewModel.AddError(nameof(ProductName), "Product name must be specified.");
                }

                OnPropertyChanged("ProductName");
            }
        }
        public string ProductCode
        {
            get { return productCode; }
            set 
            { 
                productCode = value;
                errorsViewModel.ClearErrors(nameof(ProductCode));
                if (string.IsNullOrWhiteSpace(productCode))
                {
                    errorsViewModel.AddError(nameof(ProductCode), "Product code must be specified.");
                }
                OnPropertyChanged("ProductCode");
            }
        }
        public string ProductDescription
        {
            get { return productDescription; }
            set 
            { 
                productDescription = value;
                
                OnPropertyChanged("ProductDescription");
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
        public int Quantity
        {
            get { return quantity; }
            set 
            { 
                quantity = value;
                OnPropertyChanged("Quantity");
            }
        }
        public ObservableCollection<Warehouse> Warehouses
        {
            get { return warehouses; }
            set
            {
                warehouses = value;
                OnPropertyChanged("Warehouses");
            }
        }
        
        #endregion

        #region Commands
        public AddProductCommand AddProductCommand { get; set; }
        public CloseCommand CloseCommand { get; set; }


        #endregion

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

        // Constrcutor
        public AddProductViewModel()
        {
            warehouses = new ObservableCollection<Warehouse>();

            AddProductCommand = new AddProductCommand(this);
            CloseCommand = new CloseCommand(this);

            errorsViewModel = new ErrorsViewModel();
            errorsViewModel.ErrorsChanged += ErrorsViewModel_ErrorsChanged;
        }



        #region Methods

        // Creates a product and then calls the close action on the window
        public void CreateProduct()
        {
            Product product;
            if (!string.IsNullOrWhiteSpace(ProductDescription))
            {
                product = new Product()
                {
                    Name = ProductName,
                    ProductCode = ProductCode,
                    Description = ProductDescription,
                    WarehouseNo = SelectedWarehouse.ID,
                    Quantity = Quantity
                };
            }
            else
            {
                product = new Product()
                {
                    Name = ProductName,
                    ProductCode = ProductCode,
                    WarehouseNo = SelectedWarehouse.ID,
                    Quantity = Quantity
                };
            }
            
            DatabaseAccessHelper.Insert(product);
            CloseAction();
        }

        public void GetWarehouses()
        {
            List<Warehouse> warehouses = DatabaseAccessHelper.Read<Warehouse>();
            Warehouses.Clear();
            foreach(Warehouse warehouse in warehouses)
            {
                Warehouses.Add(warehouse);
            }
        }

        public void EditProduct()
        {

        }
        

        #endregion

    }
}
