

using System.Collections.Generic;

namespace Clothes.Core.Models.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string IdentityId { get; set; }
        public AppUser Identity { get; set; }  // navigation property


        //personal information
        public string CPF { get; set; }
        public string RG { get; set; }
        public string Telphone { get; set; }
        public string Celphone { get; set; } 

        public List<Address> Addresses { get; set; }
        public List<CreditCard> CreditCards { get; set; }

        public int StatusId { get; set; }
        public Status Status { get; set; }
    }
}
