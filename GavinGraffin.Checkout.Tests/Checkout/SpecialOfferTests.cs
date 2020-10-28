using System.Collections.Generic;
using FluentAssertions;
using GavinGraffin.Checkout.Models;
using Xunit;

namespace GavinGraffin.Checkout.Tests.Checkout
{
    public class SpecialOfferTests
    {
        private static readonly IDictionary<string, decimal> StandardPrices
            = new Dictionary<string, decimal>
            {
                {"A99", 0.50m},
                {"B15", 0.30m},
                {"C40", 0.60m}
            };

        private static readonly IDictionary<string, SpecialOffer> SpecialOffers
            = new Dictionary<string, SpecialOffer>
            {
                {"A99", new SpecialOffer(3, 1.30m)},
                {"B15", new SpecialOffer(2, 0.45m)}
            };


        private readonly PriceList _priceList = new PriceList(StandardPrices, SpecialOffers);


        [Fact]
        public void WhenTheSpecialOfferThresholdIsNotMetItemsAreChargedAtFullPrice()
        {
            var checkout = new GavinGraffin.Checkout.Checkout(_priceList);

            checkout.ScanItem("A99");

            checkout.GetTotal()
                .Should().Be(0.50m);
        }

        [Fact]
        public void WhenTheSpecialOfferThresholdIsMetItemsAreDiscounted()
        {
            var checkout = new GavinGraffin.Checkout.Checkout(_priceList);

            checkout.ScanItem("A99");
            checkout.ScanItem("A99");
            checkout.ScanItem("A99");

            checkout.GetTotal()
                .Should().Be(1.30m);
        }

        [Fact]
        public void WhenTheSpecialOfferThresholdIsExceededSubsequentItemsAreNotDiscounted()
        {
            var checkout = new GavinGraffin.Checkout.Checkout(_priceList);

            // 3 to be discounted
            checkout.ScanItem("A99");
            checkout.ScanItem("A99");
            checkout.ScanItem("A99");

            // further one full price
            checkout.ScanItem("A99");

            checkout.GetTotal()
                .Should().Be(1.80m);
        }
    }
}
