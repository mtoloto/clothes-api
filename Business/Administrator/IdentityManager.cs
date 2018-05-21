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
    public class IdentityManager
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;

        public IdentityManager(UserManager<AppUser> userManager, ApplicationDbContext appDbContext)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
        }

        public async Task<CreateOrEditReturn> CreateOrEdit(AppUser user, string Password)
        {
            var userIdentity = user;

            IdentityResult result;
            if (!string.IsNullOrEmpty(user.Id))
                result = await _userManager.CreateAsync(userIdentity, Password);
            else
                result = await _userManager.UpdateAsync(userIdentity);

            if (!result.Succeeded)
                return new CreateOrEditReturn()
                {
                    Error = true,
                    Result = result
                };

            return new CreateOrEditReturn()
            {
                Error = false,
                User = userIdentity
            };
        }

        public async Task<IdentityResult> Delete(AppUser user)
        {
            try
            {
                var userDelete = await _userManager.FindByIdAsync(user.Id);
                var result = await _userManager.DeleteAsync(userDelete);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IdentityResult> AddClaim(AppUser user, Claim claim)
        {
            var result = await _userManager.AddClaimAsync(user, claim);
            return result;
        }
    }

    public class CreateOrEditReturn
    {
        public bool Error { get; set; }
        public AppUser User { get; set; }
        public IdentityResult Result { get; set; }
    }
}
