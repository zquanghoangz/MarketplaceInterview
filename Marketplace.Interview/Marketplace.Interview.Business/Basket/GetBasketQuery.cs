using System.Web;
using System.Xml.Linq;

namespace Marketplace.Interview.Business.Basket
{
    public class GetBasketQuery : IGetBasketQuery
    {
        private readonly IShippingCalculator _shippingCalculator;

        public GetBasketQuery()
        {
            _shippingCalculator = new ShippingCalculator();
        }

        public Basket Invoke(BasketRequest request)
        {
            //var basket = GetBasket();
            var basket = Basket;
            basket.Shipping = _shippingCalculator.CalculateShipping(basket);

            return basket;
        }

        public Basket Basket { get; set; }
    }

    public class BasketRequest { }
}