using System.Collections.Generic;

namespace GavinGraffin.Checkout
{
    public class Checkout
    {
        private readonly IDictionary<string, int> _scannedItems;

        public Checkout()
        {
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
    }
}
