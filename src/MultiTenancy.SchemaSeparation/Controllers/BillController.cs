using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTenancy.SchemaSeparation.Infrastructure;

namespace MultiTenancy.SchemaSeparation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly TenantDbContext context;

        public BillController(TenantDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public async Task<IActionResult> ListBills() => Ok(await this.context.Bill.ToListAsync());
    }
}
