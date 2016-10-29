using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marketplace.Interview.Business.Basket;

namespace Marketplace.Interview.Business.Shipping
{
    public class SameRegionShipping : ShippingBase
    {
        public IEnumerable<RegionShippingCost> PerRegionCosts { get; set; }

        public override string GetDescription(LineItem lineItem, Basket.Basket basket)
        {
            return $"Shipping to {lineItem.DeliveryRegion}";
        }

        /// <summary>
        /// Get amount for the same region shipping
        /// If there is at least one other item in the basket with the same Shipping Option and the same Supplier and Region, 'ReduceRate' should be deducted from the shipping.
        /// </summary>
        /// <param name="lineItem">current line item to get amount</param>
        /// <param name="basket">store all line items</param>
        /// <returns>the amount of current line item</returns>
        public override decimal GetAmount(LineItem lineItem, Basket.Basket basket)
        {
            //At least one other item in the basket with the same Shipping Option and the same Supplier and Region
            var firstItem = basket.LineItems.First(item => item.Shipping is SameRegionShipping
                                                        && item.SupplierId == lineItem.SupplierId
                                                        && item.DeliveryRegion == lineItem.DeliveryRegion);
            return
                (from c in PerRegionCosts
                 where c.DestinationRegion == lineItem.DeliveryRegion
                 select c.Amount).Single() - (firstItem.Id != lineItem.Id ? ReduceRate : 0);

            //Todo: keep this code for the confirm business logic
            //var anySameShipping = basket.LineItems
            //   .Any(item => item.Id != lineItem.Id
            //                && item.Shipping is SameRegionShipping
            //                && item.SupplierId == lineItem.SupplierId
            //                && item.DeliveryRegion == lineItem.DeliveryRegion);
            //return
            //    (from c in PerRegionCosts
            //     where c.DestinationRegion == lineItem.DeliveryRegion
            //     select c.Amount).Single() - (anySameShipping ? ReduceRate : 0);
        }

        public decimal ReduceRate { get; set; }
    }
}
