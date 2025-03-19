using System;
using System.Collections.Generic;

class Program
{
    static string usuarioCorreto = "Omori";
    static string senhaCorreta = "97663"; 
    static List<Cliente> clientes = new List<Cliente>();
    static List<Carro> carros = new List<Carro>();

    static void Main()
    {
        if (!TelaLogin())
        {
            Console.WriteLine("Número máximo de tentativas atingido. Encerrando o sistema...");
            return;
        }

        int opcao;
        do
        {
            Console.Clear();
            Console.WriteLine("\nMenu Principal:");
            Console.WriteLine("1 - Cadastrar Cliente");
            Console.WriteLine("2 - Cadastrar Veículo");
            Console.WriteLine("3 - Listar Veículos Cadastrados");
            Console.WriteLine("4 - Sair");
            Console.Write("Escolha uma opção: ");

            if (int.TryParse(Console.ReadLine(), out opcao))
            {
                switch (opcao)
                {
                    case 1:
                        CadastrarCliente();
                        break;
                    case 2:
                        CadastrarVeiculo();
                        break;
                    case 3:
                        ListarVeiculos();
                        break;
                    case 4:
                        Console.WriteLine("Saindo do sistema...");
                        break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Entrada inválida! Digite um número válido.");
            }
            Console.WriteLine("Pressione Enter para continuar...");
            Console.ReadLine();
        } while (opcao != 4);
    }

    static bool TelaLogin()
    {
        int tentativas = 0;
        while (tentativas < 3)
        {
            Console.Write("Usuário: ");
            string usuario = Console.ReadLine();
            Console.Write("Senha: ");
            string senha = Console.ReadLine();

            if (usuario == usuarioCorreto && senha == senhaCorreta)
            {
                Console.WriteLine("Login bem-sucedido! Pressione Enter para continuar.");
                Console.ReadLine();
                return true;
            }
            else
            {
                tentativas++;
                Console.WriteLine("Usuário ou senha incorretos. Tentativa {0}/3", tentativas);
            }
        }
        return false;
    }

    static void CadastrarCliente()
    {
        Console.Clear();
        Console.Write("Nome Completo: ");
        string nome = Console.ReadLine();
        Console.Write("Data de Nascimento (dd/MM/yyyy): ");
        DateTime dataNascimento;

        while (!DateTime.TryParse(Console.ReadLine(), out dataNascimento))
        {
            Console.Write("Data inválida! Insira novamente (dd/MM/yyyy): ");
        }

        int idade = DateTime.Now.Year - dataNascimento.Year;
        if (DateTime.Now < dataNascimento.AddYears(idade)) idade--; // Corrige idade

        if (idade < 18)
        {
            Console.WriteLine("Cadastro não permitido! Cliente menor de idade.");
            return;
        }

        clientes.Add(new Cliente { Nome = nome, DataNascimento = dataNascimento });
        Console.WriteLine("Cliente cadastrado com sucesso!");
    }

    static void CadastrarVeiculo()
    {
        Console.Clear();
        Console.Write("Marca: ");
        string marca = Console.ReadLine();
        Console.Write("Modelo: ");
        string modelo = Console.ReadLine();
        Console.Write("Ano: ");
        int ano;
        while (!int.TryParse(Console.ReadLine(), out ano) || ano < 1886 || ano > DateTime.Now.Year + 1)
        {
            Console.Write("Ano inválido! Insira novamente: ");
        }
        Console.Write("Valor do veículo (mínimo R$60.000,00): ");
        double valor;
        while (!double.TryParse(Console.ReadLine(), out valor) || valor < 60000)
        {
            Console.Write("Valor inválido! Digite um valor maior que R$60.000: ");
        }

        carros.Add(new Carro { Marca = marca, Modelo = modelo, Ano = ano, Valor = valor });
        Console.WriteLine("Veículo cadastrado com sucesso!");
    }

    static void ListarVeiculos()
    {
        Console.Clear();
        if (carros.Count == 0)
        {
            Console.WriteLine("Nenhum veículo cadastrado.");
            return;
        }
        Console.WriteLine("\nLista de Veículos Cadastrados:");
        foreach (var carro in carros)
        {
            Console.WriteLine($"Marca: {carro.Marca} | Modelo: {carro.Modelo} | Ano: {carro.Ano} | Valor: R${carro.Valor:F2}");
        }
    }
}

class Cliente
{
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
}

class Carro
{
    public string Marca { get; set; }
    public string Modelo { get; set; }
    public int Ano { get; set; }
    public double Valor { get; set; }
}

/// Teste Commit