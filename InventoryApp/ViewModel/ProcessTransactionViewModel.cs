using InventoryApp.Commands;
using InventoryApp.Helper;
using InventoryApp.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace InventoryApp.ViewModel
{
    public class ProcessTransactionViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        #region Fields

        // Sale
        private Product saleSelectedProduct;
        private int saleSelectedQuantity;
        private double salePricePerUnit;

        // Purchase
        private string purchaseProductName;
        private string purchaseProductCode;
        private string purchaseProductDescription;
        private ObservableCollection<Warehouse> warehouses;
        private Warehouse selectedWarehouse;
        private int purchaseQuantity;
        private double purchasePricePerUnit;

        // Stock Adjustment
        private Product stockSelectedProduct;
        private int stockSelectedQuantity;
        private string stockComment;

        private readonly ErrorsViewModel errorsViewModel;

        private Visibility saleVis;
        private Visibility purchaseVis;
        private Visibility stockAdjustmentVis;
        #endregion

        #region Properties
        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<int> Quantities { get; set; }

        // Sale
        public Product SaleSelectedProduct
        {
            get { return saleSelectedProduct; }
            set
            {
                saleSelectedProduct = value;
                OnPropertyChanged("SaleSelectedProduct");
            }
        }
        public int SaleSelectedQuantity
        {
            get { return saleSelectedQuantity; }
            set
            {
                saleSelectedQuantity = value;
                OnPropertyChanged("SaleSelectedQuantity");
            }
        }
        public double SalePricePerUnit
        {
            get { return salePricePerUnit; }
            set
            {
                salePricePerUnit = value;
                errorsViewModel.ClearErrors(nameof(SalePricePerUnit));
                if(salePricePerUnit < 0)
                {
                    errorsViewModel.AddError(nameof(SalePricePerUnit), "Price cannot be below 0");
                }
                OnPropertyChanged("SalePricePerUnit");
            }
        }

        // Purchase
        public string PurchaseProductName
        {
            get { return purchaseProductName; }
            set
            {
                purchaseProductName = value;
                OnPropertyChanged("PurchaseProductName");
            }
        }
        public string PurchaseProductCode
        {
            get { return purchaseProductCode; }
            set
            {
                purchaseProductCode = value;
                OnPropertyChanged("PurchaseProductCode");
            }
        }
        public string PurchaseProductDescription
        {
            get { return purchaseProductDescription; }
            set
            {
                purchaseProductDescription = value;
                OnPropertyChanged("PurchaseProductDescription");
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
        public int PurchaseQuantity
        {
            get { return purchaseQuantity; }
            set
            {
                purchaseQuantity = value;
                OnPropertyChanged("PurchaseQuantity");
            }
        }
        public double PurchasePricePerUnit
        {
            get { return purchasePricePerUnit; }
            set
            {
                purchasePricePerUnit = value;
                OnPropertyChanged("PurchasePricePerUnit");
            }
        }

        // Stock Adjustment
        public Product StockSelectedProduct
        {
            get { return stockSelectedProduct; }
            set
            {
                stockSelectedProduct = value;
                OnPropertyChanged("StockSelectedProduct");
            }
        }
        public int StockSelectedQuantity
        {
            get { return stockSelectedQuantity; }
            set
            {
                stockSelectedQuantity = value;
                OnPropertyChanged("StockSelectedQuantity");
            }
        }
        public string StockComment
        {
            get { return stockComment; }
            set
            {
                stockComment = value;
                OnPropertyChanged("StockComment");
            }
        }

        public Visibility SaleVis
        {
            get { return saleVis; }
            set
            {
                saleVis = value;
                OnPropertyChanged("SaleVis");
            }
        }
        public Visibility PurchaseVis
        {
            get { return purchaseVis; }
            set
            {
                purchaseVis = value;
                OnPropertyChanged("PurchaseVis");
            }
        }
        public Visibility StockAdjustmentVis
        {
            get { return stockAdjustmentVis; }
            set
            {
                stockAdjustmentVis = value;
                OnPropertyChanged("StockAdjustmentVis");
            }
        }



        #endregion


        #region Commands
        public ProcessTransactionCommand ProcessTransactionCommand { get; set; }
        public UpdateQuantityCommand UpdateQuantityCommand { get; set; }
        public CloseCommand CloseCommand { get; set; }
        public ShowSaleCommand ShowSaleCommand { get; set; }
        public ShowPurchaseCommand ShowPurchaseCommand { get; set; }
        public ShowStockAdjustmentCommand ShowStockAdjustmentCommand { get; set; }
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

        public ProcessTransactionViewModel()
        {
            errorsViewModel = new ErrorsViewModel();
            errorsViewModel.ErrorsChanged += ErrorsViewModel_ErrorsChanged;

            ProcessTransactionCommand = new ProcessTransactionCommand(this);
            UpdateQuantityCommand = new UpdateQuantityCommand(this);
            CloseCommand = new CloseCommand(this);
            ShowSaleCommand = new ShowSaleCommand(this);
            ShowPurchaseCommand = new ShowPurchaseCommand(this);
            ShowStockAdjustmentCommand = new ShowStockAdjustmentCommand(this);

            Products = new ObservableCollection<Product>();
            Quantities = new ObservableCollection<int>();
            Warehouses = new ObservableCollection<Warehouse>();

            SaleVis = Visibility.Visible;
            PurchaseVis = Visibility.Collapsed;
            StockAdjustmentVis = Visibility.Collapsed;

            GetProducts();
            GetWarehouses();
        }
        

        #region Methods

        public void ProcessTransaction()
        {
            Transaction transaction;
            if(SaleVis == Visibility.Visible)
            {
                transaction = new Transaction()
                {
                    TimeProcessed = DateTime.Now,
                    TargetProductID = SaleSelectedProduct.ID,
                    TargetProductName = SaleSelectedProduct.Name,
                    TransactionTypeValue = 0,
                    Quantity = SaleSelectedQuantity,
                    PricePerUnit = SalePricePerUnit,
                    TotalPrice = SaleSelectedQuantity * SalePricePerUnit,
                    Comment = ""
                };
                DatabaseAccessHelper.Insert(transaction);

                int remainingQuantity = SaleSelectedProduct.Quantity - SaleSelectedQuantity;
                if(remainingQuantity == 0)
                {
                    DatabaseAccessHelper.Delete(SaleSelectedProduct);
                }
                else
                {
                    saleSelectedProduct.Quantity = remainingQuantity;
                    DatabaseAccessHelper.Update(SaleSelectedProduct);
                }
            } 
            else if(PurchaseVis == Visibility.Visible)
            {
                Product product = new Product()
                {
                    Name = PurchaseProductName,
                    ProductCode = PurchaseProductCode,
                    Description = PurchaseProductDescription,
                    Quantity = PurchaseQuantity,
                    WarehouseNo = SelectedWarehouse.ID
                };
                Product productMatch = DatabaseAccessHelper.Read<Product>().Find(x => (x.Name == product.Name) && (x.ProductCode == product.ProductCode) && (x.WarehouseNo == product.WarehouseNo));
                if (productMatch != null)
                {
                    productMatch.Quantity += PurchaseQuantity;
                    DatabaseAccessHelper.Update(productMatch);
                    transaction = new Transaction()
                    {
                        TimeProcessed = DateTime.Now,
                        TargetProductID = productMatch.ID,
                        TargetProductName = productMatch.Name,
                        TransactionTypeValue = 1,
                        Quantity = PurchaseQuantity,
                        PricePerUnit = PurchasePricePerUnit,
                        TotalPrice = PurchaseQuantity * PurchasePricePerUnit,
                        Comment = ""
                    };
                    DatabaseAccessHelper.Insert(transaction);
                }
                else
                {
                    DatabaseAccessHelper.Insert(product);
                    transaction = new Transaction()
                    {
                        TimeProcessed = DateTime.Now,
                        TargetProductID = product.ID,
                        TargetProductName = product.Name,
                        TransactionTypeValue = 1,
                        Quantity = PurchaseQuantity,
                        PricePerUnit = PurchasePricePerUnit,
                        TotalPrice = PurchaseQuantity * PurchasePricePerUnit,
                        Comment = ""
                    };
                    DatabaseAccessHelper.Insert(transaction);
                }
            }
            else if(StockAdjustmentVis == Visibility.Visible)
            {
                transaction = new Transaction()
                {
                    TimeProcessed = DateTime.Now,
                    TargetProductID = StockSelectedProduct.ID,
                    TargetProductName = StockSelectedProduct.Name,
                    TransactionTypeValue = 2,
                    Quantity = StockSelectedQuantity,
                    PricePerUnit = 0,
                    TotalPrice = 0,
                    Comment = StockComment
                };
                DatabaseAccessHelper.Insert(transaction);

                int remainingQuantity = StockSelectedProduct.Quantity - StockSelectedQuantity;
                if (remainingQuantity == 0)
                {
                    DatabaseAccessHelper.Delete(SaleSelectedProduct);
                }
                else
                {
                    saleSelectedProduct.Quantity = remainingQuantity;
                    DatabaseAccessHelper.Update(SaleSelectedProduct);
                }
            }

            
            CloseAction();
        }

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
            foreach(Warehouse warehouse in warehouses)
            {
                Warehouses.Add(warehouse);
            }
        }

        public void GetQuantities(Product selectedProduct)
        {
            if(SaleVis == Visibility.Visible)
            {
                int total = SaleSelectedProduct.Quantity;
                Quantities.Clear();
                for (int i = 1; i <= total; i++)
                {
                    Quantities.Add(i);
                }
            } else if(StockAdjustmentVis == Visibility.Visible)
            {
                int total = StockSelectedProduct.Quantity;
                Quantities.Clear();
                for (int i = 1; i <= total; i++)
                {
                    Quantities.Add(i);
                }
            }
        }

        public void ShowSale()
        {
            SaleVis = Visibility.Visible;
            PurchaseVis = Visibility.Collapsed;
            StockAdjustmentVis = Visibility.Collapsed;
        }
        public void ShowPurchase()
        {
            SaleVis = Visibility.Collapsed;
            PurchaseVis = Visibility.Visible;
            StockAdjustmentVis = Visibility.Collapsed;
        }
        public void ShowStockAdjustment()
        {
            SaleVis = Visibility.Collapsed;
            PurchaseVis = Visibility.Collapsed;
            StockAdjustmentVis = Visibility.Visible;
        }
        #endregion
    }
}
