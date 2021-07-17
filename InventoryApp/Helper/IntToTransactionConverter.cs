using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using InventoryApp.Model;

namespace InventoryApp.Helper
{
    public class IntToTransactionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int _value = System.Convert.ToInt32(value);
            TransactionType transactionType = (TransactionType)value;
            switch (transactionType)
            {
                case TransactionType.SALE:
                    return "Sale";
                case TransactionType.PURCHASE:
                    return "Purchase";
                case TransactionType.STOCKADJUSTMENT:
                    return "Stock Adjustment";
                default:
                    return "ERROR";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
