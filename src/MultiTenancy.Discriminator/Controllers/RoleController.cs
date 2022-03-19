using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTenancy.Discriminator.Infrastructure;

namespace MultiTenancy.Discriminator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly MasterDbContext dbContext;

        public RoleController(MasterDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        [HttpGet]
        public async Task<IActionResult> ListRoles() => Ok(await this.dbContext.Role.ToListAsync());
    }
}
