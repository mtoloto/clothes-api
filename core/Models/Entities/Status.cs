

namespace Clothes.Core.Models.Entities
{
    public class Status
    {
        public int StatusId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string InternalDescription { get; set; }
        public bool Blocked { get; set; }
    }
}
