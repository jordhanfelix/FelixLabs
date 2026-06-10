using FelixLabs.CleanCode.Entities;
using FelixLabs.CleanCode.Helpers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FelixLabs.CleanCode.Validators
{
    public class ProdutoValidator : AbstractValidator<Produto>
    {
        public ProdutoValidator()
        {

            RuleFor(x => x.Nome)
              .NotNull()
              .NotEmpty()
              .WithMessage(Messages.NomeIncorreto);


            RuleFor(x => x.Valor)
                .GreaterThan(0)
                .WithMessage(Messages.ValorInvalido);

            RuleFor(x => x.Quantidade)
                .GreaterThan(0)
                .WithMessage(Messages.ValorInvalido);
        }
    }
}
