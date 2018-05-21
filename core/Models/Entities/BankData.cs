

namespace Clothes.Core.Models.Entities
{
    public class BankData
    {
        public int BankDataId { get; set; } 

        public int BankId { get; set; }
        public Bank Bank { get; set; }

        public string Agency { get; set; }
        public string DigitAgency { get; set; } 
        public string CheckingAccount { get; set; } 
        public string DigitCheckingAccount { get; set; }

    }
}
