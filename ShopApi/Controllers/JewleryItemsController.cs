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
        private readonly IJewleryItemService jewleryItemService;
        private readonly ILogger logger;

        public JewleryItemsController(IJewleryItemService jewleryItemService, ILogger logger)
        {
            this.jewleryItemService = jewleryItemService;
            this.logger = logger;
        }

        // GET: api/JewleryItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JewleryItem>>> GetJewleryItems()
        {
            var items = await jewleryItemService.GetJewleryItems();

            if (items.Count() == 0)
            {
                return NotFound();
            }

            return Ok(items);
        }

        // GET: api/JewleryItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JewleryItem>> GetJewleryItem(long id)
        {
            var jewleryItem = await jewleryItemService.GetJewleryItemById(id);

            if (jewleryItem is null)
            {
                return NotFound();
            }

            return Ok(jewleryItem);
        }

        // PUT: api/JewleryItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<JewleryItem>> PutJewleryItem(long id, JewleryItem jewleryItem)
        {
            if (id != jewleryItem.Id)
            {
                return BadRequest();
            }

            try
            {
                var item = await jewleryItemService.UpdateJewleryItem(id, jewleryItem);

                return Ok(item);
            }
            catch (DbUpdateConcurrencyException exc)
            {
                logger.LogError($"{nameof(JewleryItemsController)} :: exception occured {exc}");
                throw;
            }
        }

        // POST: api/JewleryItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JewleryItem>> PostJewleryItem(JewleryItem jewleryItem)
        {
            await jewleryItemService.CreateJewleryItem(jewleryItem);
            
            return CreatedAtAction(nameof(GetJewleryItem), new { id = jewleryItem.Id }, jewleryItem);
        }

        // DELETE: api/JewleryItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<JewleryItem>> DeleteJewleryItem(long id)
        {
            var item = await jewleryItemService.GetJewleryItemById(id);
            
            if (item is null)
            {
                return NotFound();
            }

            await jewleryItemService.DeleteJewleryItem(id);

            return Ok(item);
        }
    }
}
