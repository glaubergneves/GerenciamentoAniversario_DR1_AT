namespace Library
{
    public class PessoaRepository : IPessoaRepository
    {
        #region Private Atributes
        private static readonly IList<Pessoa> _pessoas = new List<Pessoa>();

        private static readonly string _diretorio = @"C:\MeuDiretorio2022";

        private static readonly string _nomeArquivo = "MeuArquivo.txt";

        private static readonly string _caminho = Path.Combine(_diretorio, _nomeArquivo);
        #endregion

        #region Public Methods
        public void PesquisarPessoa()
        {
            Carregar();
            Console.WriteLine("Digite o nome do amigo:");
            string nome = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nome))
            {
                Console.WriteLine("Digite um nome válido!");
                return;
            }

            var lista = _pessoas.Where(x => x.Nome.ToLower().Contains(nome.ToLower())).ToList();

            if (!lista.Any())
            {
                Console.WriteLine("Não há itens para exibir!");
            }
            else
            {
                lista.ForEach(x => Console.WriteLine(x));
            }
            Console.WriteLine(string.Empty);
        }
        public void AdicionarPessoa()
        {
            string nome = GetNome();
            string sobrenome = GetSobrenome();
            Console.WriteLine("Digite a data do aniversário no formato dd/MM/yyyy:");
            DateTime dataNascimento = Convert.ToDateTime(Console.ReadLine());
            int id = GetNewId();

            _pessoas.Add(new Pessoa(id, nome, sobrenome, dataNascimento));
            Persistir();
        }
        public void AlterarPessoa()
        {
            PesquisarPessoa();
            var pessoa = GetPessoa();

            if (pessoa == null)
            {
                Console.WriteLine("Id não localizado!");
            }
            else
            {
                Console.WriteLine("Digite o nome:");
                string? nome = Console.ReadLine();
                Console.WriteLine("Digite o sobrenome:");
                string? sobrenome = Console.ReadLine();
                Console.WriteLine("Digite a data do aniversário no formato dd/MM/yyyy:");
                DateTime dataNascimento = Convert.ToDateTime(Console.ReadLine());
                pessoa.Nome = nome;
                pessoa.Sobrenome = sobrenome;
                pessoa.DataNascimento = dataNascimento;

                Console.WriteLine(pessoa);
            }
            Console.WriteLine(string.Empty);
            Persistir();
        }
        public void ExcluirPessoa()
        {
            PesquisarPessoa();
            var pessoa = GetPessoa();

            if (pessoa != null)
            {
                _pessoas.Remove(pessoa);
                return;
            }
            Console.WriteLine("Pessoa não localizada!");
            Persistir();
        }
        #endregion

        #region Private Methods
        private static string GetNome()
        {
            Console.WriteLine("Digite o nome da pessoa que deseja adicionar:");
            string nome = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new Exception("Nome inválido!");
            }

            return nome;
        }
        private static string GetSobrenome()
        {
            Console.WriteLine("Digite o sobrenome da pessoa que deseja adicionar:");
            string sobrenome = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(sobrenome))
            {
                throw new Exception("Nome inválido!");
            }
            return sobrenome;
        }
        private static int GetNewId()
        {
            if (_pessoas.Count == 0)
                return 1;

            return _pessoas.Max(x => x.Id) + 1;
        }
        private static Pessoa? GetPessoa()
        {
            Console.WriteLine("Digite o Id do amigo:");
            int id = 0;

            try
            {
                id = int.Parse(Console.ReadLine());
                var pessoa = _pessoas.FirstOrDefault(x => x.Id == id);
                return pessoa;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        private static void Persistir()
        {
            Directory.CreateDirectory(_diretorio);

            if(File.Exists(_caminho))
            {
                File.Delete(_caminho);
            }
            var x = File.Create(_caminho);
            x.Close();
            x.Dispose();

            var sw = new StreamWriter(_caminho);
            _pessoas.ToList().ForEach(x => sw.WriteLine(x.ToCsv()));

            sw.Close();
        }
        private static void Carregar()
        {
            _pessoas.Clear();            
            var sr = new StreamReader(_caminho);
            var line = sr.ReadLine();

            while(line != null)
            {
                var split = line.Split(";");
                
                var pessoa = new Pessoa();
                pessoa.Id = int.Parse(split[0]);
                pessoa.Nome = split[1];
                pessoa.Sobrenome = split[2];
                pessoa.DataNascimento = Convert.ToDateTime(split[3]);
                _pessoas.Add(pessoa);

                line = sr.ReadLine();
            }
            sr.Close();
        }
        #endregion
    }
}