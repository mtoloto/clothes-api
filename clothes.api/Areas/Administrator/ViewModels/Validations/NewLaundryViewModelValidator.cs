
using Clothes.ViewModels;
using FluentValidation;


namespace Clothes.AdministratorArea.ViewModels.Validations
{
    public class RegistrationViewModelValidator : AbstractValidator<NewLaundryViewModel>
    {
        public RegistrationViewModelValidator()
        {
            RuleFor(vm => vm.Email).NotEmpty().WithMessage("E-mail é obrigatório"); ;

            RuleFor(vm => vm.StatusId).NotEqual(0).WithMessage("Selecione um status");

            RuleFor(vm => vm.Address.Street).NotEmpty().WithMessage("Rua é obrigatório");
            RuleFor(vm => vm.Address.Number).NotEmpty().WithMessage("Núumero é obrigatório");
            RuleFor(vm => vm.Address.ZipCode).NotEmpty().WithMessage("CEP é obrigatório");
            RuleFor(vm => vm.Address.Neighborhood).NotEmpty().WithMessage("Bairro é obrigatório");
            RuleFor(vm => vm.Address.City).NotEmpty().WithMessage("Cidade é obrigatório");
            RuleFor(vm => vm.Address.State).NotEmpty().WithMessage("Estado é obrigatório");




            RuleFor(vm => vm.BankData.BankId).NotEmpty().WithMessage("Selecione um banco");
            RuleFor(vm => vm.BankData.Agency).NotEmpty().WithMessage("Agencia é obrigatório");
            RuleFor(vm => vm.BankData.DigitAgency).NotEmpty().WithMessage("Digito da agência é obrigatório");
            RuleFor(vm => vm.BankData.CheckingAccount).NotEmpty().WithMessage("Conta corrente é obrigatório");
            RuleFor(vm => vm.BankData.DigitCheckingAccount).NotEmpty().WithMessage("Digito da conta é obrigatório");

        }
    }
}
