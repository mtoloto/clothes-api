

namespace Clothes.Core.Models.Entities
{
    public class Administrator
    {
        public int Id { get; set; }
        public string IdentityId { get; set; }
        public AppUser Identity { get; set; }  // navigation property  

        public Status Status { get; set; }
    }
}
