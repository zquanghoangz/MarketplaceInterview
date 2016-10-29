using System.Collections.Generic;
using NUnit.Framework;
using Marketplace.Interview.Business.Basket;
using Marketplace.Interview.Business.Shipping;

namespace Marketplace.Interview.Tests
{
    [TestFixture]
    public class ShippingOptionTests
    {
        [Test]
        public void FlatRateShippingOptionTest()
        {
            var flatRateShippingOption = new FlatRateShipping { FlatRate = 1.5m };
            var shippingAmount = flatRateShippingOption.GetAmount(new LineItem(), new Basket());

            Assert.That(shippingAmount, Is.EqualTo(1.5m), "Flat rate shipping not correct.");
        }

        [Test]
        public void PerRegionShippingOptionTest()
        {
            var perRegionShippingOption = new PerRegionShipping()
            {
                PerRegionCosts = new[]
                                                                       {
                                                                           new RegionShippingCost()
                                                                               {
                                                                                   DestinationRegion =
                                                                                       RegionShippingCost.Regions.UK,
                                                                                   Amount = .75m
                                                                               },
                                                                           new RegionShippingCost()
                                                                               {
                                                                                   DestinationRegion =
                                                                                       RegionShippingCost.Regions.Europe,
                                                                                   Amount = 1.5m
                                                                               }
                                                                       },
            };

            var shippingAmount = perRegionShippingOption.GetAmount(new LineItem() { DeliveryRegion = RegionShippingCost.Regions.Europe }, new Basket());
            Assert.That(shippingAmount, Is.EqualTo(1.5m));

            shippingAmount = perRegionShippingOption.GetAmount(new LineItem() { DeliveryRegion = RegionShippingCost.Regions.UK }, new Basket());
            Assert.That(shippingAmount, Is.EqualTo(.75m));
        }

        [Test]
        public void SameRegionShippingOptionTest()
        {
            var sameRegionShippingOption = new SameRegionShipping
            {
                PerRegionCosts = new[]
                {
                    new RegionShippingCost()
                    {
                        DestinationRegion =
                            RegionShippingCost.Regions.UK,
                        Amount = .75m
                    },
                    new RegionShippingCost()
                    {
                        DestinationRegion =
                            RegionShippingCost.Regions.Europe,
                        Amount = 1.5m
                    }
                },
                ReduceRate = 0.5m
            };

            //2 items match condition
            var basket = new Basket
            {
                LineItems = new List<LineItem>
                {
                    new LineItem() {Id=1, DeliveryRegion = RegionShippingCost.Regions.Europe,SupplierId = 1, Shipping = sameRegionShippingOption},
                    new LineItem() {Id=2, DeliveryRegion = RegionShippingCost.Regions.Europe, SupplierId = 1, Shipping = sameRegionShippingOption}
                }
            };
            var shippingAmount = sameRegionShippingOption.GetAmount(basket.LineItems[0], basket);
            Assert.That(shippingAmount, Is.EqualTo(1.5m));
            shippingAmount = sameRegionShippingOption.GetAmount(basket.LineItems[1], basket);
            Assert.That(shippingAmount, Is.EqualTo(1m));


            //Only one item
            basket = new Basket
            {
                LineItems = new List<LineItem>
                {
                    new LineItem() {Id=1, DeliveryRegion = RegionShippingCost.Regions.Europe,SupplierId = 1, Shipping = sameRegionShippingOption},
                }
            };
            shippingAmount = sameRegionShippingOption.GetAmount(basket.LineItems[0], basket);
            Assert.That(shippingAmount, Is.EqualTo(1.5m));

            //Diffirent supplier
            basket = new Basket
            {
                LineItems = new List<LineItem>
                {
                    new LineItem() {Id=1, DeliveryRegion = RegionShippingCost.Regions.Europe,SupplierId = 1, Shipping = sameRegionShippingOption},
                    new LineItem() {Id=2, DeliveryRegion = RegionShippingCost.Regions.Europe,SupplierId = 2, Shipping = sameRegionShippingOption},
                }
            };
            shippingAmount = sameRegionShippingOption.GetAmount(basket.LineItems[0], basket);
            Assert.That(shippingAmount, Is.EqualTo(1.5m));
            shippingAmount = sameRegionShippingOption.GetAmount(basket.LineItems[1], basket);
            Assert.That(shippingAmount, Is.EqualTo(1.5m));

            //Diffirent shipping
            basket = new Basket
            {
                LineItems = new List<LineItem>
                {
                    new LineItem() {Id=1, DeliveryRegion = RegionShippingCost.Regions.Europe,SupplierId = 1, Shipping = sameRegionShippingOption},
                    new LineItem() {Id=2, DeliveryRegion = RegionShippingCost.Regions.Europe,SupplierId = 1, Shipping = new PerRegionShipping()},
                }
            };
            shippingAmount = sameRegionShippingOption.GetAmount(basket.LineItems[0], basket);
            Assert.That(shippingAmount, Is.EqualTo(1.5m));

            //Diffirent region
            basket = new Basket
            {
                LineItems = new List<LineItem>
                {
                    new LineItem() {Id=1, DeliveryRegion = RegionShippingCost.Regions.UK,SupplierId = 1, Shipping = sameRegionShippingOption},
                    new LineItem() {Id=2, DeliveryRegion = RegionShippingCost.Regions.Europe,SupplierId = 1, Shipping = sameRegionShippingOption},
                }
            };
            shippingAmount = sameRegionShippingOption.GetAmount(basket.LineItems[0], basket);
            Assert.That(shippingAmount, Is.EqualTo(0.75m));
            shippingAmount = sameRegionShippingOption.GetAmount(basket.LineItems[1], basket);
            Assert.That(shippingAmount, Is.EqualTo(1.5m));
        }

        [Test]
        public void BasketShippingTotalTest()
        {
            var perRegionShippingOption = new PerRegionShipping()
            {
                PerRegionCosts = new[]
                {
                    new RegionShippingCost()
                    {
                        DestinationRegion =
                            RegionShippingCost.Regions.UK,
                        Amount = .75m
                    },
                    new RegionShippingCost()
                    {
                        DestinationRegion =
                            RegionShippingCost.Regions.Europe,
                        Amount = 1.5m
                    }
                },
            };

            var flatRateShippingOption = new FlatRateShipping { FlatRate = 1.1m };

            var sameRegionShippingOption = new SameRegionShipping
            {
                PerRegionCosts = new[]
                {
                    new RegionShippingCost()
                    {
                        DestinationRegion =
                            RegionShippingCost.Regions.UK,
                        Amount = .75m
                    },
                    new RegionShippingCost()
                    {
                        DestinationRegion =
                            RegionShippingCost.Regions.Europe,
                        Amount = 1.5m
                    }
                },
                ReduceRate = 0.5m
            };

            var basket = new Basket()
            {
                LineItems = new List<LineItem>
                {
                    new LineItem()
                    {
                        Id = 1,
                        DeliveryRegion = RegionShippingCost.Regions.UK,
                        Shipping = perRegionShippingOption
                    },
                    new LineItem()
                    {
                        Id = 2,
                        DeliveryRegion = RegionShippingCost.Regions.Europe,
                        Shipping = perRegionShippingOption
                    },
                    new LineItem() {Id = 3, Shipping = flatRateShippingOption},
                    new LineItem
                    {
                        Id = 4,
                        DeliveryRegion = RegionShippingCost.Regions.Europe,
                        Shipping = sameRegionShippingOption
                    },
                    new LineItem
                    {
                        Id = 5,
                        DeliveryRegion = RegionShippingCost.Regions.Europe,
                        Shipping = sameRegionShippingOption
                    },
                    new LineItem
                    {
                        Id = 6,
                        DeliveryRegion = RegionShippingCost.Regions.UK,
                        Shipping = sameRegionShippingOption
                    },
                }
            };

            var calculator = new ShippingCalculator();

            decimal basketShipping = calculator.CalculateShipping(basket);

            Assert.That(basketShipping, Is.EqualTo(6.6m));
        }
    }
}