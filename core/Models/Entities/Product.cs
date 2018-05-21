

namespace Clothes.Core.Models.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Photo { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int StatusId { get; set; }
        public Status Status { get; set; }

        public decimal Price { get; set; }
        public int MaximumInSameOrder { get; set; }
        public int DiasUteisPrazo { get; set; }
    }
}
