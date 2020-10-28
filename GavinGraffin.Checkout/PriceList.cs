using System;
using System.Collections.Generic;

namespace GavinGraffin.Checkout
{
    public interface IPriceList
    {
        decimal GetPriceForSku(string sku);
    }

    public class PriceList : IPriceList
    {
        private readonly IDictionary<string, decimal> _standardPrices;

        public PriceList(IDictionary<string, decimal> standardPrices)
        {
            _standardPrices = standardPrices;
        }

        public decimal GetPriceForSku(string sku)
        {
            if (!_standardPrices.TryGetValue(sku, out var price))
            {
                throw new Exception($"Invalid SKU '{sku}'.");
            }

            return price;
        }
    }
}