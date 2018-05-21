namespace Clothes.Core.Models.Entities
{
    public class CreditCard
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public string Hash { get; set; }

        public string NameOnCard { get; set; }

        public Status Status { get; set; }

    }
}
