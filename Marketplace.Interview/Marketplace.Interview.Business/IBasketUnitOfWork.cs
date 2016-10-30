using System;

namespace Marketplace.Interview.Business
{
    public interface IBasketUnitOfWork : IDisposable
    {
        IAddToBasketCommand AddToBasketCommand { get; set; }
        IRemoveFromBasketCommand RemoveFromBasketCommand { get; set; }
        IGetBasketQuery GetBasketQuery { get; set; }
        Basket.Basket Basket { get; set; }
        void SaveChanges();
    }
}