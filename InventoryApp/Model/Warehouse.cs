using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace InventoryApp.Model
{
    public class Warehouse
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        // public int OwnerID { get; set; }
        public string WarehouseName { get; set; }

        public override string ToString()
        {
            return WarehouseName;
        }
    }
}
