using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.API.Data;
using Sales.Shared.DTOs;
using Sales.Shared.Entities;

namespace Sales.API.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/temporalSales")]
    public class TemporalSalesController : ControllerBase
    {
        private readonly DataContext _context;

        public TemporalSalesController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Post(TemporalSaleDTO temporalSaleDTO)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.id == temporalSaleDTO.productId);
            if (product == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.Identity!.Name);
            if (user == null)
            {
                return NotFound();
            }

            var temporalSale = new TemporalSale
            {
                product = product,
                quantity = temporalSaleDTO.quantity,
                remarks = temporalSaleDTO.remarks,
                user = user
            };

            try
            {
                _context.Add(temporalSale);
                await _context.SaveChangesAsync();
                return Ok(temporalSaleDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _context.TemporalSales
                .Include(ts => ts.user!)
                .Include(ts => ts.product!)
                .ThenInclude(p => p.ProductCategories!)
                .ThenInclude(pc => pc.category)
                .Include(ts => ts.product!)
                .ThenInclude(p => p.ProductImages)
                .Where(x => x.user!.Email == User.Identity!.Name)
                .ToListAsync());
        }

        [HttpGet("count")]
        public async Task<ActionResult> GetCount()
        {
            return Ok(await _context.TemporalSales
                .Where(x => x.user!.Email == User.Identity!.Name)
                .SumAsync(x => x.quantity));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _context.TemporalSales
                .Include(ts => ts.user!)
                .Include(ts => ts.product!)
                .ThenInclude(p => p.ProductCategories!)
                .ThenInclude(pc => pc.category)
                .Include(ts => ts.product!)
                .ThenInclude(p => p.ProductImages)
                .FirstOrDefaultAsync(x => x.id == id));
        }

        [HttpPut]
        public async Task<ActionResult> Put(TemporalSaleDTO temporalSaleDTO)
        {
            var currentTemporalSale = await _context.TemporalSales.FirstOrDefaultAsync(x => x.id == temporalSaleDTO.id);
            if (currentTemporalSale == null)
            {
                return NotFound();
            }

            currentTemporalSale!.remarks = temporalSaleDTO.remarks;
            currentTemporalSale.quantity = temporalSaleDTO.quantity;

            _context.Update(currentTemporalSale);
            await _context.SaveChangesAsync();
            return Ok(temporalSaleDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var temporalSale = await _context.TemporalSales.FirstOrDefaultAsync(x => x.id == id);
            if (temporalSale == null)
            {
                return NotFound();
            }

            _context.Remove(temporalSale);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
