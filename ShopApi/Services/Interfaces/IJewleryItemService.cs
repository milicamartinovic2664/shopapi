using Microsoft.AspNetCore.Mvc;
using ShopApi.Models;

namespace ShopApi.Services.Interfaces
{
    public interface IJewleryItemService
    {
        Task<ActionResult<IEnumerable<JewleryItem>>> GetJewleryItems();
        Task<JewleryItem?> GetJewleryItemById(long id);
        Task UpdateJewleryItem(long id, JewleryItem jewleryItem);
        Task CreateJewleryItem(JewleryItem jewleryItem);
        Task DeleteJewleryItem(long jewleryItemId);
    }
}
