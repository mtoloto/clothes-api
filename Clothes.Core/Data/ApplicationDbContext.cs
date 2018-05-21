
using Clothes.Core.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Clothes.Core.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Laundry> Laundries { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<BankData> BankData { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
