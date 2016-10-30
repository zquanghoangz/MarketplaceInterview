using System;
using Marketplace.Interview.Business.Basket;

namespace Marketplace.Interview.Business
{
    public class BasketUnitOfWork : BasketOperationBase, IBasketUnitOfWork
    {
        public IAddToBasketCommand AddToBasketCommand { get; set; }
        public IRemoveFromBasketCommand RemoveFromBasketCommand { get; set; }
        public IGetBasketQuery GetBasketQuery { get; set; }
        public Basket.Basket Basket { get; set; }

        public BasketUnitOfWork()
        {
            Basket = GetBasket();
            AddToBasketCommand = new AddToBasketCommand { Basket = Basket };
            RemoveFromBasketCommand = new RemoveFromBasketCommand { Basket = Basket };
            GetBasketQuery = new GetBasketQuery { Basket = Basket };
        }

        public BasketUnitOfWork(IAddToBasketCommand addToBasketCommand,
            IRemoveFromBasketCommand removeFromBasketCommand,
            IGetBasketQuery getBasketQuery)
        {
            Basket = GetBasket();
            AddToBasketCommand = addToBasketCommand;
            RemoveFromBasketCommand = removeFromBasketCommand;
            GetBasketQuery = getBasketQuery;

            AddToBasketCommand.Basket = Basket;
            RemoveFromBasketCommand.Basket = Basket;
            GetBasketQuery.Basket = Basket;
        }

        public void SaveChanges()
        {
            SaveBasket(Basket);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}