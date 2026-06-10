using FelixLabs.CleanCode.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FelixLabs.CleanCode.Interfaces
{
    public interface IPedidoService
    {
        void ProcessarPedido(Cliente cliente);
        void GerarRelatorio();
    }
}
