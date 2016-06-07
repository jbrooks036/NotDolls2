using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
// using Microsoft.Data.Entity;
using NotDolls2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;



// SB:  this code looks very similar to a node.js controller!!

namespace NotDolls2.Controllers
{
    [Route("api/[controller]")]
    public class InventoryController : Controller
    {
        private NotDolls2Context _context;

        public InventoryController(NotDolls2Context context)
        {
            _context = context;
        }

        // GET: api/inventory
        [HttpGet]
        public IActionResult Get()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IQueryable<object> inventory = from i in _context.Inventory
                                           select i;

            if (inventory == null)
            {
                return NotFound();
            }

            return Ok(inventory);
        }

        // GET api/inventory/5
        [HttpGet("{id}", Name = "GetInventory")]  // need to add attribute (name?)
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Inventory inventory = _context.Inventory.Single(m => m.InventoryId == id);

            if (inventory == null)
            {
                return NotFound();
            }

            return Ok(inventory);
        }

        // POST api/inventory
        [HttpPost]
        public IActionResult Post([FromBody]Inventory inventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Inventory.Add(inventory);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (InventoryExists(inventory.InventoryId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetInventory", new { id = inventory.InventoryId }, inventory);

        }

        // PUT api/inventory/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/inventory/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private bool InventoryExists(int id)
        {
            return _context.Inventory.Count(e => e.InventoryId == id) > 0;
        }

    }
}
