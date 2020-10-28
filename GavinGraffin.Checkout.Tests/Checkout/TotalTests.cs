using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace GavinGraffin.Checkout.Tests.Checkout
{
    public class TotalTests
    {
        private readonly IDictionary<string, decimal> _priceList
            = new Dictionary<string, decimal>
        {
            {"A99", 0.50m},
            {"B15", 0.30m},
            {"C40", 0.60m}
        };


        [Fact]
        public void WhenNoItemsHaveBeenScannedTheTotalIsZero()
        {
            var checkout = new GavinGraffin.Checkout.Checkout(_priceList);
            checkout.GetTotal()
                .Should().Be(0);
        }


        [Fact]
        public void WhenAnItemIsScannedThisIsReflectedInTheTotal()
        {
            var checkout = new GavinGraffin.Checkout.Checkout(_priceList);

            checkout.ScanItem("B15");

            checkout.GetTotal()
                .Should().Be(0.30m);
        }

        [Fact]
        public void WhenMultipleItemsAreScannedThisIsReflectedInTheTotal()
        {
            var checkout = new GavinGraffin.Checkout.Checkout(_priceList);

            checkout.ScanItem("B15");
            checkout.ScanItem("A99");

            checkout.GetTotal()
                .Should().Be(0.80m);
        }

        [Fact]
        public void WhenTheSameSkuIsScannedMultipleTimesThisIsReflectedInTheTotal()
        {
            var checkout = new GavinGraffin.Checkout.Checkout(_priceList);

            checkout.ScanItem("B15");
            checkout.ScanItem("B15");

            checkout.GetTotal()
                .Should().Be(0.60m);
        }

        // (GG) gap here with scanning an invalid item.  would be nicer to know that before calling GetTotal()
    }
}
