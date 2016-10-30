using Marketplace.Interview.Business.Basket;
using Marketplace.Interview.Business.Core;

namespace Marketplace.Interview.Business
{
    public interface IRemoveFromBasketCommand : ICommand<int, bool>
    {
        Basket.Basket Basket { get; set; }
    }

    public interface IAddToBasketCommand : ICommand<AddToBasketRequest, AddToBasketResponse>
    {
        Basket.Basket Basket { get; set; }
    }
}