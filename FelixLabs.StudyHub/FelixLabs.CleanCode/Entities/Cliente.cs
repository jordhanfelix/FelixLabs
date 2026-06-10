using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FelixLabs.CleanCode.Entities
{
    public class Cliente
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; }
        public string Email { get; set; }
        public Pedido Pedido { get; set; }

        public override string ToString()
        {
            return $"{Id} {Nome} {Email} {Pedido.DataSolicitacao} {Pedido.Total}";
        }
    }
}
