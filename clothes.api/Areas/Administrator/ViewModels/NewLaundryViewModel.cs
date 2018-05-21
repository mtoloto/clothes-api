

using Clothes.Core.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Clothes.AdministratorArea.ViewModels
{
    public class NewLaundryViewModel
    {
        [EmailAddress]
        public string Email { get; set; }


        [EmailAddress]
        [Compare(nameof(Email), ErrorMessage = "Email não é igual a confirmação.")]
        public string EmailConfirm { get; set; }

        [Required(ErrorMessage = "Nome fantasia é obrigatório")]
        public string NomeFantasia { get; set; }
        [Required(ErrorMessage = "Razão Social é obrigatório")]
        public string RazaoSocial { get; set; }
        [Required(ErrorMessage = "CNPJ é obrigatório")]
        public string CNPJ { get; set; }
        [Required(ErrorMessage = "IE é obrigatório")]
        public string IE { get; set; }
        [Required(ErrorMessage = "Logo é obrigatório")]
        public string Logo { get; set; }

        public Address Address { get; set; }
        public BankData BankData{ get; set; }
        public int StatusId { get; set; }
    }
}
