using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web.Resource;
using StoreBPO.Data;
using StoreBPO.Models;

namespace StoreBPO.Controllers
{
    [ApiController]
    [Route("Stores")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class StoreController : Controller
    {
        private readonly StoreDbContext _context;

        public StoreController(StoreDbContext context)
        {
            _context = context;
        }

        // GET: Stores
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return _context.Stores != null ?
                        Ok(await _context.Stores.ToListAsync()) :
                        Ok("No Stores found!") ;
        }

        // GET: Stores/Details/5
        [HttpGet]
        [Route("details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Stores == null)
            {
                return NotFound("Store not found!");
            }

            var Store = await _context.Stores
                .FirstOrDefaultAsync(m => m.StoreID == id);
            if (Store == null)
            {
                return NotFound("Store not found!");
            }

            return Ok(Store);
        }

        // POST: Stores/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("StoreName")] Store Store)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Store);
                await _context.SaveChangesAsync();
                return CreatedAtAction(null,Store);
            }
            return BadRequest("Database is not ready, please try again in a few minutes");
        }

      
        // PUT: Stores/Edit/5
        [HttpPut]
        public async Task<IActionResult> Edit(int id, [Bind("StoreId,StoreName")] Store Store)
        {
            if (id != Store.StoreID)
            {
                return NotFound("Id does not match");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Store);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreExists(Store.StoreID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return BadRequest("Error in database.");
                    }
                }
                return Ok(Store);
            }
            return BadRequest("Database is not ready, please try again in a few minutes");
        }

        // POST: Stores/Delete/5
        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Stores == null)
            {
                return Problem("Entity set 'StoreDbContext.Stores'  is null.");
            }
            var Store = await _context.Stores.FindAsync(id);
            if (Store != null)
            {
                _context.Stores.Remove(Store);
            }
            else
            {
                return NotFound("Store not found!");
            }
            
            await _context.SaveChangesAsync();
            return Ok("Store deleted!");
        }

        private bool StoreExists(int id)
        {
          return (_context.Stores?.Any(e => e.StoreID == id)).GetValueOrDefault();
        }
    }
}
