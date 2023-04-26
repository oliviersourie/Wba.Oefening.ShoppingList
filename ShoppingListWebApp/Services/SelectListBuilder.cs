using ClassLib.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingListWebApp.ViewModels;

namespace ShoppingListWebApp.Services
{
    public class SelectListBuilder : ISelectListBuilder
    {
        private readonly ShoppingListContext _db;

        public SelectListBuilder(ShoppingListContext schoolContext)
        {
            _db = schoolContext;
        }

        public async Task<IEnumerable<SelectListItem>> GetCategoriesSelectListAsync()
        {
            return await _db.Categories.Select(si => new SelectListItem
            {
                Value = si.Id.ToString(),
                Text = si.Description
            })
                                        .ToListAsync();
        }
    }
}
