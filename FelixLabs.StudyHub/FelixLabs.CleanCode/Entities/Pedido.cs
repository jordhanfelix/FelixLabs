using System;
using System.Collections.Generic;
using System.Text;

namespace FelixLabs.CleanCode.Entities
{
    public class Pedido
    {
        public Guid Id { get; set; }
        public List<Produto> Produtos { get; set; } = new();
        public DateTime DataSolicitacao { get; set; } = DateTime.Now;
        public decimal Total => Produtos.Sum(p => p.Quantidade * p.Valor);
    }
}
