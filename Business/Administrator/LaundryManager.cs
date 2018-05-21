using Clothes.Business.Email;
using Clothes.Business.Services;
using Clothes.Core.Data;
using Clothes.Core.Models.Email;
using Clothes.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Clothes.Business.Administrator
{
    public class LaundryManager
    {

        private readonly ApplicationDbContext _appDbContext;
        private readonly IdentityManager _IdentityManager;
        private readonly IEmailService _emailService;
        private readonly IViewRenderService _viewRenderService;

        public LaundryManager(ApplicationDbContext appDbContext, IdentityManager identityManager, IEmailService emailService, IViewRenderService viewRenderService)
        {
            _appDbContext = appDbContext;
            _IdentityManager = identityManager;
            _emailService = emailService;
            _viewRenderService = viewRenderService;
        }

        public async Task<IEnumerable<Laundry>> List()
        {
            IEnumerable<Laundry> laundries;
            try
            {
                laundries = await _appDbContext.Laundries
                    .Include(x => x.Address)
                    .Include(x => x.BankData)
                    .Include(x => x.Identity)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                //delete identity 
                throw ex;
            }
            return laundries;
        }


        public async Task<Laundry> Find(int id)
        {
            Laundry laundries;
            try
            {
                laundries = await _appDbContext.Laundries
                    .AsNoTracking()
                    .Include(x => x.Address)
                    .Include(x => x.BankData)
                    .Include(x => x.Identity)
                    .FirstOrDefaultAsync(x => x.LaundryId == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return laundries;
        }

        public async Task<Laundry> CreateOrEdit(Laundry laundry)
        {
            try
            {
                bool newLaundry = laundry.LaundryId == 0;


                if (laundry.AddressId == 0)
                    await _appDbContext.Addresses.AddAsync(laundry.Address);
                else
                    _appDbContext.Addresses.Update(laundry.Address);

                if (laundry.BankDataId == 0)
                    await _appDbContext.BankData.AddAsync(laundry.BankData);
                else
                    _appDbContext.BankData.Update(laundry.BankData);

                if (laundry.LaundryId == 0)
                    await _appDbContext.Laundries.AddAsync(laundry);
                else
                    _appDbContext.Laundries.Update(laundry);

                await _appDbContext.SaveChangesAsync();

                laundry = await Find(laundry.LaundryId);

                //teste email
                if (newLaundry)
                {
                    var message = new EmailMessage();
                    message.Subject = "Bem vindo ao iClothes";
                    message.FromAddresses.Add(new EmailAddress() { Address = "noreply@abefarma.com.br", Name = "Clothes" });
                    message.ToAddresses.Add(new EmailAddress() { Address = laundry.Identity.Email, Name = laundry.NomeFantasia });
                    var result = await _viewRenderService.RenderToStringAsync("EmailTemplate/BemVindoLaundry", laundry); 
                    message.Content = result;
                    _emailService.Send(message);
                }
            }
            catch (Exception ex)
            {
                //delete identity
                //await _IdentityManager.Delete(new AppUser() { Id = laundry.IdentityId });
                throw ex;
            }
            return laundry;
        }

        public async Task<object> Delete(int id)
        {

            try
            {
                var laundry = await Find(id);

                if (laundry.BankData != null)
                    _appDbContext.BankData.Remove(laundry.BankData);

                if (laundry.Address != null)
                    _appDbContext.Addresses.Remove(laundry.Address);

                _appDbContext.Laundries.Remove(laundry);

                await _IdentityManager.Delete(new AppUser() { Id = laundry.IdentityId });
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new
            {
                ok = true
            };
        }
    }
}
