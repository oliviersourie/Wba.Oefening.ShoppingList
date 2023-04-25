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
                                         }),
                CategoryList = _db.Categories.Select(c => new SelectListItem
                                                {
                                                    Text = c.Description,
                                                    Value = c.Id.ToString()
                                                })
            },
            ShopItems = _db.ShopItems
                            .Select(si => new ShopItemViewModel
                            {
                                Id = si.Id,
                                Name = si.Name,
                                Quantity = si.Quantity,
                                UnitPrice = si.UnitPrice,
                                CategoryName = si.Category.Description
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
                Quantity = newShopItemsViewModel.ShopItem.Quantity,
                UnitPrice = newShopItemsViewModel.ShopItem.UnitPrice,
                CategoryId = newShopItemsViewModel.ShopItem.SelectedCategoryId
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
                                         }),
                CategoryList = _db.Categories.Select(c => new SelectListItem
                                                {
                                                    Text = c.Id.ToString(),
                                                    Value = c.Description
                                                })
            },
            ShopItems = _db.ShopItems.Select(si => new ShopItemViewModel
            {
                Id = si.Id,
                Name = si.Name,
                Quantity = si.Quantity,
                UnitPrice = si.UnitPrice,
                CategoryName = _db.Categories
                                    .SingleOrDefault(c => c.Id.Equals(si.CategoryId))
                                    .Description
            })
        };
        return View(shopItemsViewModel);
    }

    [HttpGet]
    public IActionResult Edit(long id)
    {
        ShopItem shopItemToEdit = _db.ShopItems
                                     .SingleOrDefault(si => si.Id.Equals(id));

        ShopItemsViewModel shopItemsViewModel = new ShopItemsViewModel
        {
            ShopItem = new ShopItemViewModel()
            {
                Name = shopItemToEdit.Name,
                QuantityList = Enumerable.Range(1, 5)
                                         .ToList()
                                         .Select(q => new SelectListItem()
                                         {
                                             Text = q.ToString(),
                                             Value = q.ToString(),
                                         }),
                UnitPrice = shopItemToEdit.UnitPrice,
                CategoryList = _db.Categories.Select(c => new SelectListItem
                                        {
                                            Text = c.Description,
                                            Value = c.Id.ToString()
                                        })
            },
            ShopItems = _db.ShopItems.Select(si => new ShopItemViewModel
            {
                Id = si.Id,
                Name = si.Name,
                Quantity = si.Quantity,
                UnitPrice = si.UnitPrice,
                CategoryName = _db.Categories
                                    .SingleOrDefault(c => c.Id.Equals(si.CategoryId))
                                    .Description
            })
        };
        return View(shopItemsViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit([FromRoute] long id, ShopItemsViewModel updatedShopItemsViewModel)
    {
        if (ModelState.IsValid)
        {
            ShopItem shopItemToEdit = _db.ShopItems
                             .SingleOrDefault(si => si.Id.Equals(id));

            shopItemToEdit.Name = updatedShopItemsViewModel.ShopItem.Name;
            shopItemToEdit.Quantity = updatedShopItemsViewModel.ShopItem.Quantity;
            shopItemToEdit.UnitPrice = updatedShopItemsViewModel.ShopItem.UnitPrice;
            shopItemToEdit.CategoryId = updatedShopItemsViewModel.ShopItem.SelectedCategoryId;
            
            _db.SaveChanges();

            return RedirectToAction(controllerName: "Shopping",
                                    actionName: nameof(Add));
        }

        ShopItemsViewModel shopItemsViewModel = new ShopItemsViewModel
        {
            ShopItem = new ShopItemViewModel()
            {
                Name = updatedShopItemsViewModel.ShopItem.Name,
                QuantityList = Enumerable.Range(1, 5)
                                         .ToList()
                                         .Select(q => new SelectListItem()
                                         {
                                             Text = q.ToString(),
                                             Value = q.ToString()
                                         }),
                UnitPrice = updatedShopItemsViewModel.ShopItem.UnitPrice,
                CategoryList = _db.Categories.Select(c => new SelectListItem
                                            {
                                                Text = c.Description,
                                                Value = c.Id.ToString()
                                            })
            },
            ShopItems = _db.ShopItems.Select(si => new ShopItemViewModel
            {
                Id = si.Id,
                Name = si.Name,
                Quantity = si.Quantity,
                UnitPrice = si.UnitPrice,
                CategoryName = _db.Categories
                                    .SingleOrDefault(c => c.Id.Equals(si.CategoryId))
                                    .Description
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