

using System.Threading.Tasks;
using Clothes.Core.Data;
using Clothes.Helpers;
using Clothes.Core.Models.Entities;
using Clothes.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Clothes.Core.Models;
using System;
using System.Security.Claims;
using Clothes.Core.Helpers;
using Clothes.Business.Administrator;
using Microsoft.AspNetCore.Authorization;

namespace clothes.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AccountsController : Controller
    {
        private readonly IdentityManager _identityManager;
        private readonly CustomerManager _customerManager;
        private readonly IMapper _mapper;

        public AccountsController(IdentityManager identityManager,
                                  CustomerManager customerManager,
                                  IMapper mapper)
        {
            _identityManager = identityManager;
            _customerManager = customerManager;
            _mapper = mapper;
        }

        // POST api/accounts
        [HttpPost]
        public async Task<IActionResult> Customer([FromBody]RegistrationViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = _mapper.Map<AppUser>(model);
             
            //creating identity
            var result = await _identityManager.CreateOrEdit(userIdentity, model.Password);
            if (result.Error)
                return new BadRequestObjectResult(Errors.AddErrorsToModelState(result.Result, ModelState));

            //creating the customer
            var customer = new Customer(); 
            customer.IdentityId = userIdentity.Id; 

            var customerCreated = await _customerManager.CreateOrEdit(customer); 

            return new OkObjectResult(customerCreated);
        }


        // POST api/accounts
        [HttpPost]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Laundry([FromBody]RegistrationViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = _mapper.Map<AppUser>(model);

            //creating identity
            var result = await _identityManager.CreateOrEdit(userIdentity, model.Password);
            if (result.Error)
                return new BadRequestObjectResult(Errors.AddErrorsToModelState(result.Result, ModelState));

            //creating the customer
            var customer = new Customer();
            customer.IdentityId = userIdentity.Id;

            var customerCreated = await _customerManager.CreateOrEdit(customer);

            return new OkObjectResult(customerCreated);
        }
    }
}
