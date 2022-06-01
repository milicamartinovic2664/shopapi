using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApi.CustomExceptions;
using ShopApi.Models;
using ShopApi.Models.DBContext;
using ShopApi.Services.Interfaces;

namespace ShopApi.Services
{
    public class JewleryItemService : IJewleryItemService
    {
        private readonly ApiContext _context;

        public JewleryItemService(ApiContext context)
        {
            _context = context;
        }

        public async Task<JewleryItem?> GetJewleryItemById(long id)
        {
            return await _context.JewleryItems.FindAsync(id);
        }

        public async Task<ActionResult<IEnumerable<JewleryItem>>> GetJewleryItems()
        {
            return await _context
                .JewleryItems
                .Include(item => item.Manufacturer)
                .ToListAsync();
        }

        public async Task CreateJewleryItem(JewleryItem jewleryItem)
        {
            if (jewleryItem.Manufacturer is not null)
            {
                var manufactorer = _context
                    .Manufacturers
                    .Find(jewleryItem.ManufacturerId);

                if (manufactorer is null)
                {
                    _context.Manufacturers.Add(jewleryItem.Manufacturer);
                }
            }

            _context.JewleryItems.Add(jewleryItem);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateJewleryItem(long id, JewleryItem jewleryItem)
        {
            var existingItem = await _context
                .JewleryItems
                .FindAsync(id);

            if (existingItem is null)
            {
                throw new EntryNotFoundException($"{nameof(JewleryItem)} id: {id} not found!");
            }

            var manufacturer = await _context
                .Manufacturers
                .FindAsync(jewleryItem.ManufacturerId);

            if (manufacturer is null)
            {
                throw new EntryNotFoundException($"{nameof(Manufacturer)} id: {jewleryItem.ManufacturerId} not found!");
            }

            jewleryItem.Manufacturer = manufacturer;
            jewleryItem.ManufacturerId = manufacturer.Id;

            _context.Entry(existingItem).State = EntityState.Detached;

            _context.Entry(jewleryItem).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteJewleryItem(long jewleryItemId)
        {
            var jewleryItem = await GetJewleryItemById(jewleryItemId);

            if (jewleryItem is null)
            {
                throw new EntryNotFoundException($"{nameof(JewleryItem)} id: {jewleryItemId} not found!");
            }

            _context.JewleryItems.Remove(jewleryItem);

            await _context.SaveChangesAsync();
        }

    }
}
