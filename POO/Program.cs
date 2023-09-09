using POO.Classes;

class Program
{
    static void Main()
    {
        string nome = getUserName();
        Pessoa pessoa = new Pessoa(nome);
        Biblioteca biblioteca = new Biblioteca();
        pessoa.adicionarBiblioteca(biblioteca);
        Biblioteca pessoaBiblioteca = pessoa.pegarBiblioteca();

        Menu();

        string getUserName()
        {
            Console.WriteLine("Insira seu nome: ");
            return Console.ReadLine();
        }

        string getInput(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }

        int integerNumber(string message, bool trueOrFalse)
        {
            if (trueOrFalse) { Console.WriteLine(message); }
            string stringToInt = Console.ReadLine();

            bool isNumber = int.TryParse(stringToInt, out int number);
            if (!isNumber) { Console.WriteLine("Número inválido!"); Environment.Exit(0); }

            return number;
        }

        void Menu()
        {
            Console.Clear();
            Console.WriteLine($"{nome} o que deseja fazer na sua biblioteca? \n");
            Console.WriteLine("1 - Adicionar livro");
            Console.WriteLine("2 - Remover livro");
            Console.WriteLine("3 - Adicionar ebook");
            Console.WriteLine("4 - Remover ebook");
            Console.WriteLine("5 - Listar livros");

            int escolha = integerNumber("", false);

            if(escolha == 1) { adicionarLivro(); }
            else if (escolha == 2) { removerLivro(); }
            else if (escolha == 3) { adicionarEbook(); }
            else if (escolha == 4) { removerEbook(); }
            else if (escolha == 5) { listarLivros(); }
            else 
            { 
                Console.WriteLine("Opção inválida"); 
                Menu(); 
            }
        }

        void adicionarLivro()
        {
            Console.Clear();
            Console.WriteLine("\nAdicione um livro à biblioteca: \n");

            string titulo = getInput("Título: ");
            string assunto = getInput("Assunto: ");
            string autor = getInput("Autor: ");

            int ano = integerNumber("Ano: ", true);
            int numeroPaginas = integerNumber("Número de Páginas: ", true);

            Livro livro = new Livro(titulo, assunto, autor, ano, numeroPaginas);
            pessoaBiblioteca.adicionarLivro(livro);

            Thread.Sleep(2000);
            Menu();
        }

        void removerLivro()
        {
            Console.Clear();
            Console.WriteLine("Digite o nome do livro que você quer remover: ");
            string nomeLivro = Console.ReadLine();

            pessoaBiblioteca.removerLivro(nomeLivro);
            Menu();
        }

        void adicionarEbook()
        {
            Console.Clear();
            Console.WriteLine("\nAdicione um livro à biblioteca: \n");

            string id = getInput("ID: ");
            string titulo = getInput("Título: ");
            string assunto = getInput("Assunto: ");
            string autor = getInput("Autor: ");

            int ano = integerNumber("Ano: ", true);
            int numeroPaginas = integerNumber("Número de Páginas: ", true);

            Ebook ebook = new Ebook(id, titulo, assunto, autor, ano, numeroPaginas);
            pessoaBiblioteca.adicionarEbook(ebook);

            Thread.Sleep(2000);
            Menu();
        }

        void removerEbook()
        {
            Console.Clear();
            Console.WriteLine("Digite o nome do ebook que você quer remover: ");
            string nomeEbook = Console.ReadLine();

            pessoaBiblioteca.removerEbook(nomeEbook);
            Menu();
        }

        void listarLivros()
        {
            Console.Clear();
            pessoaBiblioteca.listarLivros();

            Console.WriteLine("\n1 - Voltar");
            int escolha = integerNumber("", false);
            if (escolha == 1) { Menu(); }
            else { Menu(); }
        }

    }
}
