using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.API.Data;
using Sales.API.Helpers;
using Sales.Shared.DTOs;
using Sales.Shared.Entities;
using Sales.Shared.Enums;

namespace Sales.API.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/sales")]
    public class SalesController : ControllerBase
    {
        private readonly IOrdersHelper _ordersHelper;
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SalesController(IOrdersHelper ordersHelper, DataContext context, IUserHelper userHelper)
        {
            _ordersHelper = ordersHelper;
            _context = context;
            _userHelper = userHelper;
        }

        [HttpPost]
        public async Task<ActionResult> Post(SaleDTO saleDTO)
        {
            var response = await _ordersHelper.ProcessOrderAsync(User.Identity!.Name!, saleDTO.remarks);
            if (response.IsSuccess)
            {
                return NoContent();
            }

            return BadRequest(response.Message);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            var sale = await _context.Sales
                .Include(s => s.user!)
                .ThenInclude(u => u.City!)
                .ThenInclude(c => c.State!)
                .ThenInclude(s => s.Country)
                .Include(s => s.SaleDetails!)
                .ThenInclude(sd => sd.product)
                .ThenInclude(p => p.ProductImages)
                .FirstOrDefaultAsync(s => s.id == id);

            if (sale == null)
            {
                return NotFound();
            }

            return Ok(sale);
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] PaginationDTO pagination)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.Identity!.Name);
            if (user == null)
            {
                return BadRequest("Usuario no valido.");
            }

            var queryable = _context.Sales
                .Include(s => s.user)
                .Include(s => s.SaleDetails)
                .ThenInclude(sd => sd.product)
                .AsQueryable();

            var isAdmin = await _userHelper.IsUserInRoleAsync(user, UserType.Admin.ToString());
            if (!isAdmin)
            {
                queryable = queryable.Where(s => s.user!.Email == User.Identity!.Name);
            }

            return Ok(await queryable
                .OrderByDescending(x => x.date)
                .Paginate(pagination)
                .ToListAsync());
        }

        [HttpGet("totalPages")]
        public async Task<ActionResult> GetPages([FromQuery] PaginationDTO pagination)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.Identity!.Name);
            if (user == null)
            {
                return BadRequest("Usuario no valido.");
            }

            var queryable = _context.Sales
                .AsQueryable();

            var isAdmin = await _userHelper.IsUserInRoleAsync(user, UserType.Admin.ToString());
            if (!isAdmin)
            {
                queryable = queryable.Where(s => s.user!.Email == User.Identity!.Name);
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }

        [HttpPut]
        public async Task<ActionResult> Put(SaleDTO saleDTO)
        {
            var user = await _userHelper.GetUserAsync(User.Identity!.Name!);
            if (user == null)
            {
                return NotFound();
            }

            var isAdmin = await _userHelper.IsUserInRoleAsync(user, UserType.Admin.ToString());
            if (!isAdmin)
            {
                return BadRequest("Solo permitido para administradores.");
            }

            var sale = await _context.Sales
                .Include(s => s.SaleDetails)
                .FirstOrDefaultAsync(s => s.id == saleDTO.id);
            if (sale == null)
            {
                return NotFound();
            }

            if (saleDTO.orderStatus == OrderStatus.Cancelado)
            {
                await ReturnStockAsync(sale);
            }

            sale.OrderStatus = saleDTO.orderStatus;
            _context.Update(sale);
            await _context.SaveChangesAsync();
            return Ok(sale);
        }

        private async Task ReturnStockAsync(Sale sale)
        {
            foreach (var saleDetail in sale.SaleDetails!)
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.id == saleDetail.productId);
                if (product != null)
                {
                    product.stock += saleDetail.quantity;
                }
            }
            await _context.SaveChangesAsync();
        }

    }
}
