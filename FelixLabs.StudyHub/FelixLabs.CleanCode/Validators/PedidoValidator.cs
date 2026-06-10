using FelixLabs.CleanCode.Entities;
using FelixLabs.CleanCode.Helpers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FelixLabs.CleanCode.Validators
{
    public class PedidoValidator : AbstractValidator<Pedido>
    {
        public PedidoValidator()
        {
            RuleForEach(x => x.Produtos)
                .SetValidator(new ProdutoValidator());
        }
    }
}
