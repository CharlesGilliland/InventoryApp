using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace InventoryApp.Helper
{
    public class DatabaseAccessHelper
    {
        public static readonly string dbFile = System.IO.Path.Combine(Environment.CurrentDirectory, "inventory.db");
        
        public static bool Insert<T>(T item)
        {
            bool result = false;
            using (SQLiteConnection connection = new SQLiteConnection(dbFile))
            {
                connection.CreateTable<T>();
                int rows = connection.Insert(item);
                if(rows > 0)
                {
                    result = true;
                }
                return result;
            }
        }

        public static bool Update<T>(T item)
        {
            bool result = false;
            using (SQLiteConnection connection = new SQLiteConnection(dbFile))
            {
                connection.CreateTable<T>();
                int rows = connection.Update(item);
                if (rows > 0)
                {
                    result = true;
                }
                return result;
            }
        }

        public static bool Delete<T>(T item)
        {
            bool result = false;
            using (SQLiteConnection connection = new SQLiteConnection(dbFile))
            {
                connection.CreateTable<T>();
                int rows = connection.Delete(item);
                if (rows > 0)
                {
                    result = true;
                }
                return result;
            }
        }

        public static List<T> Read<T>() where T : new()
        {
            List<T> result;
            using (SQLiteConnection connection = new SQLiteConnection(dbFile))
            {
                connection.CreateTable<T>();
                result = connection.Table<T>().ToList();
                return result;
            }
        }
    }
}
