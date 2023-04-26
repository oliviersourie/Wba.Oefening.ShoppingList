using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShoppingListWebApp.Services
{
    public interface ISelectListBuilder
    {
        Task<IEnumerable<SelectListItem>> GetCategoriesSelectListAsync();
    }
}