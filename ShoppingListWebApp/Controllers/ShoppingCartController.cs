using Microsoft.AspNetCore.Mvc;
using ShoppingListWebApp.ViewModels;
using System.Text.Json;

namespace ShoppingListWebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly string _cart = "ShoppingCart";

        public ViewResult Index()
        {
            ICollection<ShopItem> allCartItems = new List<ShopItem>();

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString(_cart)))
            {
                allCartItems = JsonSerializer.Deserialize<ICollection<ShopItem>>
                                        (HttpContext.Session.GetString(_cart));
            }

            CartItemsViewModel cartItemsVM= new CartItemsViewModel
            {
                CartItems = allCartItems.Select(si => new CartItemViewModel
                                                    {
                                                        Id = si.Id,
                                                        Name = si.Name,
                                                        Quantity = si.Quantity,
                                                        UnitPrice = si.UnitPrice,   
                                                    }).ToList()
                };
            return View(cartItemsVM);
        }


        public IActionResult DeleteItem()
        {
            return View();
        }

    }
}
