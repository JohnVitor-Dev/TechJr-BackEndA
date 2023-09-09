namespace POO.Classes
{
    public class Biblioteca
    {
        // Atributos
        public Livro Livro;
        public Ebook Ebook;
        public List<Livro> Livros;
        public List<Ebook> Ebooks;

        // Construtor
        public Biblioteca()
        {
            // Instanciação
            Livros = new List<Livro>();
            Ebooks = new List<Ebook>();
        }

        // Métodos
        public void listarLivros()
        {
            string livrosString = "Livros físicos";
            if (Livros.Count < 2) { livrosString = "Livro Físico"; }

            string ebooksString = "Livro Digital";
            if (Ebooks.Count < 2) { ebooksString = "Livro Digital"; }

            Console.WriteLine($"\nEsta biblioteca contém {Livros.Count} {livrosString} e {Ebooks.Count} {ebooksString}. \n");

            Console.WriteLine("Físico: ");
            foreach(var livroView in Livros)
            {
                Console.WriteLine(livroView.Titulo);
            }

            Console.WriteLine("Digital: ");
            foreach (var ebookView in Ebooks)
            {
                Console.WriteLine(ebookView.Titulo);
            }
        }

        public void adicionarLivro(Livro livro)
        {
            Livros.Add(livro);
            Console.WriteLine("\nLivro adicionado!");
            Thread.Sleep(2000);
        }

        public void adicionarEbook(Ebook ebook)
        {
            Ebooks.Add(ebook);
            Console.WriteLine("\nEbook adicionado!");
            Thread.Sleep(2000);
        }

        public void removerLivro(string livro)
        {
            List<Livro> livrosParaRemover = new List<Livro>();

            if (Livros.Count != 0)
            {
                foreach (var livroDelete in Livros)
                {
                    if (livroDelete.Titulo == livro)
                    {
                        livrosParaRemover.Add(livroDelete);
                    }
                    else
                    {
                        Console.WriteLine("\nLivro não encontrado!");
                    }
                }

                if (livrosParaRemover.Count != 0)
                {
                    foreach (var livroRemovido in livrosParaRemover)
                    {
                        Livros.Remove(livroRemovido);
                        Console.WriteLine("\nLivro removido!");
                    }
                }
            }
            else
            {
                Console.WriteLine("\nBiblioteca não contém livros físicos!");
            }

            Thread.Sleep(2000);
           
        }

        public void removerEbook(string ebook)
        {
            List<Ebook> ebooksParaRemover = new List<Ebook>();

            if (Ebooks.Count != 0)
            {
                foreach (var ebookDelete in Ebooks)
                {
                    if (ebookDelete.Titulo == ebook)
                    {
                        ebooksParaRemover.Add(ebookDelete);
                    }
                    else
                    {
                        Console.WriteLine("\nEbook não encontrado!");
                    }
                }

                if (ebooksParaRemover.Count != 0)
                {
                    foreach (var ebookRemovido in ebooksParaRemover)
                    {
                        Ebooks.Remove(ebookRemovido);
                        Console.WriteLine("\nEbook removido!");
                    }
                }
            }
            else
            {
                Console.WriteLine("\nBiblioteca não contém ebooks!");
            }

            Thread.Sleep(2000);

        }


    }
}
