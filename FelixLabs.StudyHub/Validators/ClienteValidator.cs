using FelixLabs.CleanCode.Entities;
using FelixLabs.CleanCode.Helpers;
using FluentValidation;

namespace FelixLabs.CleanCode.Validators
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(x => x.Nome)
                .NotNull()
                .NotEmpty()
                .WithMessage(Messages.NomeIncorreto);
            
            
            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage(Messages.EmailEnviado);


            RuleFor(x => x.Pedido)
            .NotNull()
            .SetValidator(new PedidoValidator());
        }
    }
}
