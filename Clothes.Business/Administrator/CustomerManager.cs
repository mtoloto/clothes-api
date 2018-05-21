using AutoMapper;
using Clothes.Core.Data;
using Clothes.Core.Helpers;
using Clothes.Core.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Clothes.Business.Administrator
{
    public class CustomerManager
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly IdentityManager _IdentityManager;

        public CustomerManager(ApplicationDbContext appDbContext, IdentityManager identityManager)
        {
            _appDbContext = appDbContext;
            _IdentityManager = identityManager;
        }

        public async Task<Customer> CreateOrEdit(Customer user)
        {
            try
            {
                await _appDbContext.Customers.AddAsync(user);
                await _appDbContext.SaveChangesAsync();
                await _IdentityManager.AddClaim(user.Identity, new Claim("Role", "Customer"));
            }
            catch (Exception ex)
            {
                //delete identity
                await _IdentityManager.Delete(new AppUser() { Id = user.IdentityId });
                throw ex;
            }
            return user;
        }
    }
}
