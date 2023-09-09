namespace POO.Classes
{
    public class Ebook : Livro
    {
        // Atributos
        public string ID;

        // Construtor
        public Ebook(string id, string titulo, string assunto, string autor, int ano, int numeroPaginas) : base(titulo, assunto, autor, ano, numeroPaginas)
        {
            ID = id;
        }

        // Polimorfismo 2/2
        public override void sobreLivro()
        {
            Console.WriteLine($"Informações sobre o livro: ");
            Console.WriteLine(ID);
            Console.WriteLine(Titulo);
            Console.WriteLine(Assunto);
            Console.WriteLine(Autor);
            Console.WriteLine(Ano);
            Console.WriteLine(NumeroPaginas);
        }
    }
}
