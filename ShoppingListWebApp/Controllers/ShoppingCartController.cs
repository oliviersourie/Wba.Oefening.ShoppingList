using Microsoft.AspNetCore.Mvc;
using ShoppingListWebApp.ViewModels;
using System.Text.Json;

namespace ShoppingListWebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly string _cart = "ShoppingCart";
        private ICollection<ShopItem> _allCartItems;

        public CartController()
        {
            _allCartItems = new List<ShopItem>();
        }

        public ViewResult Index()
        {
            GetAllCartItems();
            CartItemsViewModel cartItemsVM= new CartItemsViewModel
            {
                CartItems = _allCartItems.Select(si => new CartItemViewModel
                                                    {
                                                        Id = si.Id,
                                                        Name = si.Name,
                                                        Quantity = si.Quantity,
                                                        UnitPrice = si.UnitPrice,   
                                                    }).ToList()
                };
            return View(cartItemsVM);
        }

        public IActionResult DeleteItem(long id)
        {
            GetAllCartItems();
            _allCartItems.Remove(_allCartItems.SingleOrDefault(ci => ci.Id.Equals(id)));

            HttpContext.Session.SetString(_cart,
                    JsonSerializer.Serialize(_allCartItems));

            return RedirectToAction(nameof(Index));
        }


        private void GetAllCartItems()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString(_cart)))
            {
                _allCartItems = JsonSerializer.Deserialize<ICollection<ShopItem>>
                                        (HttpContext.Session.GetString(_cart));
            }
        }

    }
}
