
using FluentValidation;
 

namespace Clothes.ViewModels.Validations
{
    public class CredentialsViewModelValidator : AbstractValidator<CredentialsViewModel>
    {
        public CredentialsViewModelValidator()
        {
            RuleFor(vm => vm.UserName).NotEmpty().WithMessage("Usuário não pode ser vazio");
            RuleFor(vm => vm.Password).NotEmpty().WithMessage("Senha não pode ser vazia");
            RuleFor(vm => vm.Password).Length(6, 12).WithMessage("Senha deve ter de 6 a 12 caractéres");
        }
    }
}
