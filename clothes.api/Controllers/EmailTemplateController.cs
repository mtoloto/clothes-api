using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clothes.Business.Administrator;
using Microsoft.AspNetCore.Mvc;

namespace Clothes.Api.Controllers
{
    public class EmailTemplateController : Controller
    {
        private readonly LaundryManager _laundryManager;

        public EmailTemplateController(LaundryManager laundryManager)
        {
            _laundryManager = laundryManager;
        }
        public async Task<IActionResult> BemVindoLaundry(int id, string password)
        {
            var laundry = await _laundryManager.Find(id);
            laundry.Identity.PasswordHash = password;
            return View(laundry);
        }
    }
}