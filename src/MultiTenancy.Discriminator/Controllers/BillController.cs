using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTenancy.Discriminator.Infrastructure;

namespace MultiTenancy.Discriminator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillController : ControllerBase
    {
        private readonly TenantDbContext dbContext;

        public BillController(TenantDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        [HttpGet]
        public async Task<IActionResult> ListBills() => Ok(await this.dbContext.Bill.ToListAsync());
    }
}
