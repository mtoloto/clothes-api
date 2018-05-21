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
    public class ProductManager
    {

        private readonly ApplicationDbContext _appDbContext;

        public ProductManager(ApplicationDbContext appDbContext, IdentityManager identityManager, IEmailService emailService, IViewRenderService viewRenderService)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Product>> List()
        {
            IEnumerable<Product> products;
            try
            {
                products = await _appDbContext.Products
                    .Include(x => x.Status)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return products;
        }


        public async Task<Product> Find(int id)
        {
            Product products;
            try
            {
                products = await _appDbContext.Products
                    .AsNoTracking()
                    .Include(x => x.Status)
                    .FirstOrDefaultAsync(x => x.ProductId == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return products;
        }

        public async Task<Product> CreateOrEdit(Product product)
        {
            try
            { 
                if (product.ProductId == 0)
                    await _appDbContext.Products.AddAsync(product);
                else
                    _appDbContext.Products.Update(product);

                await _appDbContext.SaveChangesAsync();

                product = await Find(product.ProductId);
 
            }
            catch (Exception ex)
            { 
                throw ex;
            }
            return product;
        }

        public async Task<object> Delete(int id)
        {

            try
            {
                var product = await Find(id); 
                _appDbContext.Products.Remove(product); 
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
