using Microsoft.AspNetCore.Mvc;
using ShopApi.Models;

namespace ShopApi.Services.Interfaces
{
    public interface IJewleryItemService
    {
        Task<IEnumerable<JewleryItem>> GetJewleryItems();
        Task<JewleryItem?> GetJewleryItemById(long id);
        Task<JewleryItem> UpdateJewleryItem(long id, JewleryItem jewleryItem);
        Task CreateJewleryItem(JewleryItem jewleryItem);
        Task DeleteJewleryItem(long jewleryItemId);
        bool JewleryItemExists(long id);
    }
}
