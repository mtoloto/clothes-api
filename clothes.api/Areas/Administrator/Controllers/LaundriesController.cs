
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Clothes.Core.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clothes.Core.Data;
using Clothes.AdministratorArea.ViewModels;
using AutoMapper;
using Clothes.Business.Administrator;
using Clothes.Core.Helpers;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Clothes.Areas.AdministratorArea.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("api/administrator/[controller]/{id?}")]
    public class LaundriesController : Controller
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IdentityManager _identityManager;
        private readonly LaundryManager _laundryManager;
        private readonly IHostingEnvironment _environment;

        public LaundriesController(ApplicationDbContext appDbContext,
            IMapper mapper,
            IdentityManager
            identityManager,
            LaundryManager laundryManager,
            IHostingEnvironment environment)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _identityManager = identityManager;
            _laundryManager = laundryManager;
            _environment = environment;
        }

        // GET api/dashboard/home
        [HttpGet]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Get(int? id)
        {
            // retrieve the user info 
            object laundries;
            if (!id.HasValue)
            {
                laundries = await _appDbContext.Laundries.Include(laundry => laundry.Address).Include(laundry => laundry.Identity).Include(laundry => laundry.BankData).ToListAsync();

            }
            else
            {
                laundries = await _appDbContext.Laundries.Include(laundry => laundry.Address).Include(laundry => laundry.Identity).Include(laundry => laundry.BankData).FirstOrDefaultAsync(x => x.LaundryId == id);
            }
            return new OkObjectResult(new
            {
                laundries
            });
        }

        [HttpPost]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Post([FromBody]NewLaundryViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = _mapper.Map<AppUser>(model);
            userIdentity.FirstName = model.RazaoSocial;
            userIdentity.LastName = model.NomeFantasia;
            string password = Password.GenerateRandomPassword();
            userIdentity.PictureUrl = model.Logo;

            //creating identity
            var result = await _identityManager.CreateOrEdit(userIdentity, password);
            if (result.Error)
                return new BadRequestObjectResult(Errors.AddErrorsToModelState(result.Result, ModelState));

            //creating the laundry 
            var laundry = _mapper.Map<Laundry>(model);
            laundry.IdentityId = userIdentity.Id;
            var laundryCreated = await _laundryManager.CreateOrEdit(laundry);

            return new OkObjectResult(laundryCreated);
        }


        [HttpPut]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody]UpdateLaundryViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //creating the laundry  
            var laundry = _mapper.Map<Laundry>(model);
            var laundryIdentity = await _laundryManager.Find(id);

            laundry.IdentityId = laundryIdentity.IdentityId;
            laundry.AddressId = laundryIdentity.AddressId;
            laundry.BankDataId = laundryIdentity.BankDataId;
            laundry.LaundryId = id;

            var laundryUpdated = await _laundryManager.CreateOrEdit(laundry);

            return new OkObjectResult(laundryUpdated);
        }

        [HttpDelete]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            //creating the laundry     
            var laundryUpdated = await _laundryManager.Delete(id);

            return new OkObjectResult(laundryUpdated);
        }
 
    }
}

