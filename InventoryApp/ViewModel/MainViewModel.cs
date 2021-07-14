using InventoryApp.Commands;
using InventoryApp.Helper;
using InventoryApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace InventoryApp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields
        private ObservableCollection<Product> products;
        private ObservableCollection<Transaction> transactions;
        private ObservableCollection<Warehouse> warehouses;
        private Visibility productsVis;
        private Visibility transactionsVis;
        private Warehouse selectedWarehouse;
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
        public ObservableCollection<Transaction> Transactions
        {
            get { return transactions; }
            set
            {
                transactions = value;
                OnPropertyChanged("Transactions");
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
                SelectedWarehouseChanged?.Invoke(this, new EventArgs());
            }
        }

        private bool isTransactionsVisible = false;

        public Visibility ProductsVis
        {
            get { return productsVis; }
            set
            {
                productsVis = value;
                OnPropertyChanged("ProductsVis");
            }
        }
        public Visibility TransactionsVis
        {
            get { return transactionsVis; }
            set
            {
                transactionsVis = value;
                OnPropertyChanged("TransactionsVis");
            }
        }
        #endregion

        #region Commands
        public ShowTransactionsCommand ShowTransactionsCommand { get; set; }

        #endregion

        public event EventHandler SelectedWarehouseChanged;

        public MainViewModel()
        {
            // Initializing values
            Products = new ObservableCollection<Product>();
            Transactions = new ObservableCollection<Transaction>();
            Warehouses = new ObservableCollection<Warehouse>();
            Warehouses.Add(new Warehouse() { WarehouseName = "All Warehouses" });
            ProductsVis = Visibility.Visible;
            TransactionsVis = Visibility.Collapsed;

            // Initializing commands
            ShowTransactionsCommand = new ShowTransactionsCommand(this);

            SelectedWarehouseChanged += OnSelectedWarehouseChanged;

        }

        #region Methods
        // Get the Products from the database and adds them to the observable collection
        public void GetProducts()
        {
            List<Product> products;
            products = DatabaseAccessHelper.Read<Product>();
            Products.Clear();
            foreach(Product product in products)
            {
                Products.Add(product);
            }
        }
        // Gets transactions from the database and adds them to the observable collection
        public void GetTransactions()
        {
            List<Transaction> transactions = DatabaseAccessHelper.Read<Transaction>();
            Transactions.Clear();
            foreach(Transaction transaction in transactions)
            {
                Transactions.Add(transaction);
            }
        }
        // Gets the Warehouses from the database and adds them to tthe observable collection
        public void GetWarehouses()
        {
            List<Warehouse> warehouses = DatabaseAccessHelper.Read<Warehouse>();
            Warehouses.Clear();
            Warehouses.Add(new Warehouse() { WarehouseName = "All Warehouses" });
            foreach(Warehouse warehouse in warehouses)
            {
                Warehouses.Add(warehouse);
            }
        }

        // Shows the transactions data grid 
        public void ShowTransactions()
        {
            if (!isTransactionsVisible)
            {
                TransactionsVis = Visibility.Visible;
                ProductsVis = Visibility.Collapsed;
            }
            else
            {
                TransactionsVis = Visibility.Collapsed;
                ProductsVis = Visibility.Visible;
            }
            isTransactionsVisible = !isTransactionsVisible;
        }

        public void OnSelectedWarehouseChanged(object sender, EventArgs e)
        {
            if(SelectedWarehouse == null)
            {
                return;
            }
            if(SelectedWarehouse.WarehouseName == "All Warehouses")
            {
                GetProducts();
                return;
            }
            if(SelectedWarehouse != null)
            {
                List<Product> selectedStock = DatabaseAccessHelper.Read<Product>().Where(x => x.WarehouseNo == SelectedWarehouse.ID).ToList();
                Products.Clear();
                foreach(Product product in selectedStock)
                {
                    Products.Add(product);
                }
            }
        }


        #endregion

    }
}
