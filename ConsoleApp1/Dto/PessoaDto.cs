using System;
using System.Collections.Generic;

namespace Dto
{
    public class PessoaDto
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public List<EnderecoDto> Enderecos { get; set; }
    }
}
