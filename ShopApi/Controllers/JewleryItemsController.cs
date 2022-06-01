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
    [Route("api/jewlery-items")]
    [ApiController]
    public class JewleryItemsController : ControllerBase
    {
        private readonly ApiContext context;
        private readonly IJewleryItemService jewleryItemService;

        public JewleryItemsController(ApiContext context, IJewleryItemService jewleryItemService)
        {
            this.context = context;
            this.jewleryItemService = jewleryItemService;
        }

        // GET: api/JewleryItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JewleryItem>>> GetJewleryItems()
        {
            if (context.JewleryItems is null)
            {
                return NotFound();
            }

            return await jewleryItemService.GetJewleryItems();
        }

        // GET: api/JewleryItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JewleryItem>> GetJewleryItem(long id)
        {
            if (context.JewleryItems is null)
            {
                return NotFound();
            }

            var jewleryItem = await jewleryItemService.GetJewleryItemById(id);

            if (jewleryItem is null)
            {
                return NotFound();
            }

            return jewleryItem;
        }

        // PUT: api/JewleryItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJewleryItem(long id, JewleryItem jewleryItem)
        {
            if (id != jewleryItem.Id)
            {
                return BadRequest();
            }

            try
            {
                await jewleryItemService.UpdateJewleryItem(id, jewleryItem);

                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JewleryItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/JewleryItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JewleryItem>> PostJewleryItem(JewleryItem jewleryItem)
        {
            if (context.JewleryItems is null)
            {
                return Problem("Entity set 'JewleryItemContext.JewleryItems'  is null.");
            }

            await jewleryItemService.CreateJewleryItem(jewleryItem);
            
            return CreatedAtAction(nameof(GetJewleryItem), new { id = jewleryItem.Id }, jewleryItem);
        }

        // DELETE: api/JewleryItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJewleryItem(long id)
        {
            if (context.JewleryItems is null)
            {
                return NotFound();
            }

            await jewleryItemService.DeleteJewleryItem(id);

            return Ok();
        }

        private bool JewleryItemExists(long id)
        {
            return (context.JewleryItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
