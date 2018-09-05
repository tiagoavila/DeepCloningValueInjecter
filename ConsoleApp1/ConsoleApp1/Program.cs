using ConsoleApp1.DeepCloning;
using Domain;
using Dto;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var pessoa = new Pessoa
            {
                Nome = "Tiago",
                DataNascimento = new DateTime(1991, 7, 9),
                Enderecos = new List<Endereco>
                {
                    new Endereco
                    {
                        Rua = "Rua ABCD",
                        Numero = 1234,
                        Bairro = "Centro",
                        Cidade = "New York"
                    }
                }
            };

            var pessoaDto = new PessoaDto().InjectFrom<CloneInjection>(pessoa);

            Console.ReadKey();
        }
    }
}
