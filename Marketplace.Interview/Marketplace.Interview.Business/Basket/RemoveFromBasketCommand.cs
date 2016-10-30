namespace Marketplace.Interview.Business.Basket
{
    public class RemoveFromBasketCommand : IRemoveFromBasketCommand
    {
        public bool Invoke(int id)
        {
            //var basket = GetBasket();
            var basket = Basket;

            basket.LineItems.RemoveWhere(li => li.Id == id);

            //SaveBasket(basket);

            return true;
        }

        public Basket Basket { get; set; }
    }
}