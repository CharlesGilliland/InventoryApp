using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace InventoryApp.Model
{
    public class Product
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; } = "No description given";
        public int Quantity { get; set; }
        // public int User { get; set; }
        public int WarehouseNo { get; set; }

        public override string ToString()
        {
            return $"{Name} - {Description}";
        }


    }
}
