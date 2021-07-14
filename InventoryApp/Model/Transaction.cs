using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace InventoryApp.Model
{
    public class Transaction
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        // public int TargetUserID { get; set; }
        public DateTime TimeProcessed { get; set; }
        public int TargetProductID { get; set; }
        public string TargetProductName { get; set; }
        public int TransactionTypeValue { get; set; }
        public int Quantity { get; set; }
        public double PricePerUnit { get; set; }
        public double TotalPrice { get; set; }
        public string Comment { get; set; }

    }
    public enum TransactionType : int
    {
        SALE = 0,
        PURCHASE = 1,
        STOCKADJUSTMENT = 2
    }
}
