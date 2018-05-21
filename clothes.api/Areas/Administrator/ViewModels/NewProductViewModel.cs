 using System.ComponentModel.DataAnnotations;

namespace Clothes.AdministratorArea.ViewModels
{
    public class NewProductViewModel
    { 
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Name { get; set; }
         
        [Required(ErrorMessage = "Descrição é obrigatório")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Preço é obrigatório")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Máximo por pedido é obrigatório")]
        public int MaximumInSameOrder { get; set; }

        [Required(ErrorMessage = "Selecione um status")]
        public int StatusId { get; set; }
    }
}
