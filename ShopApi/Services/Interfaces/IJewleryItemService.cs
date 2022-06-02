using Microsoft.AspNetCore.Mvc;
using ShopApi.Models;

namespace ShopApi.Services.Interfaces
{
    public interface IJewleryItemService
    {
        /// <summary>
        /// Returns all jewlery items from DB
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<JewleryItem>> GetJewleryItems();
        /// <summary>
        /// Returns one jewlery item by specified id if exists, if not returns null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<JewleryItem?> GetJewleryItemById(long id);
        /// <summary>
        /// Updates existing jewlery item specified by id with properties from second parameter
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jewleryItem"></param>
        /// <returns></returns>
        /// <exception cref="T:ShopApi.CustomExceptions.EntryNotFoundException">JewleryItem with Id: {jewleryItemId} not found!; </exception>
        Task<JewleryItem> UpdateJewleryItem(long id, JewleryItem jewleryItem);
        /// <summary>
        /// Creates a new instance of jewlery item
        /// </summary>
        /// <param name="jewleryItem"></param>
        /// <returns></returns>
        Task CreateJewleryItem(JewleryItem jewleryItem);
        /// <summary>
        /// Deletes jewlery item with specified id if exists, otherwise throws exception
        /// </summary>
        /// <param name="jewleryItemId"></param>
        /// <returns></returns>
        /// <exception cref="T:ShopApi.CustomExceptions.EntryNotFoundException">JewleryItem with Id: {jewleryItemId} not found!; </exception>
        Task DeleteJewleryItem(long jewleryItemId);
        /// <summary>
        /// Checks if exists jewlery item with specified id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool JewleryItemExists(long id);
    }
}
