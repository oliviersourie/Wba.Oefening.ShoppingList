using ClassLib.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingListWebApp.ViewModels;

namespace ShoppingListWebApp.Controllers;

public class ShoppingController : Controller
{
    private readonly ShoppingListContext _db;

    public ShoppingController(ShoppingListContext shoppingListContext)
    {
        _db = shoppingListContext;
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
            ShopItems = _db.ShopItems.Select(si => new ShopItemViewModel
            {
                Id = si.Id,
                Name = si.Name,
                Quantity = si.Quantity
            })
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
            _db.ShopItems.Add(new ShopItem
            {
                Name = newShopItemsViewModel.ShopItem.Name,
                Quantity = newShopItemsViewModel.ShopItem.Quantity
            });
            _db.SaveChanges();

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
            ShopItems = _db.ShopItems.Select(si => new ShopItemViewModel
            {
                Id = si.Id,
                Name = si.Name,
                Quantity = si.Quantity
            })
        };
        return View(shopItemsViewModel);
    }

    public IActionResult Delete(long id)
    {
        ShopItem deleteShopItem = _db.ShopItems
                                     .SingleOrDefault(si => si.Id == id);
        if(deleteShopItem is ShopItem)
        {
            _db.ShopItems.Remove(deleteShopItem);
        }      

        try
        {
            _db.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Something went wrong: {e.Message}");
        }

        return RedirectToAction(nameof(ShoppingController.Add));
    }
}