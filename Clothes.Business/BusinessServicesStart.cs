using Clothes.Business.Administrator;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clothes.Business
{
    public static class BusinessServicesStart
    {
        public static void Start(IServiceCollection services)
        {
            services.AddTransient<IdentityManager, IdentityManager>();
            services.AddTransient<CustomerManager, CustomerManager>();
            services.AddTransient<LaundryManager, LaundryManager>();
            services.AddTransient<ProductManager, ProductManager>();
        }
    }
}
