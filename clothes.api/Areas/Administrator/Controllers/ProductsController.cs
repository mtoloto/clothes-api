
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
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly IMapper _mapper; 
        private readonly ProductManager _productManager;
        private readonly IHostingEnvironment _environment;

        public ProductsController(ApplicationDbContext appDbContext,
            IMapper mapper, 
            ProductManager productManager,
            IHostingEnvironment environment)
        {
            _appDbContext = appDbContext;
            _mapper = mapper; 
            _productManager = productManager;
            _environment = environment;
        }

        // GET api/dashboard/home
        [HttpGet]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Get(int? id)
        {
            // retrieve the user info 
            object products;
            if (!id.HasValue)
            {
                products = await _appDbContext.Products.Include(product => product.Status).ToListAsync();

            }
            else
            {
                products = await _appDbContext.Products.Include(product => product.Status).FirstOrDefaultAsync(x => x.ProductId == id);
            }
            return new OkObjectResult(new
            {
                products
            });
        }

        [HttpPost]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Post([FromBody]NewProductViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }   

            //creating the product 
            var product = _mapper.Map<Product>(model); 
            var productCreated = await _productManager.CreateOrEdit(product);

            return new OkObjectResult(productCreated);
        } 

        [HttpPut]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody]UpdateLaundryViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //creating the product  
            var product = _mapper.Map<Product>(model);  
            product.ProductId = id; 
            var productUpdated = await _productManager.CreateOrEdit(product);

            return new OkObjectResult(productUpdated);
        }

        [HttpDelete]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            //creating the product     
            var productUpdated = await _productManager.Delete(id);

            return new OkObjectResult(productUpdated);
        }
 
    }
}

