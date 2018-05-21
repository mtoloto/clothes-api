
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Clothes.Core.Data;
using Clothes.Core.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Clothes.Areas.AdministratorArea.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("api/administrator/[controller]/[action]")]
    public class DashboardController : Controller
    {
        private readonly ClaimsPrincipal _caller;
        private readonly ApplicationDbContext _appDbContext;

        public DashboardController(UserManager<AppUser> userManager, ApplicationDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _caller = httpContextAccessor.HttpContext.User;
            _appDbContext = appDbContext;
        }

        // GET api/dashboard/home
        [HttpGet]
        public async Task<IActionResult> Home()
        {
            // retrieve the user info
            //HttpContext.User
            var userId = _caller.Claims.FirstOrDefault(c => c.Type == "id");
            var customer = await _appDbContext.Administrators.Include(c => c.Identity).FirstOrDefaultAsync(c => c.Identity.Id == userId.Value);

            return new OkObjectResult(new
            {
                Message = "This is secure API and user data!",
                customer.Identity.FirstName,
                customer.Identity.LastName,
                customer.Identity.PictureUrl,
                customer.Identity.FacebookId
            });
        }
    }
}

