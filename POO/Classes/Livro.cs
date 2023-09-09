namespace POO.Classes
{
    public class Livro
    {
        // Atributos
        public string Titulo;
        public string Assunto;
        public string Autor;
        public int Ano;
        public int NumeroPaginas;

        // Construtor
        public Livro(string titulo, string assunto, string autor, int ano, int numeroPaginas)
        {
            Titulo = titulo;
            Assunto = assunto;
            Autor = autor;
            Ano = ano;
            NumeroPaginas = numeroPaginas;
        }

        // Polimorfismo 1/2
        public virtual void sobreLivro()
        {
            Console.WriteLine($"Informações sobre o livro: ");
            Console.WriteLine(Titulo);
            Console.WriteLine(Assunto);
            Console.WriteLine(Autor);
            Console.WriteLine(Ano);
            Console.WriteLine(NumeroPaginas);
        }

    }
}
