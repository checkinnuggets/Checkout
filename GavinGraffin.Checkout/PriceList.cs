using System;
using System.Collections.Generic;
using GavinGraffin.Checkout.Models;

namespace GavinGraffin.Checkout
{
    public interface IPriceList
    {
        decimal GetPriceForSku(string sku);
        SpecialOffer GetSpecialOfferForSku(string sku);
    }

    public class PriceList : IPriceList
    {
        private readonly IDictionary<string, decimal> _standardPrices;
        private readonly IDictionary<string, SpecialOffer> _specialOffers;

        public PriceList(IDictionary<string, decimal> standardPrices, IDictionary<string, SpecialOffer> specialOffers)
        {
            _standardPrices = standardPrices;
            _specialOffers = specialOffers;
        }

        public decimal GetPriceForSku(string sku)
        {
            if (!_standardPrices.TryGetValue(sku, out var price))
            {
                throw new Exception($"Invalid SKU '{sku}'.");
            }

            return price;
        }

        public SpecialOffer GetSpecialOfferForSku(string sku)
        {
            if (!_specialOffers.TryGetValue(sku, out var specialOffer))
            {
                return null;
            }

            return specialOffer;
        }
    }
}