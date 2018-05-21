
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
using System;

namespace Clothes.Areas.AdministratorArea.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("api/administrator/[controller]/[action]/{id?}")]
    public class LaundriesUtilController : Controller
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IdentityManager _identityManager;
        private readonly LaundryManager _laundryManager;
        private readonly IHostingEnvironment _environment;

        public LaundriesUtilController(ApplicationDbContext appDbContext,
            IMapper mapper,
            IdentityManager identityManager,
            LaundryManager laundryManager,
            IHostingEnvironment environment)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _identityManager = identityManager;
            _laundryManager = laundryManager;
            _environment = environment;
        }

        [HttpPost]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> UpdateLogo([FromRoute] int id, [FromForm] IFormFile logo)
        {
            try
            {
                var filePath = Path.Combine(_environment.ContentRootPath, @"wwwroot/assets/Uploads/Laundries", logo.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await logo.CopyToAsync(stream);
                }

                var laundry = await _laundryManager.Find(id);
                laundry.Logo = logo.FileName;

                laundry = await _laundryManager.CreateOrEdit(laundry);

                return new OkObjectResult(laundry);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}

