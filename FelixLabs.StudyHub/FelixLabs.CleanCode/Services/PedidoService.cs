using FelixLabs.CleanCode.Entities;
using FelixLabs.CleanCode.Helpers;
using FelixLabs.CleanCode.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FelixLabs.CleanCode.Services
{
    public class PedidoService(IValidator<Cliente> validator) : IPedidoService
    {
        private List<Cliente> pedidosCliente = [];

        private static string GerarConteudoPedido(Cliente cliente)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Cliente: {cliente.Nome}");
            sb.AppendLine($"Email: {cliente.Email}");
            sb.AppendLine($"Data: {DateTime.Now}");
            sb.AppendLine();

            foreach (var produto in cliente.Pedido.Produtos)
            {
                sb.AppendLine($"{produto.Nome} {produto.Valor} {produto.Quantidade}");
            }

            sb.AppendLine();
            sb.AppendLine($"Total: {cliente.Pedido.Total}");

            return sb.ToString();
        }

        private static void SalvarArquivo(string conteudo)
        {
            var fileName = $"pedido_{Guid.NewGuid()}.txt";

            File.WriteAllText(fileName, conteudo);
        }

        private static string ObterCategoriaCliente(Pedido pedido)
        {
            return pedido.Total > 1000 ? Messages.Vip : Messages.Comum;

        }
        private static void EnviarEmail(Pedido pedido)
        {
            Console.WriteLine("Email enviado");
        }

        private static void FinalizarProcesso()
        {
            Console.WriteLine("Fim");
        }

        private static void FinalizarPedido(Pedido pedido)
        {
            Console.WriteLine(ObterCategoriaCliente(pedido));
            EnviarEmail(pedido);
            FinalizarProcesso();
        }

        public void GerarRelatorio()
        {
            foreach (var cliente in pedidosCliente)
            {
                Console.WriteLine(cliente);
            }
        }

        public void ProcessarPedido(Cliente cliente)
        {
            var result = validator.Validate(cliente);
            if (!result.IsValid)
            {

                var message = string.Join("; ",
                    result.Errors.Select(e => e.ErrorMessage));

                throw new Exception($"Erro de validação: {message}");
            }

            pedidosCliente.Add(cliente);

            Console.WriteLine("Pedido criado");

            try
            {
                var conteudo = GerarConteudoPedido(cliente);
                SalvarArquivo(conteudo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao gerar pedido: {ex.Message}");
            }

            FinalizarPedido(cliente.Pedido);
        }
    }
}
