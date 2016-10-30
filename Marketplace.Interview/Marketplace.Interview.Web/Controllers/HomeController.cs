using System.Web.Mvc;
using Marketplace.Interview.Business;
using Marketplace.Interview.Business.Basket;
using Marketplace.Interview.Business.Shipping;
using Marketplace.Interview.Web.Views.Home;

namespace Marketplace.Interview.Web.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        //private readonly IGetBasketQuery _basketLoader;
        //private readonly IAddToBasketCommand _addToBasket;
        private readonly IGetShippingOptionsQuery _getShippingOptions;
        //private readonly IRemoveFromBasketCommand _removeFromBasketCommand;
        private readonly IBasketUnitOfWork _basketUnitOfWork;

        public HomeController(IBasketUnitOfWork basketUnitOfWork,
            IGetShippingOptionsQuery getShippingOptions)
        {
            _basketUnitOfWork = basketUnitOfWork;
            _getShippingOptions = getShippingOptions;

        }

        public ActionResult Index()
        {
            var basket = _basketUnitOfWork.GetBasketQuery.Invoke(new BasketRequest());
            var shippingOptions = _getShippingOptions.Invoke(new GetShippingOptionsRequest()).ShippingOptions;

            var viewModel = new InterviewViewModel { Basket = basket, ShippingOptions = shippingOptions };

            return View(viewModel);
        }

        public ActionResult RemoveItem(int id)
        {
            _basketUnitOfWork.RemoveFromBasketCommand.Invoke(id);
            _basketUnitOfWork.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult AddItem(LineItemViewModel lineItemViewModel)
        {
            var shippingOptions = _getShippingOptions.Invoke(new GetShippingOptionsRequest()).ShippingOptions;

            var lineItem = new LineItem()
            {
                Amount = lineItemViewModel.Amount,
                ProductId = lineItemViewModel.ProductId,
                Shipping = shippingOptions[lineItemViewModel.ShippingOption],
                SupplierId = lineItemViewModel.SupplierId,
                DeliveryRegion = lineItemViewModel.DeliveryRegion,
            };

            _basketUnitOfWork.AddToBasketCommand.Invoke(new AddToBasketRequest() { LineItem = lineItem });
            _basketUnitOfWork.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
