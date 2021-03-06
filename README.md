# DeepCloningValueInjecter
Exemplo de clonagem de objetos com propriedades com o mesmo nome, mas com tipos diferentes, utilizando ValueInjecter.

## Exemplo de uso

Uma classe de domínio chamada Pessoa possui uma lista de endereços do tipo Endereco. 

```
public class Pessoa
{
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public List<Endereco> Enderecos { get; set; }
}
```
Há uma classe de Dto chamada PessoaDto possui também uma lista de endereços mas do tipo EnderecoDto.

```
public class PessoaDto
{
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public List<EnderecoDto> Enderecos { get; set; }
}
```

No comportamento padrão do ValueInjecter, ao mapear um objeto da classe Pessoa para PessoaDto, a lista de endereços não seria mapeada, pois são de tipos complexos diferentes.

Utilizando a classe de mapeamento CloneInjection é possível obter este tipo de mapeamento. Desta forma:

```
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
```
  
Veja mais em: https://medium.com/code-expert/clonagem-de-objetos-utilizando-valueinjecter-6363119f6ff9
