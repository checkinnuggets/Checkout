namespace GavinGraffin.Checkout.Models
{
    public class SpecialOffer
    {
        public int Quantity { get; }
        public decimal Price { get; }

        public SpecialOffer(int quantity, decimal price)
        {
            Quantity = quantity;
            Price = price;
        }
    }
}
