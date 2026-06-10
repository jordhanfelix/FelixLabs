using FelixLabs.CleanCode.Entities;
using FelixLabs.CleanCode.Interfaces;
using FelixLabs.CleanCode.Services;
using FelixLabs.CleanCode.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;

namespace FelixLabs.CleanCode
{
    public class X
    {
        private List<object[]> l = new List<object[]>();

        public void A(string n, string e, List<Tuple<string, decimal, int>> p)
        {
            if (n == null || n == "")
            {
                throw new Exception("Nome errado");
            }

            if (e == null || e == "")
            {
                throw new Exception("Email errado");
            }

            decimal t = 0;

            foreach (var i in p)
            {
                t += i.Item2 * i.Item3;
            }

            object[] o =
            {
                Guid.NewGuid(),
                n,
                e,
                DateTime.Now,
                t
            };

            l.Add(o);

            Console.WriteLine("Pedido criado");

            try
            {
                string s = "";

                s += "Cliente:" + n + "\n";
                s += "Email:" + e + "\n";
                s += "Data:" + DateTime.Now + "\n";

                foreach (var i in p)
                {
                    s += i.Item1 + " " + i.Item2 + " " + i.Item3 + "\n";
                }

                s += "Total:" + t;

                File.WriteAllText(
                    $"pedido_{Guid.NewGuid()}.txt",
                    s);
            }
            catch
            {
                Console.WriteLine("Erro");
            }

            if (t > 1000)
            {
                Console.WriteLine("Cliente VIP");
            }
            else
            {
                Console.WriteLine("Cliente comum");
            }

            Console.WriteLine("Email enviado");

            Console.WriteLine("Fim");
        }

        public void B()
        {
            foreach (var x in l)
            {
                Console.WriteLine(
                    $"{x[0]} {x[1]} {x[2]} {x[3]} {x[4]}");
            }
        }
    }

    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Codigo mal escrito:");
            var x = new X();

            var p = new List<Tuple<string, decimal, int>>
            {
                new Tuple<string, decimal, int>(
                    "Notebook",
                    5000,
                    1),

                new Tuple<string, decimal, int>(
                    "Mouse",
                    100,
                    2)
            };

            x.A(
                "Jordan",
                "jordan@email.com",
                p);

            x.B();
            Console.WriteLine("================================================================");
            Console.WriteLine("");

            Console.WriteLine("Codigo com cleancode:");

            var services = new ServiceCollection();

            services.AddScoped<IPedidoService, PedidoService>();
            services.AddScoped<IValidator<Cliente>, ClienteValidator>();
            var serviceProvider = services.BuildServiceProvider();

            var pedidoService = serviceProvider.GetRequiredService<IPedidoService>();

            var cliente = new Cliente
            {
                Nome = "Jordan",
                Email = "jordan@email.com",
                Pedido = new()
                {
                    Produtos = new List<Produto>
                    {
                        new Produto
                        {
                            Nome = "Notebook",
                            Valor = 5000,
                            Quantidade = 1
                        },
                        new Produto
                        {
                            Nome = "Mouse",
                            Valor = 100,
                            Quantidade = 2
                        }
                    }
                }
            };

            pedidoService.ProcessarPedido(cliente);
            pedidoService.GerarRelatorio();
        }
    }
}