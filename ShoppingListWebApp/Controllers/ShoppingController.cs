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
    public async Task<IActionResult> Add()
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
                CategoryList = await _db.Categories.Select(c => new SelectListItem
                                                {
                                                    Text = c.Description,
                                                    Value = c.Id.ToString()
                                                }).ToListAsync()
            },
            ShopItems = await _db.ShopItems
                                .Select(si => new ShopItemViewModel
                                {
                                    Id = si.Id,
                                    Name = si.Name,
                                    Quantity = si.Quantity,
                                    UnitPrice = si.UnitPrice,
                                    CategoryName = si.Category.Description
                                })
                                .ToListAsync()
        };
        return View(shopItemsViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(ShopItemsViewModel newShopItemsViewModel)
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
            await _db.SaveChangesAsync();

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
                CategoryList = await _db.Categories.Select(c => new SelectListItem
                                                {
                                                    Text = c.Id.ToString(),
                                                    Value = c.Description
                                                }).ToListAsync()
            },
            ShopItems = await _db.ShopItems.Select(si => new ShopItemViewModel
            {
                Id = si.Id,
                Name = si.Name,
                Quantity = si.Quantity,
                UnitPrice = si.UnitPrice,
                CategoryName = si.Category.Description
            }).ToListAsync()
        };
        return View(shopItemsViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(long id)
    {
        ShopItem shopItemToEdit = await _db.ShopItems
                                        .SingleOrDefaultAsync(si => si.Id.Equals(id));

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
                CategoryList = await _db.Categories.Select(c => new SelectListItem
                                        {
                                            Text = c.Description,
                                            Value = c.Id.ToString()
                                        }).ToListAsync()   
            },
            ShopItems = await _db.ShopItems.Select(si => new ShopItemViewModel
            {
                Id = si.Id,
                Name = si.Name,
                Quantity = si.Quantity,
                UnitPrice = si.UnitPrice,
                CategoryName = si.Category.Description
            }).ToListAsync()
        };
        return View(shopItemsViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([FromRoute] long id, ShopItemsViewModel updatedShopItemsViewModel)
    {
        if (ModelState.IsValid)
        {
            ShopItem shopItemToEdit = await _db.ShopItems
                                        .SingleOrDefaultAsync(si => si.Id.Equals(id));

            shopItemToEdit.Name = updatedShopItemsViewModel.ShopItem.Name;
            shopItemToEdit.Quantity = updatedShopItemsViewModel.ShopItem.Quantity;
            shopItemToEdit.UnitPrice = updatedShopItemsViewModel.ShopItem.UnitPrice;
            shopItemToEdit.CategoryId = updatedShopItemsViewModel.ShopItem.SelectedCategoryId;
            
            await _db.SaveChangesAsync();

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
                CategoryList = await _db.Categories.Select(c => new SelectListItem
                                            {
                                                Text = c.Description,
                                                Value = c.Id.ToString()
                                            }).ToListAsync()
            },
            ShopItems = await _db.ShopItems.Select(si => new ShopItemViewModel
            {
                Id = si.Id,
                Name = si.Name,
                Quantity = si.Quantity,
                UnitPrice = si.UnitPrice,
                CategoryName = si.Category.Description
            }).ToListAsync()
        };
        return View(shopItemsViewModel);
    }

    public async Task<IActionResult> Delete(long id)
    {
        ShopItem deleteShopItem = await _db.ShopItems
                                     .SingleOrDefaultAsync(si => si.Id == id);
        if(deleteShopItem is ShopItem)
        {
            _db.ShopItems.Remove(deleteShopItem);
        }      

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Something went wrong: {e.Message}");
        }

        return RedirectToAction(nameof(ShoppingController.Add));
    }
}