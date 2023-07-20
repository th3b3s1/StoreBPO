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
    [Route("products")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class ProductsController : Controller
    {
        private readonly StoreDbContext _context;

        public ProductsController(StoreDbContext context)
        {
            _context = context;
        }

        // GET: Products
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return _context.Products != null ?
                        Ok(await _context.Products.ToListAsync()) :
                        Ok("No products found!") ;
        }

        // GET: Products/Details/5
        [HttpGet]
        [Route("details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound("Product not found!");
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound("Product not found!");
            }

            return Ok(product);
        }

        // POST: Products/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProductName")] VMProduct product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return CreatedAtAction(null,product);
            }
            return BadRequest("Database is not ready, please try again in a few minutes");
        }

      
        // PUT: Products/Edit/5
        [HttpPut]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound("Id does not match");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return BadRequest("Error in database.");
                    }
                }
                return Ok(product);
            }
            return BadRequest("Database is not ready, please try again in a few minutes");
        }

        // POST: Products/Delete/5
        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'StoreDbContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            else
            {
                return NotFound("Product not found!");
            }
            
            await _context.SaveChangesAsync();
            return Ok("Product deleted!");
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
