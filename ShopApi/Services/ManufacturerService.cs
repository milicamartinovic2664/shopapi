using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApi.CustomExceptions;
using ShopApi.Models;
using ShopApi.Models.DBContext;
using ShopApi.Services.Interfaces;

namespace ShopApi.Services
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly ApiContext _context;

        public ManufacturerService(ApiContext context)
        {
            _context = context;
        }

        public async Task<Manufacturer?> GetManufaturerById(long id)
            => await _context.Manufacturers.FindAsync(id);
        
        public ActionResult<IEnumerable<Manufacturer>> GetManufaturers()
            => _context.Manufacturers.ToList();
        

        public async Task CreateManufacturer(Manufacturer manufacturer)
        {
            _context.Manufacturers.Add(manufacturer);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateManufacturer(Manufacturer manufacturer)
        {
            _context.Entry(manufacturer).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteManufacturer(long manufacturerId)
        {
            var manufacturer = await GetManufaturerById(manufacturerId);

            if (manufacturer is null)
            {
                throw new EntryNotFoundException($"{nameof(Manufacturer)} id: {manufacturerId} not found!");
            }

            _context.Manufacturers.Remove(manufacturer);

            await _context.SaveChangesAsync();
        }

    }
}
