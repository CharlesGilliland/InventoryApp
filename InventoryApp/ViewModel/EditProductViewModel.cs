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
    public class EditProductViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        #region Fields
        private ObservableCollection<Product> products;
        private Product selectedProduct;
        private string productName;
        private string productCode;
        private string productDescription;
        private ObservableCollection<Warehouse> warehouses;
        private Warehouse selectedWarehouse;
        private readonly ErrorsViewModel errorsViewModel;
        #endregion

        #region Properties
        public ObservableCollection<Product> Products
        {
            get { return products; }
            set
            {
                products = value;
                OnPropertyChanged("Products");
            }
        }
        public Product SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
            }
        }
        public string ProductName 
        {
            get { return productName; }
            set
            {
                productName = value;
                errorsViewModel.ClearErrors(nameof(ProductName));
                if (string.IsNullOrWhiteSpace(productName))
                {
                    errorsViewModel.AddError(nameof(ProductName), "Product name cannot be null.");
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
        #endregion

        #region Commands
        public DisplayProductValuesCommand DisplayValuesCommand { get; set; }
        public EditProductCommand EditProductCommand { get; set; }
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

        public EditProductViewModel()
        {
            Products = new ObservableCollection<Product>();
            Warehouses = new ObservableCollection<Warehouse>();

            DisplayValuesCommand = new DisplayProductValuesCommand(this);
            EditProductCommand = new EditProductCommand(this);
            CloseCommand = new CloseCommand(this);

            errorsViewModel = new ErrorsViewModel();
            errorsViewModel.ErrorsChanged += ErrorsViewModel_ErrorsChanged;
        }


        #region Methods
        public void GetProducts()
        {
            List<Product> products = DatabaseAccessHelper.Read<Product>();
            Products.Clear();
            foreach(Product product in products)
            {
                Products.Add(product);
            }
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
            Warehouse currentWarehouse = DatabaseAccessHelper.Read<Warehouse>().Where(x => x.ID == SelectedProduct.WarehouseNo).ToList().First();

            ProductName = SelectedProduct.Name;
            ProductCode = SelectedProduct.ProductCode;
            ProductDescription = SelectedProduct.Description;
            SelectedWarehouse = currentWarehouse;
        }
        public void EditProduct()
        {
            SelectedProduct.Name = ProductName;
            SelectedProduct.ProductCode = ProductCode;
            SelectedProduct.Description = ProductDescription;
            SelectedProduct.WarehouseNo = SelectedWarehouse.ID;

            MessageBoxResult result = MessageBox.Show($"Are you sure you want to update {SelectedProduct} with the following values\n{ProductName}\n{ProductCode}\n{ProductDescription}\n{SelectedWarehouse}", "Confirm Change", MessageBoxButton.OKCancel);

            if(result == MessageBoxResult.OK)
            {
                DatabaseAccessHelper.Update(SelectedProduct);
            }
            else
            {
                MessageBox.Show("No change made.");
            }
            CloseAction();
        }
        
        #endregion
    }
}
