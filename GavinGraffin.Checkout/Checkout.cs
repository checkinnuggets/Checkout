using System;
using System.Collections.Generic;

namespace GavinGraffin.Checkout
{
    public class Checkout
    {
        private readonly IDictionary<string, decimal> _priceList;
        private readonly IDictionary<string, int> _scannedItems;

        public Checkout(IDictionary<string, decimal> priceList)
        {
            _priceList = priceList;
            _scannedItems = new Dictionary<string, int>();
        }

        public void ScanItem(string sku)
        {
            if (_scannedItems.TryGetValue(sku, out var currentQuantity))
            {
                _scannedItems[sku] = currentQuantity + 1;
            }
            else
            {
                _scannedItems[sku] = 1;
            }
        }

        public int CountScannedItems(string sku)
        {
            if (!_scannedItems.TryGetValue(sku, out var result))
            {
                return 0;
            }

            return result;
        }

        public decimal GetTotal()
        {
            decimal total = 0;

            foreach (var item in _scannedItems)
            {
                if (!_priceList.TryGetValue(item.Key, out var itemPrice))
                {
                    throw new Exception($"Invalid SKU '{item.Key}'.");
                }

                total += itemPrice * item.Value;
            }

            return total;
        }
    }
}
