using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApi.Models;
using ShopApi.Models.DBContext;
using ShopApi.Services.Interfaces;

namespace ShopApi.Controllers
{
    [Route("api/Manufacturers")]
    [ApiController]
    public class ManufacturersController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IManufacturerService _service;

        public ManufacturersController(ApiContext context, IManufacturerService service)
        {
            _context = context;
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Manufacturer>> GetManufacturers()
        {
            if (_context.Manufacturers == null)
            {
                return NotFound();
            }

            return _service.GetManufaturers();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Manufacturer>> GetManufacturer(long id)
        {
            if (_context.Manufacturers is null)
            {
                return NotFound();
            }

            var manufacturer = await _service.GetManufaturerById(id);

            if (manufacturer == null)
            {
                return NotFound();
            }

            return manufacturer;
        }

        // PUT: api/Manufacturers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/update")]
        public async Task<IActionResult> PutManufacturer(long id, Manufacturer manufacturer)
        {
            if (id != manufacturer.Id)
            {
                return BadRequest();
            }

            try
            {
                await _service.UpdateManufacturer(manufacturer);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManufacturerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Manufacturers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create")]
        public async Task<ActionResult<Manufacturer>> PostManufacturer(Manufacturer manufacturer)
        {
            if (_context.Manufacturers == null)
            {
                return Problem("Entity set 'ManufacturerContext.Manufacturers'  is null.");
            }

            await _service.CreateManufacturer(manufacturer);

            return CreatedAtAction("GetManufacturer", new { id = manufacturer.Id }, manufacturer);
        }

        // DELETE: api/Manufacturers/5
        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> DeleteManufacturer(long id)
        {
            if (_context.Manufacturers == null)
            {
                return NotFound();
            }

            await _service.DeleteManufacturer(id);

            return NoContent();
        }

        private bool ManufacturerExists(long id)
        {
            return (_context.Manufacturers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
