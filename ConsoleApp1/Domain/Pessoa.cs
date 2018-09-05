using System;
using System.Collections.Generic;

namespace Domain
{
    public class Pessoa
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public List<Endereco> Enderecos { get; set; }
    }
}
