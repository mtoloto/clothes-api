using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clothes.Core.Data;
using Clothes.Core.Models;
using Clothes.Core.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace temp.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("api/[controller]/[action]")]
    public class SystemController : Controller
    {
        private readonly ApplicationDbContext _appDbContext;
        public SystemController(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IEnumerable<Status>> GetAllStatus()
        {
            return await _appDbContext.Status.ToListAsync(); 
        }

        [Authorize(Policy ="Administrator")]
        public async Task<IEnumerable<Bank>> GetAllBanks()
        {
            return await _appDbContext.Banks.ToListAsync();
        }
    }
}
