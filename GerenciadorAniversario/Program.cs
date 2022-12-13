using Library;

public class Program
{
    static int opcao;
    public static void Main(string[] args)
    {
        Console.WriteLine("Gerenciador de Aniversários");
        Console.WriteLine(string.Empty);
        var pessoaRepository = new PessoaRepository();
        do
        {
            ExibirMenu();
            EscolherOpcao();
            switch (opcao)
            {
                case 1:
                    pessoaRepository.PesquisarPessoa();
                    break;
                case 2:
                    pessoaRepository.AdicionarPessoa();
                    break;
                case 3:
                    pessoaRepository.AlterarPessoa();
                    break;
                case 4:
                    pessoaRepository.ExcluirPessoa();
                    break;
                case 5:
                    Console.WriteLine("Fechando Programa!");
                    break;
                default:
                    Console.WriteLine("Escolha uma opção válida!");
                    break;
            }
        } while (!opcao.Equals(5));

        Console.ReadLine();
    }
    public static void EscolherOpcao()
    {
        try
        {
            opcao = int.Parse(Console.ReadLine());
        }
        catch (Exception)
        {
            Console.WriteLine("A opção deve ser um número!");
        }
    }
    public static void ExibirMenu()
    {
        Console.WriteLine("Selecione uma das opções abaixo:");
        Console.WriteLine("1 - Pesquisar pessoas");
        Console.WriteLine("2 - Adicionar nova pessoa");
        Console.WriteLine("3 - Alterar pessoa");
        Console.WriteLine("4 - Excluir pessoa");
        Console.WriteLine("5 - Sair");
    }
}
