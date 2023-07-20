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
    [Route("StoreProductMapping")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class SPMappingController : Controller
    {
        private readonly StoreDbContext _context;

        public SPMappingController(StoreDbContext context)
        {
            _context = context;
        }

        // GET: StoreProductMapping
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return _context.Mappings != null ?
                        Ok(await _context.Mappings.ToListAsync()) :
                        Ok("No StoreProductMapping found!") ;
        }

        // GET: StoreProductMapping/Details/5
        [HttpGet]
        [Route("details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Mappings == null)
            {
                return NotFound("StoreProductMapping not found!");
            }

            var StoreProductMapping = await _context.Mappings
                .FirstOrDefaultAsync(m => m.MappingID == id);
            if (StoreProductMapping == null)
            {
                return NotFound("StoreProductMapping not found!");
            }

            return Ok(StoreProductMapping);
        }

        // POST: StoreProductMapping/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("StoreID,ProductID,Stock")] StoreProductMapping StoreProductMapping)
        {
            if (ModelState.IsValid)
            {
                _context.Add(StoreProductMapping);
                await _context.SaveChangesAsync();
                return CreatedAtAction(null,StoreProductMapping);
            }
            return BadRequest("Database is not ready, please try again in a few minutes");
        }

      
        // PUT: StoreProductMapping/Edit/5
        [HttpPut]
        public async Task<IActionResult> Edit(int id, [Bind("StoreID,ProductID,Stock")] StoreProductMapping StoreProductMapping)
        {
            if (id != StoreProductMapping.MappingID)
            {
                return NotFound("Id does not match");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(StoreProductMapping);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreExists(StoreProductMapping.MappingID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return BadRequest("Error in database.");
                    }
                }
                return Ok(StoreProductMapping);
            }
            return BadRequest("Database is not ready, please try again in a few minutes");
        }

        // POST: StoreProductMapping/Delete/5
        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Mappings == null)
            {
                return Problem("Entity set 'StoreDbContext.StoreProductMapping'  is null.");
            }
            var StoreProductMapping = await _context.Mappings.FindAsync(id);
            if (StoreProductMapping != null)
            {
                _context.Mappings.Remove(StoreProductMapping);
            }
            else
            {
                return NotFound("StoreProductMapping not found!");
            }
            
            await _context.SaveChangesAsync();
            return Ok("StoreProductMapping deleted!");
        }

        private bool StoreExists(int id)
        {
          return (_context.Mappings?.Any(e => e.MappingID == id)).GetValueOrDefault();
        }
    }
}
