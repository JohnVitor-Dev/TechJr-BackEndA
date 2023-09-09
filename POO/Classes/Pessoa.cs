namespace POO.Classes
{
    public class Pessoa
    {
        // Atributos públicos
        public string Nome;
        
        // Atributos privados (Encapsulamento)
        private Biblioteca Biblioteca;

        // Construtor
        public Pessoa(string nome)
        {
            Nome = nome;
        }

        // Getter para a biblioteca
        public Biblioteca pegarBiblioteca()
        {
            return Biblioteca;
        }

        // Setter para a biblioteca
        public void adicionarBiblioteca(Biblioteca biblioteca)
        {
            Biblioteca = biblioteca;
        }

    }
}
