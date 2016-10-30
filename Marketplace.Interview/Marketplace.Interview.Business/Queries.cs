using Marketplace.Interview.Business.Basket;
using Marketplace.Interview.Business.Core;
using Marketplace.Interview.Business.Shipping;

namespace Marketplace.Interview.Business
{
    public interface IGetBasketQuery : IQuery<BasketRequest, Basket.Basket>
    {
        Basket.Basket Basket { get; set; }
    }
    public interface IGetShippingOptionsQuery : IQuery<GetShippingOptionsRequest, GetShippingOptionsResponse> { }
}