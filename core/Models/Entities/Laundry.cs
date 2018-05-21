

namespace Clothes.Core.Models.Entities
{
    public class Laundry
    {
        public int LaundryId { get; set; }
        public string IdentityId { get; set; }
        public virtual AppUser Identity { get; set; }  // navigation property  
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }
        public int? BankDataId { get; set; }
        public virtual BankData BankData { get; set; }

        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string CNPJ { get; set; }
        public string IE { get; set; }
        public string Logo { get; set; }
        public string urlAmigavel { get; set; }
    }
}
