using System.Collections.Generic;
using GavinGraffin.Checkout.Models;

namespace GavinGraffin.Checkout
{
    public class Checkout
    {
        private readonly IPriceList _priceList;

        private readonly IDictionary<string, int> _scannedItems;

        public Checkout(IDictionary<string, decimal> priceList)
            : this(new PriceList(priceList, new Dictionary<string, SpecialOffer>()))
        {
            // this may go - at the moment just propping up the existing tests
        }

        public Checkout(IPriceList priceList)
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
                var skuPrice = _priceList.GetPriceForSku(item.Key);

                var specialOffer = _priceList.GetSpecialOfferForSku(item.Key);

                if (specialOffer == null)
                {
                    total += skuPrice * item.Value;
                }
                else
                {
                    var fullPriceItems = item.Value % specialOffer.Quantity;
                    total += (fullPriceItems * skuPrice);

                    var discountedSets = item.Value / specialOffer.Quantity;
                    total += (discountedSets * specialOffer.Price);
                }
            }

            return total;
        }
    }
}
