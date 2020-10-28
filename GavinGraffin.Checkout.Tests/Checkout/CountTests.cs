using FluentAssertions;
using Xunit;

namespace GavinGraffin.Checkout.Tests.Checkout
{
    public class CountTests
    {

        [Fact]
        public void NewCheckoutHasNoItems()
        {
            var checkout = new GavinGraffin.Checkout.Checkout();

            checkout.CountScannedItems("B15")
                .Should().Be(0);
        }

        [Fact]
        public void WhenAnItemIsScannedTheCountIsIncremented()
        {
            var checkout = new GavinGraffin.Checkout.Checkout();

            checkout.ScanItem("B15");

            checkout.CountScannedItems("B15")
                .Should().Be(1);
        }

        [Fact]
        public void WhenMultipleItemsAreScannedMultipleCountsAreIncremented()
        {
            var checkout = new GavinGraffin.Checkout.Checkout();

            checkout.ScanItem("B15");
            checkout.ScanItem("A99");

            checkout.CountScannedItems("B15")
                .Should().Be(1);

            checkout.CountScannedItems("A99")
                .Should().Be(1);
        }

        [Fact]
        public void WhenTheSameSkuIsScannedMultipleTimesTheCountIsIncrementedForEach()
        {
            var checkout = new GavinGraffin.Checkout.Checkout();

            checkout.ScanItem("B15");
            checkout.ScanItem("B15");

            checkout.CountScannedItems("B15")
                .Should().Be(2);
        }
    }
}
