using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingListWebApp.ViewModels;

namespace ShoppingListWebApp.Controllers;

public class ShoppingController : Controller
{
    private ICollection<ShopItem> _shoppingItems;

    public ShoppingController()
    {
        _shoppingItems = new List<ShopItem>();
    }

    [HttpGet]
    public IActionResult Add()
    {
        ShopItemsViewModel shopItemsViewModel = new ShopItemsViewModel
        {
            ShopItem = new ShopItemViewModel()
            {
                QuantityList = Enumerable.Range(1, 5)
                                         .ToList()
                                         .Select(q => new SelectListItem()
                                         {
                                             Text = q.ToString(),
                                             Value = q.ToString()
                                         })
            },
            ShopItems = new List<ShopItemViewModel>() //GetShoppingItems().ToList()
        };
        return View(shopItemsViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(ShopItemsViewModel newShopItemsViewModel)
    {
        /*Some custom error validation here...?*/

        if (ModelState.IsValid)
        {
            //_shoppingItems = GetShoppingItems().ToList();
            _shoppingItems.Add(new ShopItem
            {
                Name = newShopItemsViewModel.ShopItem.Name,
                Quantity = newShopItemsViewModel.ShopItem.Quantity
            });
            //StoreShopItem(shoppingItems);

            return RedirectToAction(controllerName: "Shopping", 
                                    actionName:  nameof(Add));
        }

        ShopItemsViewModel shopItemsViewModel = new ShopItemsViewModel
        {
            ShopItem = new ShopItemViewModel()
            {
                QuantityList = Enumerable.Range(1, 5)
                                         .ToList()
                                         .Select(q => new SelectListItem()
                                         {
                                             Text = q.ToString(),
                                             Value = q.ToString()
                                         })
            },
            ShopItems = new List<ShopItemViewModel>() //GetShoppingItems().ToList()
        };
        return View(shopItemsViewModel);
    }
}