using Microsoft.AspNetCore.Mvc;
using ShopApi.Models;

namespace ShopApi.Services.Interfaces
{
    public interface IManufacturerService
    {
        Task<Manufacturer?> GetManufaturerById(long id);
        ActionResult<IEnumerable<Manufacturer>> GetManufaturers();
        Task CreateManufacturer(Manufacturer manufacturer);
        Task UpdateManufacturer(Manufacturer manufacturer);
        Task DeleteManufacturer(long manufacturerId);
    }
}
