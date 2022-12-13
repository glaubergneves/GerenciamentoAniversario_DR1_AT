namespace Library
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }

        public Pessoa()
        {
        }

        public Pessoa(int id, string? nome, string? sobrenome, DateTime dataNascimento)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            DataNascimento = dataNascimento;
        }

        public override string? ToString()
        {
            return $"Id: {Id} | Nome: {Nome} | Sobrenome: {Sobrenome} | Data de nascimento: {DataNascimento.ToShortDateString()}";
        }

        public string ToCsv()
        {
            return $"{Id};{Nome};{Sobrenome};{DataNascimento.ToShortDateString()};";
        }
    }
}
