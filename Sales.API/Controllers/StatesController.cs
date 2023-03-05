using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.API.Data;
using Sales.API.Helpers;
using Sales.Shared.DTOs;
using Sales.Shared.Entities;

namespace Sales.API.Controllers
{
    [ApiController]
    [Route("/api/states")]
    public class StatesController : ControllerBase
    {
        private readonly DataContext _context;

        public StatesController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Post(State state)
        {
            try
            {
                _context.Add(state);
                await _context.SaveChangesAsync();
                return Ok(state);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("El estado/departamento ya existe");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.States
                .Include(x => x.Cities)
                .Where(x => x.Country!.id == pagination.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return Ok(await queryable
                 .OrderBy(x => x.name)
                 .Paginate(pagination)
                 .ToListAsync());
        }

        [HttpGet("totalPages")]
        public async Task<ActionResult> GetPages([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.States
                .Where(x => x.Country!.id == pagination.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            var state = await _context.States
                .Include (x => x.Cities)
                .FirstOrDefaultAsync(x => x.id == id);
            if (state == null)
            {
                return NotFound();
            }
            return Ok(state);
        }

        [HttpPut]
        public async Task<ActionResult> Put(State state)
        {
            try
            {
                _context.Update(state);
                await _context.SaveChangesAsync();
                return Ok(state);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("El estado/departamento ya existe");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var state = await _context.States.FirstOrDefaultAsync(x => x.id == id);
            if (state == null)
            {
                return NotFound();
            }

            _context.Remove(state);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
